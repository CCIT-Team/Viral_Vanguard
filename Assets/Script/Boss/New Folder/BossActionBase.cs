using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǰ� �� ���� �⺻ ��ȣ�ۿ� ��ũ��Ʈ
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

    //�ִϸ��̼� �̺�Ʈ��
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
