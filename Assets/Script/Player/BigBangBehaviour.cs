using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBangBehaviour : GenericBehaviour
{
    private int bigBangTrigger;
    private bool bigBang;
    public Transform myTransform;
    [HideInInspector]
    public int keyLock;
    public bool mouseLock;
    public GameObject bigBangDamageChecker;
    public PlayerAttackCollsion attackCollsion;
    //게이지 카운트 필요

    private void Start()
    {
        bigBangTrigger = Animator.StringToHash(AnimatorKey.BigBang);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
    }

    private void BigBangManagement()
    {
        if (!behaviourController.IsGrounded()) //빅뱅이 아닌 상황
        {
            return;
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
    public void BigBangEffect1()
    {
        behaviourController.camScript.CamShakeTime(0.3f, 0.3f);
        behaviourController.particleSystems[5].Play();
    }
    public void BigBangEffect2()
    {
        behaviourController.camScript.CamShakeTime(0.1f, 0.1f);
        behaviourController.particleSystems[2].Play();
    }

    public void BigBangkStiffenCheckStart()
    {
        bigBangDamageChecker.SetActive(true);
    }

    public void BigBangStiffenCheckEnd()
    {
        bigBangDamageChecker.SetActive(false);
    }
}
