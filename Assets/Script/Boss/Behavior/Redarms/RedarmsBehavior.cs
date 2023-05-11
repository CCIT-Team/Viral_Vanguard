using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Redarms Behavior", menuName = "Scriptable Object/Redarms Behavior", order = int.MaxValue)]
public class RedarmsBehavior : ScriptableObject
{
    public enum BossBehaviorEnum { NORAMLATTACK1, NORMALATTACK2, ACTIONATTACK1, ACTIONATTACK1_1, SPECIALATTACK1, SPECIALATTACK2, SPECIALATTACK2_1, SPECIALATTACK3, SPECIALATTACK3_1 }
    public BossBehaviorEnum bossBehaivor;

    public delegate void BossActionDelegate();
    public BossActionDelegate BossAction;

    public void Action()
    {
        switch(bossBehaivor)
        {
            case BossBehaviorEnum.NORAMLATTACK1:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.NORMALATTACK2:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.ACTIONATTACK1:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.ACTIONATTACK1_1:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.SPECIALATTACK1:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.SPECIALATTACK2:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.SPECIALATTACK2_1:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.SPECIALATTACK3:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.SPECIALATTACK3_1:
                BossAction = NormalAttack1;
                break;
        }

        BossAction();
    }

    void NormalAttack1()
    {
        Debug.Log("노말공격1");
    }

    void NormalAttack2()
    {
        Debug.Log("노말공격2");
    }

    void ActionAttack1()
    {
        Debug.Log("액션 공격1");
    }

    void ActionAttack1_1()
    {
        Debug.Log("액션 공격1_1");
    }

    void SpecialAttack1()
    {
        Debug.Log("특수 공격1");
    }

    void SpecialAttack2()
    {
        Debug.Log("특수 공격2");
    }

    void SpecialAttack2_1()
    {
        Debug.Log("특수 공격2_1");
    }

    void SpecialAttack3()
    {
        Debug.Log("특수 공격3");
    }

    void SpecialAttack3_1()
    {
        Debug.Log("특수 공격3_1");
    }
}
