using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Redarms Behavior", menuName = "Scriptable Object/Redarms Behavior", order = int.MaxValue)]
public class RedarmsBehavior : ScriptableObject
{
    public enum BossBehaviorEnum { NORAMLATTACK1, NORMALATTACK2, NORMALATTACK3, ACTIONATTACK1, ACTIONATTACK1_1, SPECIALATTACK1, SPECIALATTACK2, SPECIALATTACK2_1, SPECIALATTACK3, SPECIALATTACK3_1 }
    public BossBehaviorEnum bossAttack;
    public RedarmsBehavior linkedBehavior;

    public delegate void BossActionDelegate();
    public BossActionDelegate BossAction;

    public void Action()
    {
        BossMove.instacne.TargetTracking(false);

        switch (bossAttack)
        {
            case BossBehaviorEnum.NORAMLATTACK1:
                BossAction = NormalAttack1;
                break;
            case BossBehaviorEnum.NORMALATTACK2:
                BossAction = NormalAttack2;
                break;
            case BossBehaviorEnum.NORMALATTACK3:
                BossAction = NormalAttack3;
                break;
            case BossBehaviorEnum.ACTIONATTACK1:
                BossAction = ActionAttack1;
                break;
            case BossBehaviorEnum.ACTIONATTACK1_1:
                BossAction = ActionAttack1_1;
                break;
            case BossBehaviorEnum.SPECIALATTACK1:
                BossAction = SpecialAttack1;
                break;
            case BossBehaviorEnum.SPECIALATTACK2:
                BossAction = SpecialAttack2;
                break;
            case BossBehaviorEnum.SPECIALATTACK2_1:
                BossAction = SpecialAttack2_1;
                break;
            case BossBehaviorEnum.SPECIALATTACK3:
                BossAction = SpecialAttack3;
                break;
            case BossBehaviorEnum.SPECIALATTACK3_1:
                BossAction = SpecialAttack3_1;
                break;
        }

        BossAction();
    }

    void NormalAttack1()
    {
        BossMove.instacne.NormalAttack(0);
    }

    void NormalAttack2()
    {
        BossMove.instacne.NormalAttack(1);
    }

    void NormalAttack3()
    {
        BossMove.instacne.NormalAttack(2);
    }

    void ActionAttack1()
    {
        BossMove.instacne.NormalAttack(3);
    }

    void ActionAttack1_1()
    {
        BossMove.instacne.NormalAttack(4);
    }

    void SpecialAttack1()
    {
        BossMove.instacne.NormalAttack(5);
    }

    void SpecialAttack2()
    {
        BossMove.instacne.NormalAttack(6);
    }

    void SpecialAttack2_1()
    {
        BossMove.instacne.NormalAttack(7);
    }

    void SpecialAttack3()
    {
        BossMove.instacne.NormalAttack(8);
    }

    void SpecialAttack3_1()
    {
        BossMove.instacne.NormalAttack(9);
    }

    void LinkedBehavior()
    {
        linkedBehavior.BossAction();
    }
}
