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

    public LayerMask playerIKLayerMask;
    public Transform leftFootTransform;
    public Transform rightFootTranform;
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

    public bool staminaCharge;
    public float staminaChargeSpeed;
    public float currentStamina = 100f;
    public float maxStamina;
    public float maxKineticEnergy = 100f;
    public float currentKineticEnergy;

    public float staminaCoolTime;
    private float currentStaminaTime = 0f;

    public bool isDead;
    public bool isBigBang = false;
    public bool isBigBang1 = false;
    public bool guard;
    private bool guardHit;       //가드중 몬스터가 때리면
    private bool justGuard;
    private bool guardBreak;
    private bool monsterAttack;
    private bool normalMosterAttack;
    private int soundIndex;
    [HideInInspector]
    public int lockOn;
    public ParticleSystem[] particleSystems;  //0 저스트 가드, 1 가드 히트, 2 빅뱅, 3피격 왼, 4 피격 오, 5 가드 브레이크 6:플레이어 오른공, 7: 플레이어 왼공
    public GameObject[] gameObjectsEffects; //키네틱 온 오프
    public Transform[] soundPosition;

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
                SoundManager.instance.OnShot("PlayerHitFormBoss");
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
                SoundManager.instance.OnShot("PlayerHit1");
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
                SoundManager.instance.OnShot("PlayerHit2");
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

    public void StaminaChargeOn()
    {
        if (guard)
            staminaCoolTime = 3f;
        if(currentStaminaTime < staminaCoolTime)
        {
            staminaCoolTime -= Time.deltaTime;
            if(currentStaminaTime >= staminaCoolTime)
            {
                staminaCharge = true;
                staminaCoolTime = 3f;
            }
        }
    }

    public void StaminaChargeOff()
    {
        staminaCharge = false;
        staminaCoolTime = 3f;
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
        SoundManager.instance.OnShot("PlayerDeath1"); //이거도 랜덤으로 나와야함
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
        float speeds = dir.sqrMagnitude;
        
        
        myAnimator.SetFloat(speedFloat, speeds);

        myAnimator.SetBool(groundedBool, IsGrounded());

        if (staminaCharge == true)
        {
            if (currentStamina <= maxStamina)
                currentStamina += staminaChargeSpeed * Time.deltaTime;
            stageUIManager.PlayerUpdateStamina();
        }
        else if (staminaCharge == false)
        {
            StaminaChargeOn();
        }
        //나중에 이펙트 추가시 사용
        //if (currentKineticEnergy >= 50f)
        //{
        //    gameObjectsEffects[0].SetActive(true);
        //}
        //else if (currentKineticEnergy >= 99f)
        //{
        //    gameObjectsEffects[1].SetActive(true);
        //}
        //else if(currentKineticEnergy < 50f)
        //{
        //    gameObjectsEffects[0].SetActive(false);
        //    gameObjectsEffects[1].SetActive(false);
        //}

    }

    public void OnAnimatorIK()
    {
        animatorIKSetting();
    }

    public void animatorIKSetting()
    {
        //왼발
        myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);

        Ray leftFoot = new Ray(myAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

        float leftFootDistance = leftFootTransform.position.y - transform.position.y;

        if (Physics.Raycast(leftFoot, out RaycastHit leftHit, leftFootDistance + 1f, playerIKLayerMask))
        {
            if(leftHit.transform.CompareTag("WalkableGround"))
            {
                Vector3 footPosition = leftHit.point;
                footPosition.y += leftFootDistance;

                myAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                myAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, leftHit.normal));
            }
        }

        //오른발
        myAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        myAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        Ray rightFoot = new Ray(myAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

        float rightFootDistance = rightFootTranform.position.y - transform.position.y;

        if (Physics.Raycast(rightFoot, out RaycastHit rightHit, rightFootDistance + 1f, playerIKLayerMask))
        {
            if (rightHit.transform.CompareTag("WalkableGround"))
            {
                Vector3 footPosition = rightHit.point;
                footPosition.y += rightFootDistance;

                myAnimator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                myAnimator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, rightHit.normal));
            }
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

    #region 플레이어 사운드 이벤트
    public void AttackSound1()
    {
        SoundManager.instance.OnShot("PlayerAttack1");
    }
    public void AttackSound2()
    {
        SoundManager.instance.OnShot("PlayerAttack2");
    }
    public void AttackSound3()
    {
        SoundManager.instance.OnShot("PlayerAttack3");
    }
    public void AttackBreath1()
    {
        SoundManager.instance.OnShot("PlayerBreath1");
    }
    public void AttackBreath2()
    {
        SoundManager.instance.OnShot("PlayerBreath2");
    }
    public void AttackBreath3()
    {
        SoundManager.instance.OnShot("PlayerBreath3");
    }
    public void BigBangSound()
    {
        SoundManager.instance.OnShot("BigBang");
    }
    public void GuardSound()//랜덤 사운드로 적용 필요
    {
        //랜덤으로 나와야함
        SoundManager.instance.OnShot("PlayerGuard1");
    }
    public void GuradBreakSound()
    {
        SoundManager.instance.OnShot("GuardBreak");
    }
    public void JustGuardSound()
    {
        SoundManager.instance.OnShot("PlayerJustGuard1");
        SoundManager.instance.OnShot("PlayerJustGuard2");
    }
        #endregion
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