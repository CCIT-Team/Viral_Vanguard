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

    /// <summary>
    /// 
    /// </summary>
    public GameObject[] operatorCall;


    public void Start()
    {
        if (!SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isLobbyTutorialClear)
            operatorCall[0].SetActive(true);
        if (!SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isStateTutorialClear)
            operatorCall[1].SetActive(true);
        if (!SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isOperatorTutorialClear)
            operatorCall[2].SetActive(true);
    }


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

    public void TutorialClear(int index)
    {
        if (index == 0)
            SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isLobbyTutorialClear = true;
        else if(index == 1)
            SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isStateTutorialClear = true;
        else if(index == 2)
            SaveDataManager.Instance.saveDatas[SaveDataManager.Instance.currentSaveFileIndex].isOperatorTutorialClear = true;

    }
}
