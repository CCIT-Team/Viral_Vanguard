using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.VFX;

/// <summary>
/// 현재, 기본, 오버라이딩, 잠긴 동작들 + 마우스 이동값, 땅 확인, GenericBehaviour업데이트 시켜줌
/// 플러거블 패턴
/// </summary>
public class BehaviourController : MonoBehaviour
{
    //변경가능
    public static BehaviourController instance;

    private List<GenericBehaviour> behaviours = new List<GenericBehaviour>();
    private List<GenericBehaviour> overrideBehaviours = new List<GenericBehaviour>();
    private int currentBehaviours;
    private int defaultBehaviour;
    private int behaviourLocked;

    public Transform playerCamera;
    public Transform myTransform;
    public Animator myAnimator;
    public Rigidbody myRigidbody;
    public PlayerCamera camScript;
    public StageUIManager stageUIManager;

    private float horizontal;
    private float vertical;
    public float turnSmooth = 0.06f;
    private bool changedFieldOfView;

    private Vector3 lastDirection;
    private int speedFloat;
    private int horizontalFloat;
    private int verticalFloat;
    private int groundedBool;
    private Vector3 colliderExtents;

    //
    public float maxHealthPoint;
    public float currentHealthPoint;
    private bool stiffen;
    private bool rightStiffen;
    private bool leftStiffen;

    private bool staminaCharge;
    public float staminaChargeSpeed;
    public float currentStamina = 100f;
    public float maxStamina;
    public float maxKineticEnergy = 100f;
    public float currentKineticEnergy;

    public bool isDead;
    public bool isBigBang = false;
    public bool guard;
    private bool guardHit;       //가드중 몬스터가 때리면
    private bool justGuard;
    private bool guardBreak;
    private bool monsterAttack;
    private bool normalMosterAttack;
    [HideInInspector]
    public int lockOn;
    public ParticleSystem[] particleSystems;  //0 저스트 가드, 1 가드 히트, 2 빅뱅, 3피격 왼, 4 피격 오, 5 가드 브레이크 6:플레이어 오른공, 7: 플레이어 왼공
    public VisualEffect[] visualEffects; //0 빅뱅
    public GameObject[] gameObjectsEffects; //키네틱 온 오프

    public bool GuardHit
    {
        get { return guardHit; }
        set
        {
            guardHit = value;
            if (guard == true)
            {
                myAnimator.SetTrigger("GuardHit");
                particleSystems[1].Play();
            }
        }
    }

    public bool GuardBreak
    {
        get { return guardBreak; }
        set
        {
            guardBreak = value;
            if(guard == true)
            {
                myAnimator.SetTrigger("GuardBreak");
                particleSystems[5].Play();
            }
        }
    }

    public bool MonsterAttack
    {
        get { return monsterAttack; }
        set
        {
            monsterAttack = value;
        }
    }

    public bool NormalMonsterAttack
    {
        get { return normalMosterAttack; }
        set
        {
            normalMosterAttack = value;
        }
    }

    public bool JustGuard //적이 공격할때
    {
        get { return justGuard; }
        set
        {
            justGuard = value;
            if (justGuard == true)
            {
                myAnimator.SetTrigger("JustGuard");
                particleSystems[0].Play();
            }
        }
    }

    public bool Stiffen
    {
        get { return stiffen; }
        set
        {
            stiffen = value;
            if (guard == false)
            {
                myAnimator.SetBool("Stiffen", value);
            }
        }
    }
    public bool RightStiffen
    {
        get { return rightStiffen; }
        set
        {
            rightStiffen = value;
            if (guard == false)
            {
                myAnimator.SetBool("RightStiffen", value);
                particleSystems[4].Play();
            }
        }
    }

    public bool LeftStiffen
    {
        get { return leftStiffen; }
        set
        {
            leftStiffen = value;
            if (guard == false)
            {
                myAnimator.SetBool("LeftStiffen", value);
                particleSystems[3].Play();
            }
        }
    }

