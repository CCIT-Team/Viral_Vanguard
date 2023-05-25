using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUIManager : MonoBehaviour
{
    public GameObject BossStatusObject;

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
    Coroutine bossHpCoroutine;
    Coroutine playerHpCoroutine;
    Coroutine playerSpCoroutine;
    Coroutine playerKpCoroutine;
    public float bossHpT;
    public float playerHpT;
    public float playerSpT;
    public float playerKpT;

    void Start() => StatusInitaialzation();

    void StatusInitaialzation()
    {
        playerMaxHealthPoint = BehaviourController.instance.maxHealthPoint;
        playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        playerMaxStamina = BehaviourController.instance.maxStamina;
        playerCurrentStamina = BehaviourController.instance.currentStamina;
        playerMaxKineticEnergy = BehaviourController.instance.maxKineticEnergy;
        playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;

        PlayerUpdateHP();
        PlayerUpdateStamina();
        PlayerUpdateKineticEnergy();
    }

    public void BossStatisInitailzation()
    {
        BossStatusObject.SetActive(true);

        bossName = BossMove.instacne.bossName;
        bossMaxHp = BossMove.instacne.maxHealthPoint;
        bossCurrentHp = BossMove.instacne.currentHealthPoint;

        BossUpdateHP();
    }

    public void BossUpdateHP()
    {
        bossNameText.text = bossName;

        float before = bossCurrentHp;
        bossCurrentHp = BossMove.instacne.currentHealthPoint;
        float after = bossCurrentHp;

        if (bossHpCoroutine != null)
        {
            bossHpT = 0;
            StopCoroutine(bossHpCoroutine);
        }
        bossHpCoroutine = StartCoroutine(StartLerp(BossMove.instacne, bossHpCoroutine, bossHealthPointImage, before, after, bossMaxHp, bossHpT));
    }

    public void PlayerUpdateHP()
    {
        float before = playerCurrentHealthPoint;
        playerCurrentHealthPoint = BehaviourController.instance.currentHealthPoint;
        float after = playerCurrentHealthPoint;

        if (playerHpCoroutine != null)
        {
            playerHpT = 0;
            StopCoroutine(playerHpCoroutine);
        }
        playerHpCoroutine = StartCoroutine(StartHpLerp(BehaviourController.instance, playerHpCoroutine, playerHealthPointImage, before, after, playerMaxHealthPoint, playerHpT, playerHealthPointText));
    }

    public void PlayerUpdateStamina()
    {
        playerCurrentStamina = BehaviourController.instance.currentStamina;
        playerStaminaImage.fillAmount = playerCurrentStamina / playerMaxStamina;

        float before = playerCurrentStamina;
        playerCurrentStamina = BehaviourController.instance.currentStamina;
        float after = playerCurrentStamina;

        if (playerSpCoroutine != null)
        {
            playerSpT = 0;
            StopCoroutine(playerSpCoroutine);
        }
        playerSpCoroutine = StartCoroutine(StartLerp(BehaviourController.instance, playerSpCoroutine, playerStaminaImage, before, after, playerMaxStamina, playerSpT));
    }

    public void PlayerUpdateKineticEnergy()
    {
        float before = playerCurrentKineticEnergy;
        playerCurrentKineticEnergy = BehaviourController.instance.currentKineticEnergy;
        float after = playerCurrentKineticEnergy;

        if (playerKpCoroutine != null)
        {
            playerKpT = 0;
            StopCoroutine(playerKpCoroutine);
        }

        playerKpCoroutine = StartCoroutine(StartKpLerp(BehaviourController.instance, playerKpCoroutine, playerKineticEnergyImage, before, after, playerMaxKineticEnergy, playerKpT, playerKineticEnergyText));
    }

    #region 게이지 코루틴
    IEnumerator StartLerp(BossMove boss, Coroutine couroutine, Image fill, float before, float after, float max, float t)
    {
        t += 0.1f;
        fill.fillAmount = Mathf.SmoothStep(before, after, t) / max;
        yield return new WaitForSeconds(0.01f);

        if(t < 1 && after != boss.currentHealthPoint)
        {
            before = fill.fillAmount * max;
            after = boss.currentHealthPoint;
            t = 0;

            couroutine = StartCoroutine(StartLerp(boss, couroutine, fill, before, after, max, t));
        }
        else if(t < 1 && after == BossMove.instacne.currentHealthPoint)
        {
            couroutine = StartCoroutine(StartLerp(boss, couroutine, fill, before, after, max, t));
        }
    }

    IEnumerator StartLerp(BehaviourController player, Coroutine couroutine, Image fill, float before, float after, float max, float t)
    {
        t += 0.1f;
        fill.fillAmount = Mathf.SmoothStep(before, after, t) / max;

        yield return new WaitForSeconds(0.01f);

        if (t < 1 && after != player.currentStamina)
        {
            before = fill.fillAmount * max;
            after = player.currentStamina;
            t = 0;

            couroutine = StartCoroutine(StartLerp(player, couroutine, fill, before, after, max, t));
        }
        else if (t < 1 && after == player.currentStamina)
        {
            couroutine = StartCoroutine(StartLerp(player, couroutine, fill, before, after, max, t));
        }
    }

    IEnumerator StartHpLerp(BehaviourController player, Coroutine couroutine, Image fill, float before, float after, float max, float t, TextMeshProUGUI text)
    {
        t += 0.1f;
        fill.fillAmount = Mathf.SmoothStep(before, after, t) / max;
        yield return new WaitForSeconds(0.01f);

        if (t < 1 && after != player.currentHealthPoint)
        {
            before = fill.fillAmount * max;
            after = player.currentHealthPoint;
            t = 0;

            couroutine = StartCoroutine(StartHpLerp(player, couroutine, fill, before, after, max, t, text));
        }
        else if (t < 1 && after == player.currentHealthPoint)
        {
            couroutine = StartCoroutine(StartHpLerp(player, couroutine, fill, before, after, max, t, text));
        }

        int value = (int)Mathf.Round(after / max * 100);
        text.text = value.ToString();
    }

    IEnumerator StartKpLerp(BehaviourController player, Coroutine couroutine, Image fill, float before, float after, float max, float t, TextMeshProUGUI text)
    {
        t += 0.1f;
        fill.fillAmount = Mathf.SmoothStep(before, after, t) / max;

        yield return new WaitForSeconds(0.01f);

        if (t < 1 && after != player.currentKineticEnergy)
        {
            before = fill.fillAmount * max;
            after = player.currentKineticEnergy;
            t = 0;

            couroutine = StartCoroutine(StartKpLerp(player, couroutine, fill, before, after, max, t, text));
        }
        else if (t < 1 && after == player.currentKineticEnergy)
        {
            couroutine = StartCoroutine(StartKpLerp(player, couroutine, fill, before, after, max, t, text));
        }

        int value = (int)Mathf.Round(after / max * 100);
        text.text = value.ToString();
    }
    #endregion
}

class GaugeValue
{
    public float max;
    public float current;
}