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
    /// �Ʒ� �ڵ�� ���� ��� x
    /// �� ��ȯ�� ������ �ʱ�ȭ, ����, ���� ���� �������� ��� ����
    /// </summary>

    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        //���⿡ ���� �ѱ�� �ѹ� ��������� �ϴ� �� �־��ּ���~
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
