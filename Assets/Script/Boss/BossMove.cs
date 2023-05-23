using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    public static BossMove instacne;
    void Awake() => instacne = this;

    IEnumerator update()
    {
        while(true)
        {
            agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    [Header("보스 스테이터스")]
    public static string bossName;
    public float maxHealthPoint;
    public float currentHealthPoint;
    public float attackPoint;
    public float moveSpeed;
    public float closeAttackSpeed;
    public float longRangeAttackSpeed;

    public float CurrentHealthPoint
    {
        set
        {
            currentHealthPoint = value;
        }
        get
        {
            return currentHealthPoint;
        }
    }

    [Header("이동")]
    [SerializeField] bool targetCheck;
    bool TargetCheck
    {
        set
        {
            targetCheck = value;
            StartTracking();
            bossBehavior.BehaviorStart(bossBehavior.GetRandomIndex());
        }
        get { return targetCheck; }
    }
    [SerializeField] NavMeshAgent agent;
    public Transform target;
    [SerializeField] Animator animator;

    [Header("공격 사거리 확인")]
    public BossBehavior bossBehavior;
    public BossRangeCheck[] rangeChecks;

    [Header("공격 데미지 세팅")]
    public float normalAttack1;
    public float normalAttack2;
    public float actionAttack1;
    public float actionAttack1_1;
    public float specialAttack1;
    public float specialAttack2;
    public float specialAttack2_1;
    public float specialAttack3;
    public float specialAttack3_1;

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

    bool stiffen;
    bool bigStiffen;
    public bool Stiffen
    {
        set
        {
            stiffen = value;
            animator.SetTrigger("Stiffen");
        }
        get
        {
            return stiffen;
        }
    }
    public bool BigStiffen
    {
        set
        {
            bigStiffen = value;
            animator.SetTrigger("BigStiffen");
        }
        get
        {
            return bigStiffen;
        }
    }
    public bool canStiffen;
    public bool canBigStiffen;

    public bool canAttack;
    int attackCount = 0;

    public GameObject[] attackDetections;

    public EventListener listener;

    void StartTracking()
    {
        StartCoroutine(update());
        animator.SetBool("Walk", true);
    }

    public void TargetTracking(bool b)
    {
        agent.isStopped = b ? false : true;
        animator.SetBool("Walk", b);
    }

    public void NextAction()
    {
        agent.isStopped = false;
        animator.SetBool("Walk", true);
        bossBehavior.BehaviorStart(bossBehavior.GetRandomIndex());
    }

    public void NormalAttack(int i)
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
        canStiffen = true;
        NextAction();
    }

    public void RangeCheck(DistanceDetection.DistanceType type, bool b)
    {
        switch(type)
        {
            case DistanceDetection.DistanceType.PLAYERCHECK:
                TargetCheck = b;
                break;
            case DistanceDetection.DistanceType.NORAMLATTACK1:
                rangeChecks[0].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.NORMALATTACK2:
                rangeChecks[1].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.ACTIONATTACK1:
                rangeChecks[2].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK1:
                rangeChecks[3].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK2:
                rangeChecks[4].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK3:
                rangeChecks[5].RangeCheck = b;
                break;
        }
    }

    public void PlayerStiffen()
    {
        BehaviourController.instance.Stiffen = true;
    }

    public bool CanAttack(int i)
    {
        return rangeChecks[i].RangeCheck;
    }

    public void ActiveCollider(int i)
    {
        bool b = attackDetections[i].activeSelf ? false : true;
        attackDetections[i].SetActive(b);
    }

    public void SetStiffen(int stiffenNum)
    {
        if (stiffenNum == 0 && canStiffen)
            Stiffen = true;
        else if (stiffenNum == 1 && canBigStiffen)
            BigStiffen = true;
    }

    public void CanJustGuard()
    {
        canBigStiffen = canBigStiffen ? false : true;
    }

    public void DoAttack()
    {
        BehaviourController.instance.MonsterAttack = true;
    }
}

[System.Serializable]
public class BossRangeCheck
{
    public string rangeName;
    public bool rangeCheck;
    public bool willDo;
    public int indexNum;

    public bool RangeCheck
    {
        set
        {
            rangeCheck = value;
            CanAction(indexNum);
        }
        get { return rangeCheck; }
    }

    public bool WillDo
    {
        set
        {
            willDo = value;
            CanAction(indexNum);
        }
        get { return willDo; }
    }

    public void CanAction(int i)
    {
        if (RangeCheck && WillDo)
            Action(i);
    }

    void Action(int i)
    {
        BossBehavior.instance.behaviorElemList[i].bossAction.Action();
        WillDo = false;
    }
}