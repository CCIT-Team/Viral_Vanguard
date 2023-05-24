using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUIManager : MonoBehaviour
{
    public Image playerHealthPointImage;
    public Image playerStaminaImage;
    public Image playerKineticEnergyImage;

    public Text bossNameText;
    public Image bossHealthPointImage;
    public TextMeshProUGUI playerHealthPointText;
    public TextMeshProUGUI playerKineticEnergyText;

    //플레이어 정보
    public float playerMaxHealthPoint;
    public float playerCurrentHealthPoint;
    public float playerMaxStamina;
    public float playerCurrentStamina;
    public float playerMaxKineticEnergy;
    public float playerCurrentKineticEnergy;

    //보스 정보
    public string bossName;
    public float bossMaxHp;
    public float bossCurrentHp;

    void Start() => StatusInitaialzation();

    void StatusInitaialzation()
    {
        bossName = BossMove.instacne.bossName;
        bossMaxHp = BossMove.instacne.maxHealthPoint;
        bossCurrentHp = BossMove.instacne.currentHealthPoint;

        playerMaxHealthPoint = BehaviourController.instance.maxHealthPoint;
        playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        playerMaxStamina = BehaviourController.instance.maxStamina;
        playerCurrentStamina = BehaviourController.instance.currentStamina;
        playerMaxKineticEnergy = BehaviourController.instance.maxKineticEnergy;
        playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;

        BossUpdateHP();
        PlayerUpdateHP();
        PlayerUpdateStamina();
        PlayerUpdateKineticEnergy();
    }

    public void BossUpdateHP()
    {
        bossNameText.text = bossName;
        bossCurrentHp = BossMove.instacne.currentHealthPoint;
        bossHealthPointImage.fillAmount = bossCurrentHp / bossMaxHp;
    }

    public void PlayerUpdateHP()
    {
        playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        playerHealthPointImage.fillAmount = playerCurrentHealthPoint / playerMaxHealthPoint;
        playerHealthPointText.text = (playerHealthPointImage.fillAmount * 100).ToString();

    }

    public void PlayerUpdateStamina()
    {
        playerCurrentStamina = BehaviourController.instance.currentStamina;
        playerStaminaImage.fillAmount = playerCurrentStamina / playerMaxStamina;
    }

    public void PlayerUpdateKineticEnergy()
    {
        playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;
        playerKineticEnergyImage.fillAmount = playerCurrentKineticEnergy / playerMaxKineticEnergy;
        playerKineticEnergyText.text = (playerKineticEnergyImage.fillAmount * 100).ToString();
    }
}
