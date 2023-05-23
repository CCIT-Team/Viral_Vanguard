using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : Singleton<SceneFader> 
{
    public Image fadeImage;

    private void Start() => DontDestroyOnLoad(fadeImage);

    public void StartFadeOut(string scenename)
    {
        StartCoroutine(FadeOut(3f, scenename));
    }
    public void StartFadeOut(int sceneindex)
    {
        StartCoroutine(FadeOut(3f, sceneindex));
    }
    public IEnumerator FadeIn(float time)
    {
        Color color = fadeImage.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / time;
            fadeImage.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOut<T>(float time,T parameter)
    {
        Color color = fadeImage.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            fadeImage.color = color;
            yield return null;
        }
        if (typeof(T) == typeof(int)) SceneController.LoadSceneByIndex((int)(object)parameter);
        else if (typeof(T) == typeof(string)) SceneController.LoadScene(parameter.ToString());
        color.a = 0f;
        fadeImage.color = color;
    }


    /// <summary>
    /// 아래 코드는 아직 사용 x
    /// 씬 전환시 데이터 초기화, 전달, 삭제 등의 목적으로 사용 예정
    /// </summary>

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        //여기에 씬을 넘기고 한번 실행해줘야 하는 걸 넣어주세요~
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
