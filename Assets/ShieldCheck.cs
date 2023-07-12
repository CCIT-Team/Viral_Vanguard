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
            behaviourController.justGuardChecker = true;
        }
        else
        {
            behaviourController.justGuardChecker = false;
        }
    }
}
