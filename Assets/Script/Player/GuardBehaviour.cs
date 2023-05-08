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
    public float guardFOV = 50;
    private int guardBool;
    private bool guard;
    private Transform myTransform;
    private EvasionBehaviour evasionBehaviour; //����

    private void Start()
    {
        myTransform = transform;
        guardBool = Animator.StringToHash(AnimatorKey.Guard);
        evasionBehaviour = GetComponent<EvasionBehaviour>();
    }

    void Rotating()
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Quaternion targetRotation = Quaternion.Euler(0f, behaviourController.GetCamScript.GetH, 0.0f);
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

        if (behaviourController.GetTempLockStatus(behaviourCode) || behaviourController.IsOverrideing(this)) //���׹̳� ������ ���� �Ұ� �Ǵ� ȸ����, ������
        {
            yield return false;
        }
        else
        {
            guard = true;
            behaviourController.GetCamScript.SetFOV(guardFOV);
            behaviourController.GetAnimator.SetBool(guardBool, guard);
            yield return new WaitForSeconds(0.1f);
            //behaviourController.GetAnimator.SetFloat(speedFloat, 0.0f);
            behaviourController.OverrideWithBehaviour(this);
        }
    }

    private IEnumerator ToggleGuardOff()
    {
        guard = false;
        behaviourController.GetAnimator.SetBool(guardBool, guard);
        yield return new WaitForSeconds(0.3f);
        behaviourController.GetCamScript.ResetFOV();
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
            behaviourController.GetAnimator.SetBool(evasionBehaviour.keyLock, evasionBehaviour.mouseLock);
        }
        if(Input.GetAxisRaw(ButtonKey.Guard) !=0 && !guard)
        {
            StartCoroutine(ToggleGuardOn());
        }
        else if(guard && Input.GetAxisRaw(ButtonKey.Guard) == 0)
        {
            StartCoroutine(ToggleGuardOff());
        }
        //�ȱ⸸ ��������?
        
    }
}
