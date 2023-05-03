using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Image fillImage;
    public float maxHP;
    public float currentHP;

    public BossStatus boss1;
    public BossStatus boss2;
    public BossStatus boss3;

    public BossStatus current;
   
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            current = boss1;
            maxHP = current.maxHP;
            currentHP = current.currentHP;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            current = boss2;
            maxHP = current.maxHP;
            currentHP = current.currentHP;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            current = boss3;
            maxHP = current.maxHP;
            currentHP = current.currentHP;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentHP -= 10;
            current.currentHP = currentHP;
            HpUpdate();
        }
    }

    void HpUpdate()
    {
        fillImage.fillAmount = currentHP / maxHP;
    }
}