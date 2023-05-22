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

                if (BossMove.instacne.canStiffen)
                {
                    BossMove.instacne.SetStiffen(0, BossMove.AttackDirection.RIGHT); //r = 0, l = 1, center = 2, bigbang = 3 // none, right, Left
                }
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack2)&& !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                BossMove.instacne.currentHealthPoint -= damages[1];
                behaviourController.kineticEnergy += increasekineticEnergy[1];

                if (BossMove.instacne.canStiffen)
                { 
                    BossMove.instacne.SetStiffen(1, BossMove.AttackDirection.LEFT); 
                }
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                BossMove.instacne.currentHealthPoint -= damages[2];
                behaviourController.kineticEnergy += increasekineticEnergy[2];

                if (BossMove.instacne.canStiffen)
                {
                    BossMove.instacne.SetStiffen(2, BossMove.AttackDirection.NONE); 
                }
            }
            else if(behaviourController.isBigBang)
            {
                BossMove.instacne.currentHealthPoint -= damages[3];

                if (BossMove.instacne.canStiffen)
                {
                    print("1");
                    BossMove.instacne.SetStiffen(3, BossMove.AttackDirection.NONE); 
                }
            }
        }
    }
}
