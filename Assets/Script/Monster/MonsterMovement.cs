using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public static MonsterMovement instance;

    NavMeshAgent agent;
    Transform target;
    public Transform startPoint;
    private bool stiffen;
    public float monsterDamage = 5;

    Animator animator;

    bool chase = true;
    bool attack = false;

    public MonsterMovementSub attackRange;
    public MonsterMovementSub searchRange;
    public MonsterAttack HitBox;

    public float maxHealth = 50;
    float healthPoint = 50;

    public float patrolRange = 5;

    public ParticleSystem[] blood;

    float[] damage = { 5, 6, 13, 100 };
    bool isDead = false;

    public bool Stiffen
    {
        get { return stiffen; }
        set
        {
            stiffen = value;

            animator.SetBool("Stiffen", true);
            blood[Random.Range(0, 2)].Play();
            if(value && !isDead)
            {
                agent.isStopped = true;
            }
        }
    }
    public float HealthPoint
    {
        get
        {
            return healthPoint;
        }
        set
        {
            healthPoint = value;
            if(healthPoint <= 0&&!isDead)
            {
                this.enabled = false;
                attackRange.enabled = false;
                agent.enabled = false;
                isDead = true;
                animator.SetTrigger("Die");
                animator.SetBool("Dead",isDead);
            }
        }
    }
    void Start()
    {
        healthPoint = maxHealth;
        instance = this;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = startPoint;

        attackRange.mainmove = this;
        attackRange.rangeType = RangeType.Attack;

        searchRange.mainmove = this;
        searchRange.rangeType = RangeType.Search;

        StartCoroutine("Patrol");
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MonsterPoint"))
        {
            if(Vector3.SqrMagnitude(this.transform.position - startPoint.position) <= 1)
            {
                if (!chase)
                {
                    animator.SetBool("FindPlayer", chase);
                    chase = true;
                }
                animator.SetBool("Patrol", false);
                StartCoroutine("Patrol");
            }
        }
        else if(other.CompareTag("PlayerAttackCollsion"))
        {
            Stiffen = true;
            HealthPoint -= damage[
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack1) ? 0 :
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack2) ? 1 :
                                    BehaviourController.instance.myAnimator.GetBool(AnimatorKey.Attack3) ? 2 :
                                    3
                                 ];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MonsterSpawn"))
        {
            chase = false;
            StopCoroutine("Patrol");
            target = startPoint;
        }
    }

    public void SetTarget(Transform transform)
    {
        if(chase)
        {
            target = transform;
            animator.SetBool("FindPlayer", chase);
        }
    }

    public void Attack()
    {
        if(!attack&&!isDead)
        {
            attack = true;
            animator.SetInteger("AttackType", Random.Range(0, 2));
            agent.isStopped = true;
            animator.SetTrigger("Attack");
        }
    }

    public void AttackDelay()
    {
        attack = false;
        agent.isStopped = false;
    }

    IEnumerator Patrol()
    {
        yield return new WaitForSeconds(Random.Range(8f, 15));
        animator.SetBool("Patrol", true);
        float x = Random.Range(-patrolRange * 0.6f, patrolRange * 0.6f);
        float z = Random.Range(-patrolRange*0.6f, patrolRange * 0.6f);
        //startPoint.position = startPoint.position + new Vector3(x, 0, z);
        startPoint.localPosition = new Vector3(x, 0, z);
    }

    public void MonsterDead()
    {
        gameObject.SetActive(false);
    }

    public void PlayerStiffen(int direction)
    {
        if(direction == 1)
        {
            BehaviourController.instance.RightStiffen = true;
            BehaviourController.instance.HealthPoint -= monsterDamage;
        }
        else
        {
            BehaviourController.instance.LeftStiffen = true;
            BehaviourController.instance.HealthPoint -= monsterDamage;
        }

        //가드가 아닐시 
        //데미지 넣기
    }

    public void PlayerGuardHit(int direction)
    {
        if(BehaviourController.instance.JustGuard == true)
        {

        }
        else if (BehaviourController.instance.guard == true)
        {
            if (BehaviourController.instance.currentStamina >= 0)
            {
                BehaviourController.instance.GuardHit = true;
                BehaviourController.instance.currentStamina -= monsterDamage;
                BehaviourController.instance.camScript.CamShakeTime(0.1f, 0.02f);
            }
            else if (BehaviourController.instance.currentStamina <= monsterDamage)
            {
                BehaviourController.instance.GuardBreak = true;
                BehaviourController.instance.HealthPoint -= monsterDamage - BehaviourController.instance.currentStamina;
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

    public void OnHitBox(int direction)
    {
        HitBox.OnCollider(direction);
    }

    public void OffHitBox()
    {
        HitBox.OffCollider();
    }

    void StiffenOff()
    {
        stiffen = false;
        animator.SetBool("Stiffen", false);
    }
}
