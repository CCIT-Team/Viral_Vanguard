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
    public float guardFieldOfView = 50;
    private int guardBool;
    public Transform myTransform;
    private EvasionBehaviour evasionBehaviour;
    private AttackBehaviour attackBehaviour;
    private BigBangBehaviour bigBangBehaviour;
    public float reducedStaminaGuard;
    public bool isJustGuardDelay;

    //각 행동 쿨타임

    private void Start()
    {
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
            behaviourController.myAnimator.SetBool(evasionBehaviour.keyLock, evasionBehaviour.mouseLock);
            behaviourController.myAnimator.SetBool(attackBehaviour.keyLock, attackBehaviour.mouseLock);
            behaviourController.myAnimator.SetBool(bigBangBehaviour.keyLock, bigBangBehaviour.mouseLock);
        }

        if (Input.GetAxisRaw(ButtonKey.Guard) != 0 && !behaviourController.guard && !evasionBehaviour.mouseLock && !attackBehaviour.mouseLock && !bigBangBehaviour.mouseLock && behaviourController.currentStamina >= 0.1) //스테미나 없으면 불가능
        {
            StartCoroutine(ToggleGuardOn());

        }
        else if (behaviourController.guard && Input.GetAxisRaw(ButtonKey.Guard) == 0 || evasionBehaviour.mouseLock || attackBehaviour.mouseLock || bigBangBehaviour.mouseLock || behaviourController.currentStamina <= 0)
        {
            StartCoroutine(ToggleGuardOff());
        }

        if(Input.GetKeyDown(KeyCode.Space) && behaviourController.guard)
        {
            
            if (behaviourController.MonsterAttack && !isJustGuardDelay)
            {
                behaviourController.JustGuard = true;
                BossMove.instacne.SetStiffen(0);
                if (behaviourController.currentKineticEnergy >= behaviourController.maxKineticEnergy)
                {
                    behaviourController.currentKineticEnergy = 100f;
                }
                else
                {
                    behaviourController.currentKineticEnergy += 20f;
                }
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                
            }
            else if (behaviourController.NormalMonsterAttack && !isJustGuardDelay)
            {
                behaviourController.JustGuard = true;
                MonsterMovement.instance.Stiffen = true;
                if (behaviourController.currentKineticEnergy >= behaviourController.maxKineticEnergy)
                {
                    behaviourController.currentKineticEnergy = 100f;
                }
                else
                {
                    behaviourController.currentKineticEnergy += 5f;
                }
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
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
        behaviourController.guard = false;
    }
}
