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
                behaviourController.particleSystems[6].Play();
                if(behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if(behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }    
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack2)&& !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[1];
                behaviourController.currentKineticEnergy += increasekineticEnergy[1];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                behaviourController.particleSystems[7].Play();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
            else if(behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                BossMove.instacne.CurrentHealthPoint -= damages[2];
                behaviourController.currentKineticEnergy += increasekineticEnergy[2];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                behaviourController.particleSystems[7].Play();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
            else if(behaviourController.isBigBang)
            {
                BossMove.instacne.CurrentHealthPoint -= damages[3];
                BossMove.instacne.SetStiffen(1);
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
        }
        else if(other.gameObject.CompareTag("NormalMonster"))
        {
            if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack1))
            {
                behaviourController.currentKineticEnergy += increasekineticEnergy[0];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
            else if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack2) && !behaviourController.myAnimator.GetBool(AnimatorKey.Attack3))
            {
                behaviourController.currentKineticEnergy += increasekineticEnergy[1];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
            else if (behaviourController.myAnimator.GetBool(AnimatorKey.Attack3) && behaviourController.myAnimator.GetBool(AnimatorKey.Attack2))
            {
                behaviourController.currentKineticEnergy += increasekineticEnergy[2];
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
            else if (behaviourController.isBigBang)
            {
                behaviourController.stageUIManager.PlayerUpdateKineticEnergy();
                if (behaviourController.currentKineticEnergy >= 50)
                {
                    behaviourController.gameObjectsEffects[0].SetActive(true);
                }
                else if (behaviourController.currentKineticEnergy >= 100)
                {
                    behaviourController.gameObjectsEffects[1].SetActive(true);
                }
                else
                {
                    behaviourController.gameObjectsEffects[0].SetActive(false);
                    behaviourController.gameObjectsEffects[1].SetActive(false);
                }
            }
        }
    }
}
