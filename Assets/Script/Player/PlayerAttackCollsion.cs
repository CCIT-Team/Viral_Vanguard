using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollsion : MonoBehaviour
{
    public BehaviourController behaviourController;
    public float[] damages;
    public float[] increasekineticEnergy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Test");
            if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack1))
            {
                //BossMove.instacne.currentHealthPoint -= damage[0];
                behaviourController.kineticEnergy += increasekineticEnergy[0];
                print("1");
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack2)&& !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                //BossMove.instacne.currentHealthPoint -= damage[1];
                behaviourController.kineticEnergy += increasekineticEnergy[1];
                print("2");

            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                //BossMove.instacne.currentHealthPoint -= damage[2];
                behaviourController.kineticEnergy += increasekineticEnergy[2];
                print("3");
            }
            //if (BossMove.instacne.canStiffen)
            //{//°æÁ÷ 
            //    print("1");
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
