using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본적인 움직임 디폴트 행동이라고 생각하면 됨
/// </summary>
public class MoveBehaviour : GenericBehaviour
{
    public float runSpeed = 1.0f;
    public float speedDampTime = 0.1f;
    public float speed;

    //여기서는 회피 구현 X 회피는 최우선 사항으로 생각해서 회피 행동으로 따로 뺄 예정
    private int groundedBool;
    //private bool isColliding;
    private CapsuleCollider capsuleCollider;
    private Transform myTransform;
    private bool isColliding;

    private void Start()
    {
        myTransform = transform;
        capsuleCollider = GetComponent<CapsuleCollider>();
        groundedBool = Animator.StringToHash(AnimatorKey.Grounded);
        behaviourController.GetAnimator.SetBool(groundedBool, true);

        behaviourController.SubscribeBehaviour(this);
        behaviourController.RegisterDefaultBehavior(behaviourCode);
    }

    Vector3 Rotating(float horizontal, float vertical)
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);

        forward.y = 0.0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        Vector3 targetDirection = Vector3.zero;
        targetDirection = forward * vertical + right * horizontal;

        if (behaviourController.IsMoving() && targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion newRotation = Quaternion.Slerp(behaviourController.GetRigidbody.rotation, targetRotation, behaviourController.turnSmooth);
            behaviourController.GetRigidbody.MoveRotation(newRotation);
            behaviourController.SetLastDirection(targetDirection);
        }

        if (!(Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f))
        {
            behaviourController.Repositioning();
        }
        return targetDirection;
    }

    private void RemoveVerticalVelocity()
    {
        Vector3 horizontalVelocity = behaviourController.GetRigidbody.velocity;
        horizontalVelocity.y = 0.0f;
        behaviourController.GetRigidbody.velocity = horizontalVelocity;
    }

    void MovementManagement(float horizontal, float vertical)
    {
        if(behaviourController.IsGrounded())
        {
            behaviourController.GetRigidbody.useGravity = true;
        }
        else if(behaviourController.GetRigidbody.velocity.y > 0)
        {
            RemoveVerticalVelocity();
        }
        Rotating(horizontal, vertical);
        Vector2 dir = new Vector2(horizontal, vertical);
        speed = Vector2.ClampMagnitude(dir, 1f).magnitude * runSpeed;
        behaviourController.GetAnimator.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;

        if (behaviourController.IsCurrentBehaviour(GetBehaviourCode) && collision.GetContact(0).normal.y <= 0.1f)
        {
            float vel = behaviourController.GetAnimator.velocity.magnitude;
            Vector3 targetMove = Vector3.ProjectOnPlane(myTransform.forward, collision.GetContact(0).normal).normalized * vel;
            behaviourController.GetRigidbody.AddForce(targetMove, ForceMode.VelocityChange);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }


    private void Update()
    {
        
    }

    public override void LocalFixedUpdate()
    {
        MovementManagement(behaviourController.GetH, behaviourController.GetV);
    }

}

