using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonArrays
{
    public GameObject mainMenuSlotFirstPlay;
    public GameObject mainMenuSlotNonFirstPlay;


    // 저장된 데이터가 없는 경우
    public GameObject noSaveDataVisualizationElement; 
    // 저장된 데이터가 있는 경우
    public GameObject saveDataVisualizationElement;

    public GameObject[] saveDataArrays;
}

public class TitleSceneEvent : MonoBehaviour
{
    public ButtonArrays buttonArrays = new ButtonArrays();


    public void Start()
    {
        // 외부에서 Awake로 세이브 데이터를 로드하므로 순서중요!
        if (SaveDataManager.Instance.isSaveDataExist)
        {
            buttonArrays.noSaveDataVisualizationElement.SetActive(false);
            buttonArrays.mainMenuSlotFirstPlay.SetActive(false);

            buttonArrays.saveDataVisualizationElement.SetActive(true);
            buttonArrays.mainMenuSlotNonFirstPlay.SetActive(true);
        }
        else if(!SaveDataManager.Instance.isSaveDataExist)
        {
            buttonArrays.saveDataVisualizationElement.SetActive(false);
            buttonArrays.mainMenuSlotNonFirstPlay.SetActive(false);

            buttonArrays.noSaveDataVisualizationElement.SetActive(true);
            buttonArrays.mainMenuSlotFirstPlay.SetActive(true);
        }

        for(int i = 0; i < SaveDataManager.Instance.saveDataCount; i++)
        {
            buttonArrays.saveDataArrays[i].SetActive(true);
        }
    }
}
