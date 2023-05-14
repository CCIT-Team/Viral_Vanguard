using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//피격 등 보스 기본 상호작용 스크립트
public abstract class BossActionBase : MonoBehaviour
{
    public Collider[] colliders;
    HitBox hitbox;

    float bossHealth = 1000;
    public float BossHealth
    {
        get { return bossHealth; }
        set
        {
            bossHealth = value;
            EventManager.instance.PostNotification(EVENT_TYPE.eHealthChange, this, BossHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossHealth -= 20;
        }
    }

    //애니메이션 이벤트들
    public virtual void NormalAttack(int i = 0)
    {
        hitbox = colliders[i].GetComponent<HitBox>();
        hitbox.damage = i;
        colliders[i].enabled =! colliders[i].enabled;
    }

    public virtual void SpecialAttack()
    {
        colliders[1].enabled =! colliders[1].enabled;
    }

    public virtual void Attack()
    {
        colliders[2].enabled =!colliders[2].enabled;
    }
}
