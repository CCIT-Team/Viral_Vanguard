using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField] BossMove boss;

    public enum BossAttackEnum { NORAMLATTACK1, NORMALATTACK2, ACTIONATTACK1, ACTIONATTACK1_1, SPECIALATTACK1, SPECIALATTACK2, SPECIALATTACK2_1, SPECIALATTACK3, SPECIALATTACK3_1 }
    public BossAttackEnum bossAttack;
    public float damage;
    public CapsuleCollider attackCollider;

    void Awake() => damage = GetDamage();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(BehaviourController.instance.guard)
                BehaviourController.instance.GuardHit = true;
            else if(BehaviourController.instance.justGuard)
                BehaviourController.instance.JustGuardSuccess = true;
            else
                BehaviourController.instance.Stiffen = true;
            BehaviourController.instance.HealthPoint -= damage;
            print(BehaviourController.instance.HealthPoint);
        }
    }

    float GetDamage()
    {
        float damage;

        switch(bossAttack)
        {
            case BossAttackEnum.NORAMLATTACK1:
                damage = boss.normalAttack1;
                break;
            case BossAttackEnum.NORMALATTACK2:
                damage = boss.normalAttack2;
                break;
            case BossAttackEnum.ACTIONATTACK1:
                damage = boss.actionAttack1;
                break;
            case BossAttackEnum.ACTIONATTACK1_1:
                damage = boss.actionAttack1_1;
                break;
            case BossAttackEnum.SPECIALATTACK1:
                damage = boss.specialAttack1;
                break;
            case BossAttackEnum.SPECIALATTACK2:
                damage = boss.specialAttack2;
                break;
            case BossAttackEnum.SPECIALATTACK2_1:
                damage = boss.specialAttack2_1;
                break;
            case BossAttackEnum.SPECIALATTACK3:
                damage = boss.specialAttack3;
                break;
            case BossAttackEnum.SPECIALATTACK3_1:
                damage = boss.specialAttack3_1;
                break;
            default:
                damage = 0;
                break;
        }

        return damage;
    }
}
