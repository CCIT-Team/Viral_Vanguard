using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 가드 행동 카메라가 보고 있는 방향으로 가드가 진행됨
/// </summary>
public class GuardBehaviour : GenericBehaviour
{
    public float guardTurnSmoothing;
    public float turnSmoothing;
    public float guardFOV = 50;
    private int guardBool;
    private bool guard;
    private Transform myTransform;
    private EvasionBehaviour evasionBehaviour; //참조

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

        if (behaviourController.GetTempLockStatus(behaviourCode) || behaviourController.IsOverrideing(this)) //스테미나 없으면 가드 불가 또는 회피중, 공격중
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
        //걷기만 가능한지?
        
    }
}
