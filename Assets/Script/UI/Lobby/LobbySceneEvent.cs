using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

// 해당 스크립트는 Virual_Vanguard 프로젝트의 Lobby Scene 에서만 동작 합니다.

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
            //보스 정보 전달
            //

            SceneFader.Instance.StartFadeOut("NikeMainRoad");
        }
    }
}
