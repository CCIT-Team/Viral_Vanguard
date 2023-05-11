using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    // 씬 이름으로 씬 전환
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 비동기적으로 씬 이름으로 씬 전환
    public static void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    // 추가적인 씬으로 씬 이름으로 씬 전환
    public static void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // 비동기적으로 추가적인 씬으로 씬 이름으로 씬 전환
    public static void LoadSceneAdditiveAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    // 씬 인덱스로 씬 전환
    public static void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // 비동기적으로 씬 인덱스로 씬 전환
    public static void LoadSceneByIndexAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    // 추가적인 씬으로 씬 인덱스로 씬 전환
    public static void LoadSceneAdditiveByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // 비동기적으로 추가적인 씬으로 씬 인덱스로 씬 전환
    public static void LoadSceneAdditiveByIndexAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }

    // 씬 언로드
    public static void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // 현재 씬 재시작
    public static void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 게임 종료
    public static void QuitGame()
    {
        Application.Quit();
    }
}
