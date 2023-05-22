using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{

    // 씬 이름으로 씬 전환
    public static void LoadScene(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneName);
    }

    // 비동기적으로 씬 이름으로 씬 전환
    public static void LoadSceneAsync(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneName);
    }

    // 추가적인 씬으로 씬 이름으로 씬 전환
    public static void LoadSceneAdditive(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // 비동기적으로 추가적인 씬으로 씬 이름으로 씬 전환
    public static void LoadSceneAdditiveAsync(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    // 씬 인덱스로 씬 전환
    public static void LoadSceneByIndex(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneIndex);
    }

    // 비동기적으로 씬 인덱스로 씬 전환
    public static void LoadSceneByIndexAsync(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    // 추가적인 씬으로 씬 인덱스로 씬 전환
    public static void LoadSceneAdditiveByIndex(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // 비동기적으로 추가적인 씬으로 씬 인덱스로 씬 전환
    public static void LoadSceneAdditiveByIndexAsync(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }

    // 씬 언로드
    public static void UnloadScene(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // 현재 씬 재시작
    public static void RestartScene()
    {
        UIManager.Instance.uiStack.Clear();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 게임 종료
    public static void QuitGame()
    {
        UIManager.Instance.uiStack.Clear();
        Application.Quit();
    }
}
