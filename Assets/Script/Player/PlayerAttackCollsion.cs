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
            if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack1))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[0];
                behaviourController.currentKineticEnergy += increasekineticEnergy[0];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack2)&& !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[1];
                behaviourController.currentKineticEnergy += increasekineticEnergy[1];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[2];
                behaviourController.currentKineticEnergy += increasekineticEnergy[2];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
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
                //MonsterMovement.instance.HealthPoint -= damages[0];
                //MonsterMovement.instance.Stiffen = true;
                behaviourController.currentKineticEnergy += increasekineticEnergy[0];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack2) && !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                //MonsterMovement.instance.HealthPoint -= damages[1];
                //MonsterMovement.instance.Stiffen = true;
                behaviourController.currentKineticEnergy += increasekineticEnergy[1];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                //MonsterMovement.instance.HealthPoint -= damages[2];
                //MonsterMovement.instance.Stiffen = true;
                behaviourController.currentKineticEnergy += increasekineticEnergy[2];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
            else if (behaviourController.isBigBang)
            {
                MonsterMovement.instance.HealthPoint -= damages[3];
                MonsterMovement.instance.Stiffen = true;
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
            }
        }
    }
}
