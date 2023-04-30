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
            maxHP = current.maxHP;
            currentHP = boss1.currentHP;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentHP -= 10;
            HpUpdate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            current = boss1;
        }
    }

    void HpUpdate()
    {
        fillImage.fillAmount = currentHP / maxHP;
    }
}

//public class BossStatus
//{
//    public int maxHp;
//    public int currentHp;
//    public string name;
//}