using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollsion : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(3, 2, 2);

    public Collider[] CheckOverlapBox()
    {
        return Physics.OverlapBox(transform.position, boxSize * 0.5f, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
