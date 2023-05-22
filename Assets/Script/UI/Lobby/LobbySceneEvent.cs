using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 해당 스크립트는 Virual_Vanguard 프로젝트의 Lobby Scene 에서만 동작 합니다.

public class LobbySceneEvent : MonoBehaviour
{
    public GameObject[] panels;
    public Sprite[] lobbyBackGrounds;
    public Image lobbyBackgroundImage;

    public void ChangeLobbyMenuImage(int backgroundimageNumber)
    {
        for(int i=0; i < panels.Length; i++) { panels[i].SetActive(false); }
        lobbyBackgroundImage.sprite = lobbyBackGrounds[backgroundimageNumber];
        panels[backgroundimageNumber].gameObject.SetActive(true);
    }

    private void Start()
    {
    }
}
