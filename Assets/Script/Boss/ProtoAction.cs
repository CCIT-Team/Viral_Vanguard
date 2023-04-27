using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class ProtoAction : MonoBehaviour, IListener
{
    Animator animator;
    NavMeshAgent agent;
    SphereCollider sphere;
    GameObject dummyTarget;
    GameObject target;

    public float testparameter = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.instance.AddListener(EVENT_TYPE.bFindPlayer, this);
        sphere = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        target = this.gameObject;
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);
        animator.SetFloat("Speed", Vector3.Magnitude(target.transform.position - transform.position)- testparameter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            dummyTarget = other.gameObject;
            EventManager.instance.PostNotification(EVENT_TYPE.bFindPlayer, this,other);
        }
    }


    public void OnEvent(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0));
        switch (EventType)
        {
            case EVENT_TYPE.bFindPlayer:
                Destroy(sphere);
                animator.SetTrigger("EnterBattle");
                return;
        }
    }

    void BattleStart()
    {
        target = dummyTarget;
    }
}



