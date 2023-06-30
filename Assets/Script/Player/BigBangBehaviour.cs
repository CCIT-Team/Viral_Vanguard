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
    public GameObject bigBangDamageChecker1;
    public PlayerAttackCollsion attackCollsion;
    //������ ī��Ʈ �ʿ�

    private void Start()
    {
        bigBangTrigger = Animator.StringToHash(AnimatorKey.BigBang);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
    }

    private void BigBangManagement()
    {
        if (!behaviourController.IsGrounded()) //����� �ƴ� ��Ȳ
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
        yield return new WaitForSeconds(1f);
        behaviourController.UnLockTempBehaviour(behaviourCode);
        behaviourController.RevokeOverridingBehaviour(this);
    }

    public override void LocalLateUpdate()
    {
        BigBangManagement();
    }

    private void Update()
    {
        if (Input.GetButtonDown(ButtonKey.BigBnag) && !bigBang && behaviourController.currentKineticEnergy >= 100f) //���� ������ �̻� Ȥ�� ������ �۵�
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
    public void IsBigBangTrue1()
    {
        behaviourController.isBigBang1 = true;
    }

    public void IsBigBangFalse1()
    {
        behaviourController.isBigBang1 = false;
    }
    public void BigBangEffect1()
    {
        behaviourController.camScript.CamShakeTime(0.3f, 0.3f);
        behaviourController.particleSystems[5].Play();
    }
    public void BigBangEffect2()
    {
        behaviourController.camScript.CamShakeTime(0.3f, 0.4f);
        behaviourController.particleSystems[2].Play();
    }
    public void BigBangEffect3()
    {
        behaviourController.camScript.CamShakeTime(0.3f, 0.6f);
        behaviourController.particleSystems[6].Play();
    }

    public void BigBangkStiffenCheckStart()
    {
        bigBangDamageChecker.SetActive(true);
    }

    public void BigBangStiffenCheckEnd()
    {
        bigBangDamageChecker.SetActive(false);
    }
    public void BigBangkStiffenCheckStart1()
    {
        bigBangDamageChecker1.SetActive(true);
    }

    public void BigBangStiffenCheckEnd1()
    {
        bigBangDamageChecker1.SetActive(false);
    }

    //��� ���Ͱ� ����ϴ� �̺�Ʈ
    public IEnumerator BigBangTimeScaleChage()
    {
        behaviourController.myAnimator.speed = 0.0f;
        yield return new WaitForSeconds(0.5f);
        behaviourController.myAnimator.speed = 1f;
    }

    public IEnumerator BigBangTimeScaleChage1()
    {
        behaviourController.myAnimator.speed = 0.3f;
        yield return new WaitForSeconds(0.5f);
        behaviourController.myAnimator.speed = 1f;
    }

    public void ChargeMaceOn()
    {
        behaviourController.gameObjectsEffects[0].SetActive(true);
        behaviourController.gameObjectsEffects[1].SetActive(true);
    }

    public void ChargeMaceOff()
    {
        behaviourController.gameObjectsEffects[0].SetActive(false);
        behaviourController.gameObjectsEffects[1].SetActive(false);
    }

}
