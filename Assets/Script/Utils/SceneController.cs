using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{

    // �� �̸����� �� ��ȯ
    public static void LoadScene(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneName);
    }

    // �񵿱������� �� �̸����� �� ��ȯ
    public static void LoadSceneAsync(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneName);
    }

    // �߰����� ������ �� �̸����� �� ��ȯ
    public static void LoadSceneAdditive(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // �񵿱������� �߰����� ������ �� �̸����� �� ��ȯ
    public static void LoadSceneAdditiveAsync(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    // �� �ε����� �� ��ȯ
    public static void LoadSceneByIndex(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneIndex);
    }

    // �񵿱������� �� �ε����� �� ��ȯ
    public static void LoadSceneByIndexAsync(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    // �߰����� ������ �� �ε����� �� ��ȯ
    public static void LoadSceneAdditiveByIndex(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // �񵿱������� �߰����� ������ �� �ε����� �� ��ȯ
    public static void LoadSceneAdditiveByIndexAsync(int sceneIndex)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }

    // �� ��ε�
    public static void UnloadScene(string sceneName)
    {
        UIManager.Instance.uiStack.Clear();
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // ���� �� �����
    public static void RestartScene()
    {
        UIManager.Instance.uiStack.Clear();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // ���� ����
    public static void QuitGame()
    {
        UIManager.Instance.uiStack.Clear();
        Application.Quit();
    }
}
