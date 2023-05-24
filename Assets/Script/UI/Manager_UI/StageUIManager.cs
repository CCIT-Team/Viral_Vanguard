using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{
    public Image playerHealthPointImage;
    public Image playerStaminaImage;
    public Image playerKineticEnergyImage;

    public Text bossNameText;
    public Image bossHealthPointImage;

    //플레이어 정보
    float playerMaxHealthPoint;
    float playerCurrentHealthPoint;
    float playerMaxStamina;
    float playerCurrentStamina;
    float playerMaxKineticEnergy;
    float playerCurrentKineticEnergy;

    //보스 정보
    string bossName;
    float bossMaxHp;
    float bossCurrentHp;

    void BossInitaialzation ()
    {
        bossName = BossMove.instacne.bossName;
        bossMaxHp = BossMove.instacne.maxHealthPoint;
        bossCurrentHp = BossMove.instacne.currentHealthPoint;
    }

    void BossUpdateHP()
    {
        bossNameText.text = bossName;
        bossHealthPointImage.fillAmount = bossCurrentHp / bossMaxHp;
    }

    void PlayerInitaialzation()
    {
        // 플레이어로 교체 예정
        playerMaxHealthPoint = BossMove.instacne.maxHealthPoint;
        playerCurrentHealthPoint = BossMove.instacne.currentHealthPoint;
        playerMaxStamina = BossMove.instacne.currentHealthPoint;
        playerCurrentStamina = BossMove.instacne.currentHealthPoint;
        playerMaxKineticEnergy = BossMove.instacne.currentHealthPoint;
        playerCurrentKineticEnergy = BossMove.instacne.currentHealthPoint;
    }

    public void PlayerUpdateHP()
    {
        playerHealthPointImage.fillAmount = playerCurrentHealthPoint / playerMaxHealthPoint;
    }

    public void PlayerUpdateStamina()
    {
        playerStaminaImage.fillAmount = playerCurrentStamina / playerMaxStamina;
    }

    public void PlayerUpdateKineticEnergy()
    {
        playerKineticEnergyImage.fillAmount = playerCurrentKineticEnergy / playerMaxKineticEnergy;
    }
}
