using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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
    public PlayerCameraManager camScript;

    private float horizontal;
    private float vertical;
    public float turnSmooth = 0.06f;
    private bool changedFieldOfView;

    private Vector3 lastDirection;
    private int horizontalFloat;
    private int verticalFloat;
    private int groundedBool;
    private Vector3 colliderExtents;
    private bool stiffen;

    public bool Stiffen
    {
        get { return stiffen; }
        set
        {
                stiffen = value;
                myAnimator.SetBool("Stiffen", value);
        }
    }
    public float _horizontal { get => horizontal; }
    public float _vertical { get => vertical; }
    public int _defailtBehaviour { get => defaultBehaviour; }

    private void Awake()
    {
        instance = this;
        horizontalFloat = Animator.StringToHash(AnimatorKey.Horizontal);
        verticalFloat = Animator.StringToHash(AnimatorKey.Vertical);
        groundedBool = Animator.StringToHash(AnimatorKey.Grounded);
        colliderExtents = GetComponent<Collider>().bounds.extents;
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

        myAnimator.SetFloat(horizontalFloat, horizontal, 0.1f, Time.deltaTime);
        myAnimator.SetFloat(verticalFloat, vertical, 0.1f, Time.deltaTime);
        //transform.position += h * transform.right + v * transform.forward;


        myAnimator.SetBool(groundedBool, IsGrounded());
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