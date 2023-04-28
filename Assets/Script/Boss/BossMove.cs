using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    enum BossBehavior { IDLE, TRACKING, MOVE, STUN, S_STUN, L_STUN }
    [Header("현재 행동")]
    [SerializeField] BossBehavior bossBehavior;

    [Header("이동")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Animator animator;
    bool noramlRange;
    bool inductionRange;
    bool specialRange;
    public bool NormalRange
    {
        set
        {
            noramlRange = value;
            TargetTracking(!NormalRange);
            if (NormalRange) { NormalAttack(0); }
        }
        get { return noramlRange; }
    }
    public bool InductionRange
    {
        set
        {
            inductionRange = value;
            TargetTracking(!InductionRange);
            if (InductionRange) { NormalAttack(1); }
        }
        get { return inductionRange; }
    }
    public bool SpecialRange
    {
        set
        {
            specialRange = value;
            TargetTracking(!SpecialRange);
            if (SpecialRange) { NormalAttack(2); }
        }
        get { return specialRange; }
    }

    bool ready;
    bool Ready
    {
        set
        {
            ready = value;
            if (Ready) { NormalAttack(2); }
        }
        get { return ready; }
    }

    int AttackCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetDestination(target.position);
            animator.SetBool("Walk", true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TargetTracking(agent.isStopped);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Ready = true;
        }
    }

    void TargetTracking(bool b)
    {
        agent.isStopped = b ? false : true;
        animator.SetBool("Walk", b);
    }

    void NormalAttack(int i)
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackType", i);
    }
}
