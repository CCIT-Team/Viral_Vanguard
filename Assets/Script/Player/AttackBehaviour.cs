using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어 공격 행동 공격중 회피 X, 회피중 공격X
/// </summary>
public class AttackBehaviour : GenericBehaviour
{
    public PlayerAttackCollsion playerAttackCollsion;
    private int attack1;
    private int attack2;
    private int attack3;

    public int clicks = 0;
    private float lastClickedTime = 0;
    public float attackDelay = 0.2f;
    [HideInInspector]
    public int keyLock;
    public bool mouseLock;
    public GameObject damageChecker;

    public float AttackStamina1;
    public float AttackStamina2;
    public float AttackStamina3;

    private void Start()
    {
        attack1 = Animator.StringToHash(AnimatorKey.Attack1);
        attack2 = Animator.StringToHash(AnimatorKey.Attack2);
        attack3 = Animator.StringToHash(AnimatorKey.Attack3);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
        mouseLock = false;
    }

    private void AttackManagement()
    {
        if(!behaviourController.IsGrounded()) //빅뱅이 아닌 상황
        {
            return;
        }
    }

    public override void LocalLateUpdate()
    {
        AttackManagement();
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.05f);
        if (behaviourController.GetTempLockStatus(behaviourCode))
        {
            yield return false;
        }
        else
        {
            mouseLock = true;
            behaviourController.myAnimator.SetBool(keyLock, mouseLock);
            behaviourController.OverrideWithBehaviour(this);
            behaviourController.LockTempBehaviour(behaviourCode);
            lastClickedTime = Time.time;
            clicks++;
            if (clicks == 1 && behaviourController.currentStamina > 5f)
            {
                behaviourController.myAnimator.SetBool(attack1, true);
            }
            clicks = Mathf.Clamp(clicks, 0, 3);
        }
    }

    private void Update()
    {
        if(Time.time - lastClickedTime > attackDelay)
        {
            behaviourController.UnLockTempBehaviour(behaviourCode);
            behaviourController.RevokeOverridingBehaviour(this);
            clicks = 0;
        }

        if (Input.GetAxisRaw(ButtonKey.Attack) != 0)
        {
            StartCoroutine(Attack());
        }
    }

    public void AttackReturn1()
    {
        if(clicks >= 2 && behaviourController.currentStamina > 7f)
        {
            behaviourController.myAnimator.SetBool(attack2, true);
            behaviourController.myAnimator.SetBool(attack1, false);
            
        }
        else
        {
            behaviourController.myAnimator.SetBool(attack1, false);
            clicks = 0;
        }
    }
    public void AttackReturn2()
    {
        if (clicks >= 3 && behaviourController.currentStamina > 10f)
        {
            behaviourController.myAnimator.SetBool(attack3, true);
        }
        else
        {
            behaviourController.myAnimator.SetBool(attack2, false);
            clicks = 0;
        }
    }
    public void AttackReturn3()
    {
        behaviourController.myAnimator.SetBool(attack1, false);
        behaviourController.myAnimator.SetBool(attack2, false);
        behaviourController.myAnimator.SetBool(attack3, false);
        clicks = 0;
        
    }
    public void AttackReturnReSet()
    {
        if(behaviourController.myAnimator.GetBool(attack1) && behaviourController.myAnimator.GetBool(attack2) && behaviourController.myAnimator.GetBool(attack3))
        {
            behaviourController.myAnimator.SetBool(attack1, false);
            behaviourController.myAnimator.SetBool(attack2, false);
            behaviourController.myAnimator.SetBool(attack3, false);
            clicks = 0;
            mouseLock = true;
        }
    }

    public void ComboAttackStiffenCheckStart()
    {
        damageChecker.SetActive(true);
    }

    public void ComboAttackStiffenCheckEnd()
    {
        damageChecker.SetActive(false);
    }

    public void StiffenAttackDelayReset()
    {
        attackDelay = 0f;
    }
    public void StiffenAttackDelay()
    {
        attackDelay = 0.5f;
    }
    public void Attack1Shake()
    {
        behaviourController.camScript.CamShakeTime(0.15f, 0.08f);
    }
    public void Attack2Shake()
    {
        behaviourController.camScript.CamShakeTime(0.15f, 0.1f);
    }
    public void Attack3Shake()
    {
        behaviourController.camScript.CamShakeTime(0.2f, 0.15f);
    }
    public void ReducedstaminaAttack1()
    {
        behaviourController.currentStamina -= AttackStamina1;
    }
    public void ReducedstaminaAttack2()
    {
        behaviourController.currentStamina -= AttackStamina2;
    }
    public void ReducedstaminaAttack3()
    {
        behaviourController.currentStamina -= AttackStamina3;
    }
}
