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
    }

    private void Update()
    {
        transform.rotation = new Quaternion(0,0,0,0);
        throwMonster[0].AddForce(Vector3.right * throwforce);
        throwMonster[0].AddForce(Vector3.up * throwforce * 2);
        throwMonster[1].AddForce(Vector3.right * throwforce);
        throwMonster[1].AddForce(Vector3.up * throwforce * 2);
        throwMonster[2].AddForce(Vector3.right * throwforce);
        throwMonster[2].AddForce(Vector3.up * throwforce * 2);
    }

}
