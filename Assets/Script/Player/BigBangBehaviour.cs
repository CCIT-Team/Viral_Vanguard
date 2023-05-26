using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBangBehaviour : GenericBehaviour
{
    private int grounded;//애니용
    private int bigBangTrigger;
    private bool bigBang;
    public Transform myTransform;
    [HideInInspector]
    public int keyLock;
    public bool mouseLock;
    //게이지 카운트 필요

    private void Start()
    {
        grounded = Animator.StringToHash(AnimatorKey.Grounded);
        bigBangTrigger = Animator.StringToHash(AnimatorKey.BigBang);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
    }

    Vector3 Rotation(float horizontal, float vertical)
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0.0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        Vector3 targetDirection = Vector3.zero;
        targetDirection = forward * vertical + right * horizontal;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation = Quaternion.Slerp(behaviourController.myRigidbody.rotation, targetRotation, behaviourController.turnSmooth);
        behaviourController.myRigidbody.MoveRotation(newRotation);
        behaviourController.SetLastDirection(targetDirection);

        if (!(Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f))
        {
            behaviourController.Repositioning();
        }
        return targetDirection;
    }


    private void RemoveVerticalVelocity()
    {
        Vector3 horizotalVelocity = behaviourController.myRigidbody.velocity;
        horizotalVelocity.y = 0.0f;
        behaviourController.myRigidbody.velocity = horizotalVelocity;
    }

    private void RotationManagment(float horizontal, float vertical)
    {
        if (behaviourController.IsGrounded())
        {
            behaviourController.myRigidbody.useGravity = true;
        }
        else if (behaviourController.myRigidbody.velocity.y > 0)
        {
            RemoveVerticalVelocity();
        }
        Rotation(horizontal, vertical);
    }

    private void BigBangManagement()
    {
        if (!behaviourController.IsGrounded()) //빅뱅이 아닌 상황
        {
            return;
        }
        else
        {
            RotationManagment(behaviourController.Horizontal, behaviourController.Vertical);
        }
    }
    private IEnumerator ToggleBigBangOn()
    {
        yield return new WaitForSeconds(0.05f);
        if (behaviourController.IsOverrideing(this))
        {
            yield return false;
        }
        else
        {
            bigBang = true;
            mouseLock = true;
            behaviourController.OverrideWithBehaviour(this);
            behaviourController.LockTempBehaviour(behaviourCode);
            behaviourController.myAnimator.SetTrigger(bigBangTrigger);
            behaviourController.StaminaChargeOff();
            behaviourController.myAnimator.SetBool(keyLock, mouseLock);
        }
    }

    private IEnumerator ToggleBigBangOff()
    {
        bigBang = false;
        yield return new WaitForSeconds(0.5f);
        behaviourController.UnLockTempBehaviour(behaviourCode);
        behaviourController.RevokeOverridingBehaviour(this);
    }

    public override void LocalLateUpdate()
    {
        BigBangManagement();
    }

    private void Update()
    {
        if (Input.GetButtonDown(ButtonKey.BigBnag) && !bigBang && behaviourController.currentKineticEnergy >= 100f) //일정 게이지 이상 혹은 같으면 작동
        {
            StartCoroutine(ToggleBigBangOn());
        }
        else if (bigBang && Input.GetAxisRaw(ButtonKey.BigBnag) == 0)
        {
            StartCoroutine(ToggleBigBangOff());
        }
    }

    public void ReducedKineticEnergyBigBang()
    {
        behaviourController.currentKineticEnergy -= 20;
        behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
    }

    public void IsBigBangTrue()
    {
        behaviourController.isBigBang = true;
    }

    public void IsBigBangFalse()
    {
        behaviourController.isBigBang = false;
    }

    public void BigBangEffect2()
    {
        behaviourController.particleSystems[2].Play();
    }
}
