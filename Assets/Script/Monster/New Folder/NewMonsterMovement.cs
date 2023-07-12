using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NewMonsterMovement : MonoBehaviour
{
    //-------------------------------------------------------------
    //State 받아올 값들
    [Header("State")]
    public MonsterState monsterStat;
    

    EMonsterType type = EMonsterType.None;
    public EMonsterType MType
    {
        get { return type; }
        set
        {
            if (type == value)
                return;
            type = value;
            switch (type)
            {
                case EMonsterType.Normal:
                    searchRange.gameObject.SetActive(true);
                    detectRange.gameObject.SetActive(false);
                    patrolPoint.gameObject.SetActive(true);
                    target = patrolPoint;
                    StartCoroutine(SetAgent());
                    break;
                case EMonsterType.Hiding:
                    searchRange.gameObject.SetActive(false);
                    detectRange.gameObject.SetActive(false);
                    patrolPoint.gameObject.SetActive(false);
                    agent.isStopped = true;
                    attackRange.GetComponent<BoxCollider>().size += new Vector3(0.5f, 0, 0.85f);
                    break;
                case EMonsterType.Mimicking:
                    searchRange.gameObject.SetActive(false);
                    detectRange.gameObject.SetActive(true);
                    patrolPoint.gameObject.SetActive(false);
                    agent.isStopped = true;
                    animator.SetBool("FakeDead", true);
                    break;
            }
        }
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
            if (health <= 0 && State != EMonsterState.Dead)
            {
                State = EMonsterState.Dead;
            }
        }
    }
    float health;
    float[] damage = new float[3];

    //-------------------------------------------------------------
    //네비메쉬 관련

    [Space(10)]
    [Header("Nev")]
    public Transform patrolPoint;   //패트롤 다닐 위치
    NavMeshAgent agent;
    Transform target;               //메쉬 타겟
    Vector3 spawnPoint;           //전투 이탈시 복귀장소 
    

    //-------------------------------------------------------------
    //공격, 피격 관련
    [Header("Collisoin")]
    public MonsterRange attackRange;
    public MonsterAttack[] hitBox;
   
    bool isAttack = false;

    //-------------------------------------------------------------

    Animator animator;

    AudioSource audioSource;
    [Header("Sound")]
    [SerializeField]
    AudioClip[] sounds;
     
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
                    patrolPoint.gameObject.SetActive(false);
                    attackRange.enabled = false;
                    agent.isStopped = true;
                    if(ragdoll == null)
                    {
                        animator.SetTrigger("Die");
                        animator.SetBool("Dead", true);
                    }
                    else
                    {
                        DeadWithRagdoll();
                    }
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

    public GameObject ragdoll = null;

    public GameObject bodys;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Health -= 20;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            bodys.SetActive(false);
            ragdoll.SetActive(true);

        }
            
    }
    void Start()
    {
        StartSetting();
        GetMonsterState();
        StartCoroutine(IdleSound());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackCollsion"))
        {
            Stiffen = true;
            Health -= playerDamage[
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack1) ? 0 :
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack2) ? 1 :
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack3) ? 2 :
                                    3
                                  ];
            if (!audioSource.isPlaying)
                if (BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack3))
                    audioSource.PlayOneShot(sounds[5]);
                else
                    audioSource.PlayOneShot(sounds[Random.Range(3, 5)]);
        }
    }

//-------------------------------------------------------

    void GetMonsterState()
    {
        MType = monsterStat.type;
        Health = monsterStat.maxHealth;
        damage[0] = monsterStat.damage;
        damage[1] = monsterStat.damage * 0.9f;
        damage[2] = monsterStat.damage * 1.5f;
    }

    void StartSetting()
    {
        instance = this;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        spawnPoint = transform.position;
    }

    void TypeChange(int i)
    {
        
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

    public void Attack(int AType = -1)
    {
        if (!isAttack && State != EMonsterState.Dead)
        {
            isAttack = true;
            if(AType == -1)
                animator.SetInteger("AttackType", Random.Range(0, 2));
            agent.isStopped = true;
            audioSource.PlayOneShot(sounds[2]);
            animator.SetTrigger("Attack");
        }
    }

    public void UnMimic()
    {
        animator.SetBool("FakeDead", false);
        attackRange.gameObject.SetActive(true);
        searchRange.gameObject.SetActive(true);
        patrolPoint.gameObject.SetActive(true);
    }

    void DeadWithRagdoll()
    {
        animator.SetBool("Dead", true);
        StopCoroutine(IdleSound());
        audioSource.Stop();
        audioSource.PlayOneShot(sounds[0]);
        GetComponent<BoxCollider>().enabled = false;
        OffHitBox();
        animator.enabled = false;
        bodys.SetActive(false);
        ragdoll.SetActive(true);
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
        hitBox[direction].OnCollider(direction);
    }

    public void OffHitBox()
    {
        for(int i = 0; i< hitBox.Length; i++)
        {
            hitBox[i].OffCollider();
        }
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

    IEnumerator DelayMotion()
    {
        animator.SetBool("AttackDelayed", true);
        yield return new WaitForSecondsRealtime(0.6f);
        animator.SetBool("AttackDelayed", false);
    }

    IEnumerator IdleSound()
    {
        yield return new WaitForSeconds(Random.Range(5f,10f));
        if(!animator.GetBool("Dead"))
        {
            audioSource.PlayOneShot(sounds[1]);
            StartCoroutine(IdleSound());
        }
    }

    //---------------------------------------------------------

    public void PlayerStiffen(int direction)
    {
        if (direction == 1)
        {
            BehaviourController.instance.RightStiffen = true;
            BehaviourController.instance.HealthPoint -= damage[direction];
        }
        else
        {
            BehaviourController.instance.LeftStiffen = true;
            BehaviourController.instance.HealthPoint -= damage[direction];
        }
    }

    public void PlayerGuardHit(int direction)
    {
        if (BehaviourController.instance.JustGuard == true)
        {
            BehaviourController.instance.justGuardChecker = true;
            animator.SetBool("Stiffen", true);
            StartCoroutine(DelayMotion());
            BehaviourController.instance.JustGuard = true;
        }
        else if (BehaviourController.instance.guard == true)
        {
            if (BehaviourController.instance.currentStamina >= 0)
            {
                BehaviourController.instance.GuardHit = true;
                BehaviourController.instance.currentStamina -= damage[direction];
                BehaviourController.instance.camScript.CamShakeTime(0.1f, 0.02f);
            }
            else if (BehaviourController.instance.currentStamina <= damage[direction])
            {
                BehaviourController.instance.GuardBreak = true;
                BehaviourController.instance.HealthPoint -= damage[direction] - BehaviourController.instance.currentStamina;
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
