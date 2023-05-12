using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Stack<GameObject> uiStack = new Stack<GameObject>();   

    // 매개변수에는 해당 ui를 켰을 때 그걸 끄는 버튼을 할당해주세요
    // 값을 초기화 해줘야 하는 onClick Method를 포함하는!
    public void OpenUI(GameObject uiobject)
    {
        uiobject.SetActive(true);
        uiStack.Push(uiobject);
    }

    public void CloseUI(GameObject uiobject)
    {
        if(uiStack.Contains(uiobject))
        {
            uiStack.Pop();
            uiobject.SetActive(false);
        }
    }

    private void CloseLastUI()
    {
        if(uiStack.Count > 0)
        {
            GameObject lastUI = uiStack.Pop();
            lastUI.SetActive(false);

            Button button = lastUI.GetComponent<Button>();
            if (button == null) Debug.Log("버튼이 없어요");
            else if (button != null) button.onClick.Invoke();
        }
    }

    public void Update()
    {
        Debug.Log(uiStack.Count);
       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLastUI();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneController.LoadScene("Lobby");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneController.LoadScene("NikeMainRoad");
        }
    }

}
