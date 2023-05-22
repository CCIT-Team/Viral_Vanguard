using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StiffenBehaviour : GenericBehaviour
{
    private int attack1;
    private int attack2;
    private int attack3;
    private int guardBool;
    [HideInInspector]
    public int keyLock;
    public bool mouseLock;
    private void Start()
    {
        attack1 = Animator.StringToHash(AnimatorKey.Attack1);
        attack2 = Animator.StringToHash(AnimatorKey.Attack2);
        attack3 = Animator.StringToHash(AnimatorKey.Attack3);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
        guardBool = Animator.StringToHash(AnimatorKey.Guard);
    }
    private void Update()
    {
        if (behaviourController.Stiffen)
        {
            StartCoroutine(StiffenDelay());
        }
        
    }

    private IEnumerator StiffenDelay()
    {
        yield return new WaitForSeconds(0.05f);
        behaviourController.LockTempBehaviour(behaviourCode);
        behaviourController.OverrideWithBehaviour(this);
        behaviourController.StaminaChargeOff();
        yield return new WaitForSeconds(0.5f);
        behaviourController.UnLockTempBehaviour(behaviourCode);
        behaviourController.RevokeOverridingBehaviour(this);
    }

    public void ResetAnimations()
    {
        behaviourController.myAnimator.SetBool(attack1, false);
        behaviourController.myAnimator.SetBool(attack2, false);
        behaviourController.myAnimator.SetBool(attack3, false);
        behaviourController.myAnimator.SetBool(guardBool, false);
    }

    public void PlayerStiffenFalse()
    {
        behaviourController.Stiffen = false;
    }

}
