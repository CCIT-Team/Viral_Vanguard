using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Image fillImage;

    public BossStatus boss1;
    public BossStatus boss2;
    public BossStatus boss3;

    public Text bossName;

    public BossStatus current;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            current = boss1;
            HpUpdate();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            current = boss2;
            HpUpdate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            current = boss3;
            HpUpdate();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            current.currentHP -= 10;
            HpUpdate();
        }
    }

    void HpUpdate()
    {
        bossName.text = current.BossName;
        fillImage.fillAmount = current.currentHP / current.maxHP;
    }
}