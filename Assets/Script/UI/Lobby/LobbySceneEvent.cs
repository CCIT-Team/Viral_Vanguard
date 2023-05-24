using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

// �ش� ��ũ��Ʈ�� Virual_Vanguard ������Ʈ�� Lobby Scene ������ ���� �մϴ�.

public class LobbySceneEvent : MonoBehaviour
{
    public GameObject[] panels;
    public Sprite[] lobbyBackGrounds;
    public Image lobbyBackgroundImage;

    /// <summary>
    /// Operations Panel
    /// </summary>
    public MapSelect[] mapSelects;
    public GameObject[] targetBossInfo;
    

    public void ChangeLobbyMenuImage(int backgroundimageNumber)
    {
        for(int i=0; i < panels.Length; i++) { panels[i].SetActive(false); }
        //lobbyBackgroundImage.sprite = lobbyBackGrounds[backgroundimageNumber];
        panels[backgroundimageNumber].gameObject.SetActive(true);

        if(backgroundimageNumber != 2)
        for (int i = 0; i < mapSelects.Length; i++)
        {
            mapSelects[i].targetBossInfo[0].SetActive(false);
            mapSelects[i].dispatch.sprite = mapSelects[i].originalSprite;
            mapSelects[i].OnOff();
        }
    }

    public void Dispatch(ImageHover imagehover)
    {
        if(imagehover.enabled)
        {
            //���� ���� ����
            //

            SceneFader.Instance.StartFadeOut("NikeMainRoad");
        }
    }
}
