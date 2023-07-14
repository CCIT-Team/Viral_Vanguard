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
    private EvasionBehaviour evasionBehaviour;
    private AttackBehaviour attackBehaviour;
    private BigBangBehaviour bigBangBehaviour;
    public float reducedStaminaGuard;
    public bool isJustGuardDelay;
    public AvatarMask avatarMask;
    public bool guardMouseLock;


    public BoxCollider bossAttackCheckCollider;
    public MeshRenderer meshRenderer;

    //�� �ൿ ��Ÿ��

    private void Start()
    {
        guardMouseLock = false;
        guardBool = Animator.StringToHash(AnimatorKey.Guard);
        evasionBehaviour = GetComponent<EvasionBehaviour>();
        attackBehaviour = GetComponent<AttackBehaviour>();
        bigBangBehaviour = GetComponent<BigBangBehaviour>();
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
            behaviourController.StaminaChargeOff();
            yield return new WaitForSeconds(0.1f);
            //behaviourController.myAnimator.SetFloat(speedFloat, 0.0f);
            //behaviourController.OverrideWithBehaviour(this);
        }
    }

    private IEnumerator ToggleGuardOff()
    {
        behaviourController.guard = false;
        behaviourController.myAnimator.SetBool(guardBool, behaviourController.guard);
        yield return new WaitForSeconds(0.3f);
        behaviourController.camScript.ResetFieldOfView();
        yield return new WaitForSeconds(0.1f);
        //behaviourController.RevokeOverridingBehaviour(this);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            evasionBehaviour.mouseLock = false;
            attackBehaviour.mouseLock = false;
            bigBangBehaviour.mouseLock = false;
            guardMouseLock = false;
            behaviourController.myAnimator.SetBool(evasionBehaviour.keyLock, evasionBehaviour.mouseLock);
            behaviourController.myAnimator.SetBool(attackBehaviour.keyLock, attackBehaviour.mouseLock);
            behaviourController.myAnimator.SetBool(bigBangBehaviour.keyLock, bigBangBehaviour.mouseLock);
        }

        if (Input.GetAxisRaw(ButtonKey.Guard) != 0 && !behaviourController.guard && !guardMouseLock && !evasionBehaviour.mouseLock && !attackBehaviour.mouseLock && !bigBangBehaviour.mouseLock && behaviourController.currentStamina >= 0.1 ) //���׹̳� ������ �Ұ���
        {
            StartCoroutine(ToggleGuardOn());

        }
        else if (behaviourController.guard && Input.GetAxisRaw(ButtonKey.Guard) == 0 || guardMouseLock || evasionBehaviour.mouseLock || attackBehaviour.mouseLock || bigBangBehaviour.mouseLock || behaviourController.currentStamina <= 0)
        {
            StartCoroutine(ToggleGuardOff());
        }

        if(Input.GetKeyDown(KeyCode.Space) && behaviourController.guard && !isJustGuardDelay && behaviourController.currentStamina > 20f)
        {
            guardMouseLock = true;
            behaviourController.myAnimator.SetTrigger("JustGuard");
            behaviourController.myAnimator.SetBool("JustGuardCheck", true);
            behaviourController.JustGuardSwingSound();
            behaviourController.currentStamina -= 20f;
            behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            if (behaviourController.MonsterAttack)
            {
                behaviourController.JustGuard = true;         
            }
            //else if (behaviourController.NormalMonsterAttack)
            //{
            //    behaviourController.JustGuard = true;
            //    MonsterMovement.instance.Stiffen = true;
            //    if (behaviourController.currentKineticEnergy >= behaviourController.maxKineticEnergy)
            //    {
            //        behaviourController.currentKineticEnergy = 100f;
            //    }
            //    else
            //    {
            //        behaviourController.currentKineticEnergy += 5f;
            //    }
            //    behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            //}
            isJustGuardDelay = true;
            StartCoroutine(JustGuardOnce());
        }
    }

    IEnumerator JustGuardOnce()
    {
        yield return new WaitForSeconds(2f);
        isJustGuardDelay = false;
    }
    public void playerJustGuardFalse()
    {
        behaviourController.JustGuard = false;
        behaviourController.myAnimator.SetBool("JustGuardCheck", false);
        behaviourController.guard = false;
    }

    public IEnumerator shieldShaderOn()
    {
        meshRenderer.materials[0].SetFloat("_Speed", 1f);
        yield return new WaitForSeconds(0.5f);
        meshRenderer.materials[0].SetFloat("_Speed", 0f);
    }
}
