using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    int attackType = 0;
    NewMonsterMovement monster;
    Collider col;

    private void Start()
    {
        monster = GetComponentInParent<NewMonsterMovement>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            monster.MonsterAttackCheck();
            monster.PlayerGuardHit(attackType);
        }
    }

    public void OnCollider(int type)
    {
        attackType = type;
        col.enabled = true;
    }

    public void OffCollider()
    {
        col.enabled = false;
    }
}
