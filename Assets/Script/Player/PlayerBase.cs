using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CharacterController))]
public class PlayerBase : MonoBehaviour
{
    #region 변수
    private CharacterController characterController;
    private NavMeshAgent agent;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LayerMask targetMask;

    public Transform target;

    [SerializeField]
    private Transform hitPoint;

    [SerializeField]
    private Animator animator;

    readonly int moveHash = Animator.StringToHash("Move");
    readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");

    readonly int attackTriggerHash = Animator.StringToHash("AttackTrigger");
    readonly int attackIndexHash = Animator.StringToHash("AttackIndex");

    readonly int hitTriggerHash = Animator.StringToHash("HitTrigger");

    readonly int isAliveHash = Animator.StringToHash("IsAlive");
    #endregion

    #region 초기화X

    #endregion

    #region Unity Methods

    #endregion

    #region Main Methods
    void MoveMent()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);
        transform.position += move.x * transform.right + move.z * transform.forward;
        if (vertical > 0 || vertical < 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    #endregion
}
