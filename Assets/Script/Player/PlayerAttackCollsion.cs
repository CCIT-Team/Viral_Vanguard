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
                BossMove.instacne.currentHealthPoint -= damages[0];
                behaviourController.kineticEnergy += increasekineticEnergy[0];
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack2)&& !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                BossMove.instacne.currentHealthPoint -= damages[1];
                behaviourController.kineticEnergy += increasekineticEnergy[1];
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                BossMove.instacne.currentHealthPoint -= damages[2];
                behaviourController.kineticEnergy += increasekineticEnergy[2];
            }
            else if(behaviourController.isBigBang)
            {
                BossMove.instacne.currentHealthPoint -= damages[3];

                if (BossMove.instacne.canStiffen)
                {
                    BossMove.instacne.SetStiffen(1);
                }
            }
        }
    }
}