    public void SetStiffen(int stiffenNum) //출처 해성이 BOSSMOVE
    {
        if (stiffenNum == 1)
        {
            RightStiffen = true;
        }
        else if (stiffenNum == 2)
        {
            LeftStiffen = true;
        }
        else if (stiffenNum == 3) //일반 경직
        {
                Stiffen = true;
        }
    }
    public float HealthPoint
    {
        get => currentHealthPoint;
        set
        {
            currentHealthPoint = value;
            stageUIManager.PlayerUpdateHP();
            if(currentHealthPoint <= 0)
            {
                IsDead();
            }
        }
            
    }
    public float Horizontal { get => horizontal; }
    public float Vertical { get => vertical; }
    public int DefailtBehaviour { get => defaultBehaviour; }


    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
        maxHealthPoint = currentHealthPoint;
        maxStamina = currentStamina;
        isDead = false;
        speedFloat = Animator.StringToHash(AnimatorKey.Speed);
        horizontalFloat = Animator.StringToHash(AnimatorKey.Horizontal);
        verticalFloat = Animator.StringToHash(AnimatorKey.Vertical);
        groundedBool = Animator.StringToHash(AnimatorKey.Grounded);
        colliderExtents = GetComponent<Collider>().bounds.extents;
        lockOn = Animator.StringToHash(AnimatorKey.LockOn);
    }

    public IEnumerator StaminaChargeOn()
    {
        yield return new WaitForSeconds(2f);
        if (maxStamina >= currentStamina)
            staminaCharge = true;
    }

    public void StaminaChargeOff()
    {
        staminaCharge = false;
    }

    private void IsDead()
    {
        isDead = true;
        gameObject.tag = "Untagged";
        myAnimator.SetFloat(AnimatorKey.Speed, 0f);
        myAnimator.SetBool(AnimatorKey.Dead, isDead);
        myAnimator.SetBool(AnimatorKey.Attack1, false);
        myAnimator.SetBool(AnimatorKey.Attack2, false);
        myAnimator.SetBool(AnimatorKey.Attack3, false);
        myAnimator.SetBool(AnimatorKey.Guard, false);
        myAnimator.SetBool(AnimatorKey.MouseLock, false);
        myAnimator.SetBool("Stiffen", false);
        foreach (GenericBehaviour behaviour in GetComponentsInChildren<GenericBehaviour>())
        {
            behaviour.enabled = false;
        }
        stageUIManager.BossFailAnimation();
        //사운드 or 이펙트
    }

    public bool IsMoving()
    {
        return Mathf.Abs(horizontal) > Mathf.Epsilon || Mathf.Abs(vertical) > Mathf.Epsilon;
    }

    public bool IsHorizontalMoving()
    {
        return Mathf.Abs(horizontal) > Mathf.Epsilon;
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(myTransform.position + Vector3.up * 2 * colliderExtents.x, Vector3.down);
        return Physics.SphereCast(ray, colliderExtents.x, colliderExtents.x + 0.2f);
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        myAnimator.SetFloat(horizontalFloat, horizontal);
        myAnimator.SetFloat(verticalFloat, vertical);
        Vector3 dir = new Vector3(Horizontal, 0, Vertical);
        dir.Normalize();
        float speeds = dir.sqrMagnitude;
        
        myAnimator.SetFloat(speedFloat, speeds);

        myAnimator.SetBool(groundedBool, IsGrounded());

        if (staminaCharge == true)
        {
            if (currentStamina <= maxStamina)
                currentStamina += staminaChargeSpeed * Time.deltaTime;
            stageUIManager.PlayerUpdateStamina();
        }

        if (currentKineticEnergy >= 50f)
        {
            gameObjectsEffects[0].SetActive(true);
        }
        else if (currentKineticEnergy >= 99f)
        {
            gameObjectsEffects[1].SetActive(true);
        }
        else if(currentKineticEnergy < 50f)
        {
            gameObjectsEffects[0].SetActive(false);
            gameObjectsEffects[1].SetActive(false);
        }
    }

    //낌 방지
    public void Repositioning()
    {
        if(lastDirection != Vector3.zero)
        {
            lastDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            Quaternion newRotation = Quaternion.Slerp(myRigidbody.rotation, targetRotation, turnSmooth);
            myRigidbody.MoveRotation(newRotation);
        }
    }

    private void FixedUpdate()
    {
        bool isAnyBehaniourActive = false;
        if (behaviourLocked > 0 || overrideBehaviours.Count == 0)
        {
            foreach (GenericBehaviour behaviour in behaviours)
            {
                if(behaviour.isActiveAndEnabled && currentBehaviours == behaviour.GetBehaviourCode)
                {
                    isAnyBehaniourActive = true;
                    behaviour.LocalFixedUpdate();
                }
            }
        }
        else
        {
            foreach (GenericBehaviour behaviour in overrideBehaviours)
            {
                behaviour.LocalFixedUpdate();
            }
        }

        if(!isAnyBehaniourActive && overrideBehaviours.Count == 0)
        {
            myRigidbody.useGravity = true;
            Repositioning();
        }
    }

    private void LateUpdate()
    {
        if (behaviourLocked > 0 || overrideBehaviours.Count == 0)
        {
            foreach (GenericBehaviour behaviour in behaviours)
            {
                if (behaviour.isActiveAndEnabled && currentBehaviours == behaviour.GetBehaviourCode)
                {
                    behaviour.LocalLateUpdate();
                }
            }
        }
        else
        {
            foreach (GenericBehaviour behaviour in overrideBehaviours)
            {
                behaviour.LocalLateUpdate();
            }
        }
    }

    //행동 등록
    public void SubscribeBehaviour(GenericBehaviour behaviour)
    {
        behaviours.Add(behaviour);
    }
    //기본 행동 등록
    public void RegisterDefaultBehavior(int behaviourCode)
    {
        defaultBehaviour = behaviourCode;
        currentBehaviours = behaviourCode;
    }

    public void RegisterBehavior(int behaviourCode)
    {
        if (currentBehaviours == defaultBehaviour)
        {
            currentBehaviours = behaviourCode;
        }
    }
    //행동 해제
    public void UnRegisterBehavior(int behaviourCode)
    {
        if (currentBehaviours == behaviourCode)
        {
            currentBehaviours = defaultBehaviour;
        }
    }
    //오버라이딩 행동 -- 우선시 되는 행동으로 덮어씌움
    public bool OverrideWithBehaviour(GenericBehaviour behaviour)
    {
        if (!overrideBehaviours.Contains(behaviour))
        {
            if(overrideBehaviours.Count ==0)
            {
                foreach(GenericBehaviour behaviour1 in behaviours)
                {
                    if(behaviour1.isActiveAndEnabled && currentBehaviours == behaviour1.GetBehaviourCode)
                    {
                        behaviour1.OnOverride();
                        break;
                    }
                }
            }
            overrideBehaviours.Add(behaviour);
            return true;
        }
        return false;
    }

    public bool RevokeOverridingBehaviour(GenericBehaviour behaviour) //해제
    {
        if (overrideBehaviours.Contains(behaviour))
        {
            overrideBehaviours.Remove(behaviour);
            return true;
        }
        return false;
    }

    public bool IsOverrideing(GenericBehaviour behaviour = null)
    {
        if(behaviour == null)
        {
            return overrideBehaviours.Count > 0;
        }
        return overrideBehaviours.Contains(behaviour);
    }

    public bool IsCurrentBehaviour(int behaviourCode)
    {
        return currentBehaviours == behaviourCode;
    }

    public bool GetTempLockStatus(int behaviourCode = 0)
    {
        return (behaviourLocked != 0 && behaviourLocked != behaviourCode);
    }

    public void LockTempBehaviour(int behaviourCode)
    {
        if(behaviourLocked == 0)
        {
            behaviourLocked = behaviourCode;
        }
    }

    public void UnLockTempBehaviour(int behaviourCode)
    {
        if(behaviourLocked == behaviourCode)
        {
            behaviourLocked = 0;
        }
    }

     public Vector3 GetLastDirection()
    {
        return lastDirection;
    }

    public void SetLastDirection(Vector3 direction)
    {
        lastDirection = direction;
    }
}


public abstract class GenericBehaviour : MonoBehaviour
{
    protected int speedFloat;
    protected BehaviourController behaviourController;
    protected int behaviourCode;

    private void Awake()
    {
        behaviourController = GetComponent<BehaviourController>();
        speedFloat = Animator.StringToHash(AnimatorKey.Speed);
        behaviourCode = GetType().GetHashCode();
    }

    public int GetBehaviourCode
    {
        get => behaviourCode;
    }

    public virtual void LocalLateUpdate()
    {

    }

    public virtual void LocalFixedUpdate()
    {

    }

    public virtual void OnOverride()
    {

    }
}