using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ȸ�� ĳ���Ͱ� ���� �ִ� �������� ȸ�ǰ� ����� ������ ȸ��X, ȸ���� ����X
/// </summary>
public class EvasionBehaviour : GenericBehaviour
{
    private int evasionTrigger;
    private bool evasion;
    [HideInInspector]
    public int keyLock;
    public bool mouseLock;
    public float evasionDelay = 0.2f;
    public float reducedStaminaEvasion = 10f;
    //�� �ൿ ��Ÿ��


    private void Start()
    {
        evasionTrigger = Animator.StringToHash(AnimatorKey.Evasion);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
        mouseLock = false;
    }

    private void EvasionManagement()
    {
        if(!behaviourController.IsGrounded())
        {
            return;
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
            behaviourController.myAnimator.SetTrigger(evasionTrigger);
            behaviourController.StaminaChargeOff();
            behaviourController.myAnimator.SetBool(keyLock, mouseLock);
            behaviourController.LockTempBehaviour(behaviourCode);
        }
    }

    private IEnumerator ToggleEvasionOff()
    {
        evasion = false;
        yield return new WaitForSeconds(evasionDelay); //ȸ�� ������
        behaviourController.UnLockTempBehaviour(behaviourCode);
        yield return new WaitForSeconds(0.5f);
        behaviourController.RevokeOverridingBehaviour(this);
    }

    public override void LocalLateUpdate()
    {
        EvasionManagement();
    }

    private void Update()
    {
        if(Input.GetAxisRaw(ButtonKey.Evasion) !=0 && !evasion && behaviourController.currentStamina >= reducedStaminaEvasion)
        {
            StartCoroutine(ToggleEvasionOn());
        }
        else if(evasion && Input.GetAxisRaw(ButtonKey.Evasion) == 0)
        {
            StartCoroutine(ToggleEvasionOff());
        }
    }

    public void ReducedstaminaEvasion()
    {
        behaviourController.currentStamina -= reducedStaminaEvasion;
    }
}
