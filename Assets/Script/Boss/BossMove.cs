using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class BossMove : MonoBehaviour
{
    public static BossMove instacne;

    void Awake() => instacne = this;
    void Start()
    {
        stageUIManager.BossStatisInitailzation();
    }

    IEnumerator update()
    {
        while(true)
        {
            agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    [Header("보스 스테이터스")]
    public string bossName;
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
            stageUIManager.BossUpdateHP();
            if (CurrentHealthPoint <= 0)
                BossDead();
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
    public float forwardDetectAngle = 35f;

    [Header("사거리 확인")]
    public BossBehavior bossBehavior;
    public BossRangeCheck[] rangeChecks;

    [Header("공격 데미지 세팅")]
    public float normalAttack1;
    public float normalAttack2;
    public float normalAttack3;
    public float actionAttack1;
    public float actionAttack1_1;
    public float specialAttack1;
    public float specialAttack2;
    public float specialAttack2_1;
    public float specialAttack3;
    public float specialAttack3_1;

    bool stiffen;
    public bool bigStiffen;
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
    bool doingAttack;

    public GameObject[] attackDetections;

    public EventListener listener;

    public StageUIManager stageUIManager;

    public Collider bodyCollider;

    [Header("이펙트 리스트")]
    public ParticleSystem[] particleSystems;
    public VisualEffect[] visualEffects;
    public GameObject[] effectObject;

    [Header("이펙트 위치")]
    public GameObject effectParent;
    public GameObject leftHand;

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
        doingAttack = false;
        agent.isStopped = false;
        animator.SetBool("Walk", true);

        if (DotProduct(PlayerDirection(target.position, transform.position), transform.forward) > forwardDetectAngle)
            StartCoroutine("LookAtPlayer");
        else
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
            case DistanceDetection.DistanceType.NORMALATTACK3:
                rangeChecks[2].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.ACTIONATTACK1:
                rangeChecks[3].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK1:
                rangeChecks[4].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK2:
                rangeChecks[5].RangeCheck = b;
                break;
            case DistanceDetection.DistanceType.SPECIALATTACK3:
                rangeChecks[6].RangeCheck = b;
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
        OffAttackCollision();

        if (stiffenNum == 0 && canStiffen)
            Stiffen = true;
        else if (stiffenNum == 1)
            BigStiffen = true;
    }

    void OffAttackCollision()
    {
        foreach (var col in attackDetections)
        {
            col.SetActive(false);
        }
    }

    public void CanJustGuard()
    {
        canBigStiffen = canBigStiffen ? false : true;
    }

    public void DoAttack()
    {
        BehaviourController.instance.MonsterAttack = BehaviourController.instance.MonsterAttack ? false : true;
    }

    public void StartDash(float speed)
    {
        StartCoroutine("Dash", speed);
    }

    public void EndDash()
    {
        StopCoroutine("Dash");
    }

    IEnumerator Dash(float speed)
    {
        transform.Translate(Vector3.forward * speed);

        yield return new WaitForSeconds(0.01f);

        StartCoroutine("Dash", speed);
    }

    public void BossDead()
    {
        bodyCollider.enabled = false;
        agent.baseOffset = 0;
        animator.SetTrigger("Dead");
    }

    #region 플레이어 방향으로 돌기

    Quaternion targetRotation;
    float t = 0;

    Vector3 PlayerDirection(Vector3 playerPos, Vector3 bossPos)
    {
        Vector3 playerDirection = playerPos - bossPos;
        return playerDirection;
    }

    Quaternion TargetRoation()
    {
        return Quaternion.LookRotation(target.position, transform.position);
    }

    public void DoingAttack()
    {
        doingAttack = doingAttack ? false : true;
    }

    void StartRotation()
    {
        targetRotation = TargetRoation();
        t = 0;
        StartCoroutine("LookAtPlayer");
    }

    IEnumerator LookAtPlayer()
    {
        if(targetRotation != TargetRoation())
        {
            targetRotation = TargetRoation();
            t = 0;
        }

        t += 0.01f;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);

        yield return new WaitForSeconds(0.01f);

        if (DotProduct(PlayerDirection(target.position, transform.position), transform.forward) > forwardDetectAngle)
            StartCoroutine("LookAtPlayer");
        else
        {
            NextAction();
            StopCoroutine("LookAtPlayer");
        }    
    }

    float DotProduct(Vector3 targetDir, Vector3 foward)
    {
        float dot = Vector3.Dot(targetDir, foward);

        if (dot > 1.0f)
            dot = 1.0f;
        else if (dot < -1.0f)
            dot = -1.0f;

        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        return angle;
    }
    #endregion

    #region 이펙트
    public void OnVisualEffectObject(int i)
    {
        visualEffects[i].gameObject.SetActive(true);
    }

    public void OnParticleObject(int i)
    {
        particleSystems[i].gameObject.SetActive(true);
    }

    public void OnEffectObject(int i)
    {
        bool b = effectObject[i].activeSelf ? false : true;

        effectObject[i].SetActive(b);
    }
    #endregion
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