using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollsion : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Test");
            //BossMove.instacne.currentHealthPoint -= damage[1];
            //if (BossMove.instacne.canStiffen)
            //{//경직 
            //    print("개느려");
            //    BossMove.instacne.Stiffen = true;
            //}
        }
    }
    //public Vector3 boxSize = new Vector3(3, 2, 2);

    //public Collider[] CheckOverlapBox()
    //{
    //    return Physics.OverlapBox(transform.position, boxSize * 0.5f, transform.rotation);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.matrix = transform.localToWorldMatrix;
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(Vector3.zero, boxSize);
    //}
}
