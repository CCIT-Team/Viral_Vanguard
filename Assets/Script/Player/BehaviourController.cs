using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.VFX;

/// <summary>
/// ����, �⺻, �������̵�, ��� ���۵� + ���콺 �̵���, �� Ȯ��, GenericBehaviour������Ʈ ������
/// �÷��ź� ����
/// </summary>
public class BehaviourController : MonoBehaviour
{
    //���氡��
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
    private bool guardHit;       //������ ���Ͱ� ������
    private bool justGuard;
    private bool guardBreak;
    private bool monsterAttack;
    private bool normalMosterAttack;
    [HideInInspector]
    public int lockOn;
    public ParticleSystem[] particleSystems;  //0 ����Ʈ ����, 1 ���� ��Ʈ, 2 ���, 3�ǰ� ��, 4 �ǰ� ��, 5 ���� �극��ũ 6:�÷��̾� ������, 7: �÷��̾� �ް�
    public VisualEffect[] visualEffects; //0 ���
    public GameObject[] gameObjectsEffects; //Ű��ƽ �� ����

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

    public bool JustGuard //���� �����Ҷ�
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

    public void SetStiffen(int stiffenNum) //��ó �ؼ��� BOSSMOVE
    {
        if (stiffenNum == 1)
        {
            RightStiffen = true;
        }
        else if (stiffenNum == 2)
        {
            LeftStiffen = true;
        }
        else if (stiffenNum == 3) //�Ϲ� ����
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
        //���� or ����Ʈ
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

    //�� ����
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

    //�ൿ ���
    public void SubscribeBehaviour(GenericBehaviour behaviour)
    {
        behaviours.Add(behaviour);
    }
    //�⺻ �ൿ ���
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
    //�ൿ ����
    public void UnRegisterBehavior(int behaviourCode)
    {
        if (currentBehaviours == behaviourCode)
        {
            currentBehaviours = defaultBehaviour;
        }
    }
    //�������̵� �ൿ -- �켱�� �Ǵ� �ൿ���� �����
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

    public bool RevokeOverridingBehaviour(GenericBehaviour behaviour) //����
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