using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonArrays
{
    // ����� �����Ͱ� ���� ���
    public GameObject[] noSaveDataVisualizationElements; 
    // ����� �����Ͱ� �ִ� ���
    public GameObject[] saveDataVisualizationElements;

    public GameObject[] saveDataArrays;
}

public class TitleSceneEvent : MonoBehaviour
{
    public ButtonArrays buttonArrays = new ButtonArrays();

    public void Start()
    {
        // �ܺο��� Awake�� ���̺� �����͸� �ε��ϹǷ� �����߿�!
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
}