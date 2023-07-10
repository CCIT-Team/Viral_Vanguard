using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCheck : MonoBehaviour
{
    public BehaviourController behaviourController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossAttackCollider"))
        {
            Debug.Log("checkµÊ1");
            behaviourController.justGuardChecker = true;
            Debug.Log("checkµÊ2");
        }
        else
        {
            behaviourController.justGuardChecker = false;
        }
    }
}
