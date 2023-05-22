using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [Header("���� ���� ����Ʈ")]
    public List<BossInformation> bossInfoSave = new List<BossInformation>();

    [Header("���� ���� �ǳ�")]
    public GameObject bossInfoPanel; 
    public Image bossImage;
    public Text bossNameText;
    public Text bossDescriptionText;

    public void PanelOpen(int i)
    {
        bossImage.sprite = bossInfoSave[i].bossSprite;
        bossNameText.text = bossInfoSave[i].bossName;
        bossDescriptionText.text = bossInfoSave[i].bossDescription;

        bossInfoPanel.SetActive(true);
    }

    public void PanelClose()
    {
        bossInfoPanel.SetActive(false);
    }
}

[System.Serializable]
public class BossInformation
{
    public string bossName;
    public Sprite bossSprite;
    [TextArea] public string bossDescription;
}