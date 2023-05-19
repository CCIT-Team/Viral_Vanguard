using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public Transform startPoint;

    Animator animator;

    bool chase = true;
    bool attack = false;

    public MonsterMovementSub attackRange;
    public MonsterMovementSub searchRange;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = this.transform;

        attackRange.mainmove = this;
        attackRange.rangeType = RangeType.Attack;

        searchRange.mainmove = this;
        searchRange.rangeType = RangeType.Search;
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!chase && other.CompareTag("MonsterPoint"))
        {
            animator.SetBool("FindPlayer", chase);
            chase = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MonsterSpawn"))
        {
            chase = false;
            target = startPoint;
        }
    }

    public void SetTarget(Transform transform)
    {
        if(chase)
        {
            target = transform;
            animator.SetBool("FindPlayer", chase);
        }
        
    }

    public void Attack()
    {
        if(!attack)
        {
            attack = true;
            animator.SetInteger("AttackType", Random.Range(0, 2));
            agent.isStopped = true;
            animator.SetTrigger("Attack");
        }
    }

    public void AttackDelay()
    {
        attack = false;
        agent.isStopped = false;
    }

    //안된거 = 돌아가면 기본모션으로 돌아옴
}
