using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    public static BossMove instacne;
    void Awake() => instacne = this;

    [Header("���� �������ͽ�")]
    public static string bossName;
    public float healthPoint;
    public float attackPoint;
    public float moveSpeed;
    public float closeAttackSpeed;
    public float longRangeAttackSpeed;

    enum BossBehavior { IDLE, TRACKING, MOVE, STUN, S_STUN, L_STUN }
    [Header("���� �ൿ")]
    [SerializeField] BossBehavior bossBehavior;

    [Header("�̵�")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Animator animator;

    [Header("���� ��Ÿ� Ȯ��")]
    [SerializeField] bool rangeNormalAttack1;
    [SerializeField] bool rangeNormalAttack2;
    [SerializeField] bool rangeActionAttack1;
    [SerializeField] bool rangeActionAttack1_1;
    [SerializeField] bool rangeSpecialAttack1;
    [SerializeField] bool rangeSpecialAttack2;
    [SerializeField] bool rangeSpecialAttack2_1;
    [SerializeField] bool rangeSpecialAttack3;
    [SerializeField] bool rangeSpecialAttack3_1;

    bool ready;
    bool Ready
    {
        set
        {
            ready = value;
            if (Ready) { NormalAttack(2); }
        }
        get { return ready; }
    }

    bool stiffen; //���� ������������?
    bool Stiffen
    {
        set
        {
            stiffen = value;
            animator.SetBool("Stiffen", value);
        }
    }
    bool canStiffen; //���� ���°� �� �� �ִ���?

    int AttackCount = 0;

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

        if (Input.GetKeyDown(KeyCode.S))
        {
            Ready = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StiffenStart();
        }
    }

    void TargetTracking(bool b)
    {
        agent.isStopped = b ? false : true;
        animator.SetBool("Walk", b);
    }

    void NormalAttack(int i)
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackType", i);
    }

    public void StiffenStart()
    {
        animator.SetBool("Stiffen", true);
        TargetTracking(false);
    }

    public void StiffenEnd()
    {
        animator.SetBool("Stiffen", false);
    }

    public void RangeCheck(DistanceDetection.DistanceType type, bool b)
    {
        switch(type)
        {
            case DistanceDetection.DistanceType.NORAMLATTACK1:
                rangeNormalAttack1 = b;
                break;
            case DistanceDetection.DistanceType.NORMALATTACK2:
                rangeNormalAttack2 = b;
                break;
            case DistanceDetection.DistanceType.ACTIONATTACK1:
                rangeActionAttack1 = b;
                break;
            case DistanceDetection.DistanceType.ACTIONATTACK1_1:
                rangeActionAttack1_1 = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK1:
                rangeSpecialAttack1 = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK2:
                rangeSpecialAttack2 = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK2_1:
                rangeSpecialAttack2_1 = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK3:
                rangeSpecialAttack3 = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK3_1:
                rangeSpecialAttack3_1 = b;
                break;
        }

        if (b) { }
    }

    void PlayerStiffen()
    {
        //if(player.canStiffen)
            //player.Stiffen = true;
    }

    void PlayerDamage(int damage)
    {
        //
    }
}
