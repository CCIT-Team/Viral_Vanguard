using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMonster : MonoBehaviour
{
    public float throwforce;
    public Rigidbody[] throwMonster = new Rigidbody[3];

    void Start()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        throwMonster[0].AddForce(Vector3.right * throwforce * 3);
        throwMonster[0].AddForce(Vector3.up * throwforce);
        throwMonster[1].AddForce(Vector3.right * throwforce * 3);
        throwMonster[1].AddForce(Vector3.up * throwforce);
        throwMonster[2].AddForce(Vector3.right * throwforce * 3);
        throwMonster[2].AddForce(Vector3.up * throwforce);
    }
}
