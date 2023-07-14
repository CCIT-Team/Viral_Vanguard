using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour
{
    public Animator animator;
    public float patrolRange = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        this.transform.parent = null;
        StartCoroutine(Patrol());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == animator.gameObject && !animator.GetBool("FindPlayer"))
        {
            animator.SetBool("Patrol", false);
            StartCoroutine(Patrol());
        }    
    }

    IEnumerator Patrol()
    {
        yield return new WaitForSeconds(Random.Range(3f, 8));
        float x = Random.Range(-patrolRange, patrolRange);
        float z = Random.Range(-patrolRange, patrolRange);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(new Vector3(x, this.transform.localPosition.y, z), out hit, 1.0f, NavMesh.AllAreas) && Vector3.SqrMagnitude(this.transform.localPosition - new Vector3(x, this.transform.localPosition.y, z)) >= 0.4)
        {
            animator.SetBool("Patrol", true);
            this.transform.position += new Vector3(x, 0, z);
        }
        else
        {
            StartCoroutine(Patrol());
        }
    }
}
