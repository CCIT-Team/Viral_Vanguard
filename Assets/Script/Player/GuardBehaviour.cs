using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���� �ൿ ī�޶� ���� �ִ� �������� ���尡 �����
/// </summary>
public class GuardBehaviour : GenericBehaviour
{
    public float guardTurnSmoothing;
    public float turnSmoothing;
    public float guardFieldOfView = 50;
    private int guardBool;
    public Transform myTransform;
    private EvasionBehaviour evasionBehaviour; //����
    private AttackBehaviour attackBehaviour;   //����
    private BigBangBehaviour bigBangBehaviour; //����
    public float reducedStaminaGuard;

    //�� �ൿ ��Ÿ��

    private void Start()
    {
        guardBool = Animator.StringToHash(AnimatorKey.Guard);
        evasionBehaviour = GetComponent<EvasionBehaviour>();
        attackBehaviour = GetComponent<AttackBehaviour>();
        bigBangBehaviour = GetComponent<BigBangBehaviour>();
    }

    void Rotating()
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Quaternion targetRotation = Quaternion.Euler(0f, behaviourController.camScript._getHorizotal, 0.0f);
        float mimSpeed = Quaternion.Angle(myTransform.rotation, targetRotation) * turnSmoothing;

        behaviourController.SetLastDirection(forward);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, mimSpeed * Time.deltaTime);
    }

    void GuardManagement()
    {
        Rotating();
    }

    private IEnumerator ToggleGuardOn()
    {
        yield return new WaitForSeconds(0.05f);

        if (behaviourController.GetTempLockStatus(behaviourCode) || behaviourController.IsOverrideing(this))
        {
            yield return false;
        }
        else
        {
            behaviourController.guard = true;
            behaviourController.camScript.SetFieldOfView(guardFieldOfView);
            behaviourController.myAnimator.SetBool(guardBool, behaviourController.guard);
            yield return new WaitForSeconds(0.1f);
            //behaviourController.GetAnimator.SetFloat(speedFloat, 0.0f);
            behaviourController.OverrideWithBehaviour(this);
        }
    }

    private IEnumerator ToggleGuardOff()
    {
        behaviourController.guard = false;
        behaviourController.myAnimator.SetBool(guardBool, behaviourController.guard);
        yield return new WaitForSeconds(0.3f);
        behaviourController.camScript.ResetFieldOfView();
        yield return new WaitForSeconds(0.1f);
        behaviourController.RevokeOverridingBehaviour(this);

    }


    public override void LocalLateUpdate()
    {
        GuardManagement();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            evasionBehaviour.mouseLock = false;
            attackBehaviour.mouseLock = false;
            bigBangBehaviour.mouseLock = false;
            behaviourController.myAnimator.SetBool(evasionBehaviour.keyLock, evasionBehaviour.mouseLock);
            behaviourController.myAnimator.SetBool(attackBehaviour.keyLock, attackBehaviour.mouseLock);
            behaviourController.myAnimator.SetBool(bigBangBehaviour.keyLock, bigBangBehaviour.mouseLock);
        }
        if (Input.GetAxisRaw(ButtonKey.Guard) != 0 && !behaviourController.guard && !evasionBehaviour.mouseLock && !attackBehaviour.mouseLock && !bigBangBehaviour.mouseLock && behaviourController.stamina >= 0) //���׹̳� ������ �Ұ���
        {
            StartCoroutine(ToggleGuardOn());
            
        }
        else if (behaviourController.guard && Input.GetAxisRaw(ButtonKey.Guard) == 0 || evasionBehaviour.mouseLock || attackBehaviour.mouseLock || bigBangBehaviour.mouseLock || behaviourController.stamina <= 0)
        {
            StartCoroutine(ToggleGuardOff());
        }

        //����Ʈ ����
        if (Input.GetAxisRaw(ButtonKey.JustGuard) != 0 && behaviourController.guard)
        {
            behaviourController.justGuard = true;
        }
        else if (Input.GetAxisRaw(ButtonKey.JustGuard) == 0)
        {
            behaviourController.justGuard = false;
        }
    }
}
