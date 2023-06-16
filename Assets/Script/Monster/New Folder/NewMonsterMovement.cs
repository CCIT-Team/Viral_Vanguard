using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NewMonsterMovement : MonoBehaviour
{
    //-------------------------------------------------------------

    //State 받아올 값들
    public MonsterState monsterStat;
    EMonsterType type;
    public EMonsterType MType
    {
        get { return type; }
        set { type = value; }
    }
    [HideInInspector]
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = value;
                if (health <= 0 && State != EMonsterState.Dead)
                {
                    State = EMonsterState.Dead;
                }
            }
        }
    }
    float health;
    float damage;

    //-------------------------------------------------------------

    //네비메쉬 관련
    NavMeshAgent agent;
    Transform target;               //메쉬 타겟
    Vector3 spawnPoint;           //전투 이탈시 복귀장소
    public Transform patrolPoint;   //패트롤 다닐 위치

    //-------------------------------------------------------------

    //공격, 피격 관련
    public MonsterAttack hitBox;
    public MonsterRange attackRange;
    bool isAttack = false;

    //-------------------------------------------------------------

    Animator animator;
     
    [HideInInspector]
    EMonsterState state = EMonsterState.Patrol;
    public EMonsterState State
    {
        get { return state; }
        set 
        {
            state = value;
            switch(state)
            {
                case EMonsterState.Patrol:
                    agent.isStopped = false;
                    animator.SetBool("Patrol", true);
                    target = patrolPoint;
                    break;
                case EMonsterState.Chase:
                    agent.isStopped = false;
                    animator.SetBool("Patrol", false);
                    animator.SetBool("FindPlayer", true);
                    break;
                case EMonsterState.Return:
                    animator.SetBool("FindPlayer", false);
                    patrolPoint.position = spawnPoint;
                    Health = monsterStat.maxHealth;
                    State = EMonsterState.Patrol;
                    break;
                case EMonsterState.Dead:
                    attackRange.enabled = false;
                    agent.isStopped = true;
                    animator.SetTrigger("Die");
                    animator.SetBool("Dead", true);
                    break;
            }
        }
    }

    [HideInInspector]
    public static NewMonsterMovement instance;
    float[] playerDamage = { 5, 6, 13, 100 };

    public bool Stiffen
    {
        get { return stiffen; }
        set
        {
            stiffen = value;
            animator.SetBool("Stiffen", true);
            blood[Random.Range(0, 2)].Play();
            if (value && State != EMonsterState.Dead)
            {
                agent.isStopped = true;
            }
        }
    }
    bool stiffen;

    public MonsterRange detectRange;

    public MonsterRange searchRange;

    public ParticleSystem[] blood;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Health -= 20;
    }
    void Start()
    {
        GetMonsterState();
        StartSetting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackCollsion"))
        {
            Stiffen = true;
            health -= playerDamage[
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack1) ? 0 :
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack2) ? 1 :
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack3) ? 2 :
                                    3
                                 ];
        }
    }

//-------------------------------------------------------

    void GetMonsterState()
    {
        type = monsterStat.type;
        Health = monsterStat.maxHealth;
        damage = monsterStat.damage;
    }

    void StartSetting()
    {
        instance = this;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        spawnPoint = transform.position;
        TypeChange((int)type);

    }

    void TypeChange(int i)
    {
        type = (EMonsterType)i;
        switch (type)
        {
            case EMonsterType.Normal:
                patrolPoint.gameObject.SetActive(true);
                target = patrolPoint;
                StartCoroutine(SetAgent());
                break;
            case EMonsterType.Crawling:
                break;
            case EMonsterType.Hiding:
                agent.isStopped = true;
                break;
            case EMonsterType.Mimicking:
                agent.isStopped = true;
                animator.SetBool("FakeDead", true);
                break;
        }
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
        if (transform.CompareTag("Player"))
        {
            animator.SetBool("Patrol", false);
            animator.SetBool("FindPlayer", true);
        }

    }

    public void Attack(int AType = 0)
    {
        if (!isAttack && State != EMonsterState.Dead)
        {
            isAttack = true;
            animator.SetInteger("AttackType", Random.Range(0, 2));
            agent.isStopped = true;
            animator.SetTrigger("Attack");
        }
    }

    public void UnMimic()
    {
        animator.SetBool("FakeDead", false);
        patrolPoint.gameObject.SetActive(true);
    }

    //---------------------------------------------

    //애니메이션 이벤트
    public void AttackDelay()
    {
        isAttack = false;
        agent.isStopped = false;
    }

    public void MonsterDead()
    {
        gameObject.SetActive(false);
    }

    public void OnHitBox(int direction)
    {
        hitBox.OnCollider(direction);
    }

    public void OffHitBox()
    {
        hitBox.OffCollider();
    }

    void OffStiffen()
    {
        stiffen = false;
        animator.SetBool("Stiffen", false);
        agent.isStopped = false;
    }

    void OnAgent()
    {
        agent.isStopped = false;
        StartCoroutine(SetAgent());
    }

    //---------------------------------------------------------

    IEnumerator SetAgent()
    {
        while(true)
        {
            yield return 0;
            agent.SetDestination(target.position);
        }
        
    }

    //---------------------------------------------------------

    public void PlayerStiffen(int direction)
    {
        if (direction == 1)
        {
            BehaviourController.instance.RightStiffen = true;
            BehaviourController.instance.HealthPoint -= damage;
        }
        else
        {
            BehaviourController.instance.LeftStiffen = true;
            BehaviourController.instance.HealthPoint -= damage;
        }
    }

    public void PlayerGuardHit(int direction)
    {
        if (BehaviourController.instance.JustGuard == true)
        {

        }
        else if (BehaviourController.instance.guard == true)
        {
            if (BehaviourController.instance.currentStamina >= 0)
            {
                BehaviourController.instance.GuardHit = true;
                BehaviourController.instance.currentStamina -= damage;
                BehaviourController.instance.camScript.CamShakeTime(0.1f, 0.02f);
            }
            else if (BehaviourController.instance.currentStamina <= damage)
            {
                BehaviourController.instance.GuardBreak = true;
                BehaviourController.instance.HealthPoint -= damage - BehaviourController.instance.currentStamina;
                BehaviourController.instance.currentStamina = 0;
            }
        }
        else
            PlayerStiffen(direction);
    }

    public void MonsterAttackCheck()
    {
        BehaviourController.instance.MonsterAttack = !BehaviourController.instance.MonsterAttack;
    }
}
