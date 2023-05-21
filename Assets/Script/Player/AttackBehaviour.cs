using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어 공격 행동 공격중 회피 X, 회피중 공격X
/// </summary>
public class AttackBehaviour : GenericBehaviour
{
    public PlayerAttackCollsion playerAttackCollsion;
    public float[] damage;
    private int grounded;//애니용
    private int attack1;
    private int attack2;
    private int attack3;
    public int clicks = 0;
    private float lastClickedTime = 0;
    public float attackDelay = 0.5f;
    [HideInInspector]
    public int keyLock;
    public bool mouseLock;
    public GameObject damageChecker;


    //각 행동 쿨타임

    private void Start()
    {
        attack1 = Animator.StringToHash(AnimatorKey.Attack1);
        attack2 = Animator.StringToHash(AnimatorKey.Attack2);
        attack3 = Animator.StringToHash(AnimatorKey.Attack3);
        keyLock = Animator.StringToHash(AnimatorKey.MouseLock);
        grounded = Animator.StringToHash(AnimatorKey.Grounded);
        mouseLock = false;
    }

    //공격전 회전값
    Vector3 Rotation(float horizontal, float vertical)
    {
        Vector3 forward = behaviourController.playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0.0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        Vector3 targetDirection = Vector3.zero;
        targetDirection = forward * vertical + right * horizontal;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation = Quaternion.Slerp(behaviourController.myRigidbody.rotation, targetRotation, behaviourController.turnSmooth);
        behaviourController.myRigidbody.MoveRotation(newRotation);
        behaviourController.SetLastDirection(targetDirection);

        if (!(Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f))
        {
            behaviourController.Repositioning();
        }
        return targetDirection;
    }


    private void RemoveVerticalVelocity()
    {
        Vector3 horizotalVelocity = behaviourController.myRigidbody.velocity;
        horizotalVelocity.y = 0.0f;
        behaviourController.myRigidbody.velocity = horizotalVelocity;
    }

    private void RotationManagment(float horizontal, float vertical)
    {
        if(behaviourController.IsGrounded())
        {
            behaviourController.myRigidbody.useGravity = true;
        }
        else if (behaviourController.myRigidbody.velocity.y > 0)
        {
            RemoveVerticalVelocity();
        }
        Rotation(horizontal, vertical);
    }

    private void AttackManagement()
    {
        if(!behaviourController.IsGrounded()) //빅뱅이 아닌 상황
        {
            return;
        }
        else
        {
            RotationManagment(behaviourController.Horizontal, behaviourController.Vertical);
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
            if (clicks == 1)
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

        //if (Input.GetButtonDown(ButtonKey.Attack))
        if (Input.GetAxisRaw(ButtonKey.Attack) != 0)  
        {
            StartCoroutine(Attack());
            //mouseLock = true;
            //behaviourController.myAnimator.SetBool(keyLock, mouseLock);
            //behaviourController.OverrideWithBehaviour(this);
            //behaviourController.LockTempBehaviour(behaviourCode);
            //lastClickedTime = Time.time;
            //clicks++;
            //if (clicks == 1)
            //{
            //    behaviourController.myAnimator.SetBool(attack1, true);
            //}
            //clicks = Mathf.Clamp(clicks, 0, 3);
        }

        //if(damageChecker.activeSelf)
        //{
        //    StiffenMonster();
        //}
        
    }

    public void AttackReturn1()
    {
        if(clicks >= 2)
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
        if (clicks >= 3)
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

    public void StiffenCheckStart()
    {
        damageChecker.SetActive(true);
    }

    public void StiffenCheckEnd()
    {
        damageChecker.SetActive(false);
    }

    //public void StiffenMonster(GameObject target = null)
    //{
    //    //타겟을 검사
    //    Collider[] colliders = playerAttackCollsion.CheckOverlapBox();
    //    foreach (Collider collider in colliders)
    //    {
    //        if(collider.gameObject.CompareTag("Monster"))
    //        {
    //            target = collider.gameObject;
    //        }
    //    }
    //    //타겟 상태 확인
    //    if (target)
    //    {
    //        Debug.Log("Test");
    //        //BossMove.instacne.currentHealthPoint -= damage[1];
    //        //if (BossMove.instacne.canStiffen)
    //        //{//경직 
    //        //    BossMove.instacne.Stiffen = true;
    //        //}
    //    }
    //}
    //적을 공격 했을때 콜리전 엔터를 통해서 공격 데미지를 넣어줄건지


}
