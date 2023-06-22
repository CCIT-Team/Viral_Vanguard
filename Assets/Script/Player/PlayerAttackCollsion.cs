using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollsion : MonoBehaviour
{
    public BehaviourController behaviourController;
    public float[] damages;
    public float[] increasekineticEnergy;
    public void OnTriggerEnter(Collider other)
    {
        if (behaviourController.currentKineticEnergy >= 100)
        {
            behaviourController.currentKineticEnergy = 100f;
        }
        if (other.gameObject.CompareTag("Monster"))
        {
            if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack1))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[0];
                behaviourController.currentKineticEnergy += increasekineticEnergy[0];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                behaviourController.particleSystems[6].Play();   
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack2)&& !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[1];
                behaviourController.currentKineticEnergy += increasekineticEnergy[1];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                behaviourController.particleSystems[7].Play();
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[2];
                behaviourController.currentKineticEnergy += increasekineticEnergy[2];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                behaviourController.particleSystems[7].Play();
            }
            else if(behaviourController.isBigBang)
            {
                BossMove.instacne.CurrentHealthPoint -= damages[3];
                BossMove.instacne.SetStiffen(1);
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
        }
        else if(other.gameObject.CompareTag("NormalMonster"))
        {
            if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack1))
            {
                behaviourController.currentKineticEnergy += increasekineticEnergy[0];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack2) && !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                behaviourController.currentKineticEnergy += increasekineticEnergy[1];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                behaviourController.currentKineticEnergy += increasekineticEnergy[2];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if (behaviourController.isBigBang)
            {
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
        }
    }
}
