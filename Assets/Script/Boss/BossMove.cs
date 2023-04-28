using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Animator animator;

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

        if (agent.velocity.sqrMagnitude <= 0 * 0.2f && agent.remainingDistance <= agent.stoppingDistance)
        {
            print("³¡!");
        }
    }

    void TargetTracking(bool b)
    {
        agent.isStopped = b ? false : true;
        animator.SetBool("Walk", b);
    }
}
