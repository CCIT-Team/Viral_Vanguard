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

    #endregion
}
