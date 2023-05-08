using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ȸ�� ĳ���Ͱ� ���� �ִ� �������� ȸ�ǰ� �����
/// </summary>
public class EvasionBehaviour : GenericBehaviour
{
    private int evasionTrigger;
    public bool evasion;
    private Transform myTransform;
    private int groundedBool;
    public int keyLock;
    public bool mouseLock;
    public float evasionDelay = 0.2f;

    //������ ��ũ��Ʈ���� ������ ������Ʈ�� ������ ����ϸ� ������ ������?


    private void Start()
    {
        myTransform = transform;
        evasionTrigger = Animator.StringToHash(AnimatorKey.Evasion);
        groundedBool = Animator.StringToHash(AnimatorKey.Grounded);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
        mouseLock = false;
    }

    //ȸ��
    Vector3 Rotation(float horizontal, float vertical)
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0.0f; 
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        Vector3 targetDirection = Vector3.zero;
        targetDirection = forward * vertical + right * horizontal;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation = Quaternion.Slerp(behaviourController.GetRigidbody.rotation, targetRotation, behaviourController.turnSmooth);
        behaviourController.GetRigidbody.MoveRotation(newRotation);
        behaviourController.SetLastDirection(targetDirection);

        if (!(Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f))
        {
            behaviourController.Repositioning();
        }
        return targetDirection;
    }


    private void RemoveVerticalVelocity()
    {
        Vector3 horizotalVelocity = behaviourController.GetRigidbody.velocity;
        horizotalVelocity.y = 0.0f;
        behaviourController.GetRigidbody.velocity = horizotalVelocity;
    }

    void RotationManagement(float horizontal, float vertical)
    {
        if (behaviourController.IsGrounded())
        {
            behaviourController.GetRigidbody.useGravity = true;
        }
        else if (behaviourController.GetRigidbody.velocity.y > 0)
        {
            RemoveVerticalVelocity();
        }
        Rotation(horizontal, vertical);
    }

    private void EvasionManagement()
    {
        if(!behaviourController.IsGrounded()) //������ �ƴ� ��Ȳ
        {
            return;
        }
        else
        {
            RotationManagement(behaviourController.GetH, behaviourController.GetV);
        }    
    }


    private IEnumerator ToggleEvasionOn()
    {
        yield return new WaitForSeconds(0.05f);
        if (behaviourController.GetTempLockStatus(behaviourCode) || behaviourController.IsOverrideing(this))
        {
            yield return false;
        }
        else
        {
            evasion = true;
            mouseLock = true;
            behaviourController.OverrideWithBehaviour(this);
            behaviourController.GetAnimator.SetTrigger(evasionTrigger);
            behaviourController.GetAnimator.SetBool(keyLock, mouseLock);
            behaviourController.LockTempBehaviour(behaviourCode);
        }
        
    }

    private IEnumerator ToggleEvasionOff()
    {
        evasion = false;
        yield return new WaitForSeconds(evasionDelay); //ȸ�� ������
        behaviourController.UnLockTempBehaviour(behaviourCode);
        yield return new WaitForSeconds(2f);
        behaviourController.RevokeOverridingBehaviour(this);
    }

    public override void LocalLateUpdate()
    {
        EvasionManagement();
    }

    private void Update()
    {
        if(Input.GetAxisRaw(ButtonKey.Evasion) !=0 && !evasion)
        {
            StartCoroutine(ToggleEvasionOn());
        }
        else if(evasion && Input.GetAxisRaw(ButtonKey.Evasion) == 0)
        {
            StartCoroutine(ToggleEvasionOff());
        }
        
    }


}
