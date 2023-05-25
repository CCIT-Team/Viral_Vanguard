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

<<<<<<< HEAD
=======
    float t = 0;
    Coroutine bossHealthCoroutine = null;
    Coroutine playerHealthCoroutine = null;
    Coroutine playerKineticCoroutine = null;
    Coroutine playerSteminaCoroutine = null;

    public GameObject clearAnimationObject;
    public GameObject failAnimationObject;

>>>>>>> feature/UI
    void Start() => StatusInitaialzation();

    void StatusInitaialzation()
    {
<<<<<<< HEAD
        bossName = BossMove.instacne.bossName;
        bossMaxHp = BossMove.instacne.maxHealthPoint;
        bossCurrentHp = BossMove.instacne.currentHealthPoint;

        playerMaxHealthPoint = BehaviourController.instance.maxHealthPoint;
        playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        playerMaxStamina = BehaviourController.instance.maxStamina;
        playerCurrentStamina = BehaviourController.instance.currentStamina;
        playerMaxKineticEnergy = BehaviourController.instance.maxKineticEnergy;
        playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;
=======
        //bossName = BossMove.instacne.bossName;
        //bossMaxHp = BossMove.instacne.maxHealthPoint;
        //bossCurrentHp = BossMove.instacne.currentHealthPoint;

        //playerMaxHealthPoint = BehaviourController.instance.maxHealthPoint;
        //playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        //playerMaxStamina = BehaviourController.instance.maxStamina;
        //playerCurrentStamina = BehaviourController.instance.currentStamina;
        //playerMaxKineticEnergy = BehaviourController.instance.maxKineticEnergy;
        //playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;
>>>>>>> feature/UI

        BossUpdateHP();
        PlayerUpdateHP();
        PlayerUpdateStamina();
        PlayerUpdateKineticEnergy();
    }

    public void BossUpdateHP()
    {
        bossNameText.text = bossName;
<<<<<<< HEAD
        bossCurrentHp = BossMove.instacne.currentHealthPoint;
        bossHealthPointImage.fillAmount = bossCurrentHp / bossMaxHp;
    }

    public void PlayerUpdateHP()
    {
        playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
=======

        float beforeAttackHP = bossCurrentHp;
        float afterAttackHP;
        float lerpPocket;
        //bossCurrentHp = BossMove.instacne.currentHealthPoint;
        afterAttackHP = bossCurrentHp;
    }

    public void PlayerUpdateHP()
    {
        float before = playerCurrentHealthPoint;
        float after;
        //playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        after = playerCurrentHealthPoint;
>>>>>>> feature/UI
        playerHealthPointImage.fillAmount = playerCurrentHealthPoint / playerMaxHealthPoint;
        playerHealthPointText.text = (playerHealthPointImage.fillAmount * 100).ToString();

    }

    public void PlayerUpdateStamina()
    {
<<<<<<< HEAD
        playerCurrentStamina = BehaviourController.instance.currentStamina;
=======
        float before = playerCurrentStamina;
        float after;
        //playerCurrentStamina = BehaviourController.instance.currentStamina;
        after = playerCurrentStamina;
>>>>>>> feature/UI
        playerStaminaImage.fillAmount = playerCurrentStamina / playerMaxStamina;
    }

    public void PlayerUpdateKineticEnergy()
    {
<<<<<<< HEAD
        playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;
=======
        float before = playerCurrentKineticEnergy;
        float after;
        //playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;
        after = playerCurrentKineticEnergy;
>>>>>>> feature/UI
        playerKineticEnergyImage.fillAmount = playerCurrentKineticEnergy / playerMaxKineticEnergy;
        playerKineticEnergyText.text = (playerKineticEnergyImage.fillAmount * 100).ToString();
    }

    void StartLerp(Coroutine runningCoroutine, IEnumerator coroutine)
    {
        if(runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(coroutine);
    }

    IEnumerator GaugeLerp(float before, float after, Image fillImage, float maxValue, float t)
    {
        float lerpPocket = Mathf.Lerp(before, after, t);
        fillImage.fillAmount = lerpPocket / maxValue;

        yield return new WaitForSeconds(0.1f);

        if (t < 0)
        {
            t += 0.1f;
            StartCoroutine(GaugeLerp(before, after, fillImage, maxValue, t));
        }
        else if (t > 1)
        {
            t = 0;
            StopAllCoroutines();
        }
    }
    
    public void BossClearAnimation()
    {
        clearAnimationObject.SetActive(true);
    }

    public void BossFailAnimation()
    {
        failAnimationObject.SetActive(true);
    }
}