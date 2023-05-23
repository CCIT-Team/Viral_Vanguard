using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonArrays
{
    // ����� �����Ͱ� ���� ���
    public GameObject noSaveDataVisualizationElement; 
    // ����� �����Ͱ� �ִ� ���
    public GameObject saveDataVisualizationElement;

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
            buttonArrays.saveDataVisualizationElement.SetActive(true);
        }
        else if(!SaveDataManager.Instance.isSaveDataExist)
        {
            buttonArrays.noSaveDataVisualizationElement.SetActive(!true);
        }

        for(int i = 0; i < SaveDataManager.Instance.saveDataCount; i++)
        {
            buttonArrays.saveDataArrays[i].SetActive(true);
        }
    }
}
