using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    // �� �̸����� �� ��ȯ
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // �񵿱������� �� �̸����� �� ��ȯ
    public static void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    // �߰����� ������ �� �̸����� �� ��ȯ
    public static void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // �񵿱������� �߰����� ������ �� �̸����� �� ��ȯ
    public static void LoadSceneAdditiveAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    // �� �ε����� �� ��ȯ
    public static void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // �񵿱������� �� �ε����� �� ��ȯ
    public static void LoadSceneByIndexAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    // �߰����� ������ �� �ε����� �� ��ȯ
    public static void LoadSceneAdditiveByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // �񵿱������� �߰����� ������ �� �ε����� �� ��ȯ
    public static void LoadSceneAdditiveByIndexAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }

    // �� ��ε�
    public static void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // ���� �� �����
    public static void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // ���� ����
    public static void QuitGame()
    {
        Application.Quit();
    }
}
