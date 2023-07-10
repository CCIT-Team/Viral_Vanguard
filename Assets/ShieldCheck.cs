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
            Debug.Log("check��1");
            behaviourController.justGuardChecker = true;
            Debug.Log("check��2");
        }
        else
        {
            behaviourController.justGuardChecker = false;
        }
    }
}
