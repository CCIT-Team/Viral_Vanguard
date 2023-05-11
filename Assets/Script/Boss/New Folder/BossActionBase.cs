using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossActionBase : MonoBehaviour
{
    public Collider[] colliders;

    public virtual void AttackD1()
    {
        colliders[0].enabled =! colliders[0].enabled;
    }

    public virtual void AttackD2()
    {
        colliders[1].enabled =! colliders[1].enabled;
    }

    public virtual void AttackS1()
    {
        colliders[2].enabled =! colliders[2].enabled;
    }

    public virtual void AttackS2()
    {
        colliders[3].enabled =! colliders[3].enabled;
    }

    public virtual void AttackS3()
    {
        colliders[4].enabled =!colliders[4].enabled;
    }
}
