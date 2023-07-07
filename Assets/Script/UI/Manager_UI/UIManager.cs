using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : Singleton<UIManager>
{
    // Option
    private bool isApplySoundSetting = false;
    public Scrollbar masterVolume;
    public Scrollbar bgmVolume;
    public Scrollbar sfxVolume;
    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI bgmVolumeText;
    public TextMeshProUGUI sfxVolumeText;
    public GameObject optionCanvas;


    public Stack<GameObject> uiStack = new Stack<GameObject>();   


    // Close UI
    // �Ű��������� �ش� ui�� ���� �� �װ� ���� ��ư�� �Ҵ����ּ���
    // ���� �ʱ�ȭ ����� �ϴ� onClick Method�� �����ϴ�!
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
            
            if(uiobject.name == "OptionUI" && isApplySoundSetting == false)
            {
                masterVolume.value = mv;
                bgmVolume.value = bv;
                sfxVolume.value = sv;
            }

            isApplySoundSetting = false; // Option Setting Initialization
        }
    }

    private void CloseLastUI()
    {
        if(uiStack.Count > 0)
        {
            GameObject lastUI = uiStack.Pop();
            Debug.Log(lastUI.name);
            lastUI.SetActive(false);

            Button button = lastUI.GetComponent<Button>();
            if (button == null) Debug.Log("��ư�� �����");
            else if (button != null) button.onClick.Invoke();

            if (lastUI.name == "OptionUI" && isApplySoundSetting == false)
            {
                masterVolume.value = mv;
                bgmVolume.value = bv;
                sfxVolume.value = sv;
            }
        }

        isApplySoundSetting = false;// Option Setting Initialization
    }


    // SoundOption
    float mv = 0, bv = 0, sv = 0;
    public void GetSoundVolumeValue()
    {
        isApplySoundSetting = false;

        mv = masterVolume.value;
        bv = bgmVolume.value;
        sv = sfxVolume.value;
    }

    public void SetSoundVolumeValue()
    {
        isApplySoundSetting = true;
    }

    private void Start() => DontDestroyOnLoad(optionCanvas);

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLastUI();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneFader.Instance.StartFadeOut("Lobby",3f);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneFader.Instance.StartFadeOut("NikeMainRoad",3f);
        }

        if (masterVolume.value <= 0) masterVolume.value = 0.001f;
        if (bgmVolume.value <= 0) bgmVolume.value = 0.001f;
        if (sfxVolume.value <= 0) sfxVolume.value = 0.001f;

        masterVolumeText.text = Mathf.Round(masterVolume.value * 100).ToString();
        bgmVolumeText.text = Mathf.Round(bgmVolume.value * 100).ToString();
        sfxVolumeText.text = Mathf.Round(sfxVolume.value * 100).ToString();
    }

}
