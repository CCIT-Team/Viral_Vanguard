using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ش� ��ũ��Ʈ�� Virual_Vanguard ������Ʈ�� Lobby Scene ������ ���� �մϴ�.

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
