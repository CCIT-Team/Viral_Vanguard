using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonArrays
{
    // 저장된 데이터가 없는 경우
    public GameObject[] noSaveDataVisualizationElements; 
    // 저장된 데이터가 있는 경우
    public GameObject[] saveDataVisualizationElements;

    public GameObject[] saveDataArrays;
    public GameObject[] saveDataCheckMark;
}

public class MainSceneEvent : MonoBehaviour
{
    public ButtonArrays buttonArrays = new ButtonArrays();

    public void Start()
    {
        // 외부에서 Awake로 세이브 데이터를 로드하므로 순서중요!
        if (SaveDataManager.Instance.isSaveDataExist)
        {
            for (int i = 0; i < buttonArrays.saveDataVisualizationElements.Length; i++)
                buttonArrays.saveDataVisualizationElements[i].SetActive(true);
        }
        else if(!SaveDataManager.Instance.isSaveDataExist)
        {
            for (int i = 0; i < buttonArrays.noSaveDataVisualizationElements.Length; i++)
                buttonArrays.noSaveDataVisualizationElements[i].SetActive(true);
        }

        for(int i = 0; i < SaveDataManager.Instance.saveDataCount; i++)
        {
            buttonArrays.saveDataArrays[i].SetActive(true);
        }
    }

    public void Update()
    {
        
    }
}
