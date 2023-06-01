using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingManager : Singleton<GameSettingManager>
{
    // ���� ������ ������ Ű(Key) ��� ����
    private const string soundVolumeKey = "SoundVolume";
    private const string musicVolumeKey = "MusicVolume";
    private const string graphicsQualityKey = "GraphicsQuality";

    // �⺻ ���� ���� ��
    private const float defaultSoundVolume = 0.5f;
    private const float defaultMusicVolume = 0.5f;
    private const int defaultGraphicsQuality = 2;

    // ���� ������ �ε��ϴ� �޼���
    public void LoadGameSettings()
    {
        // ���� ���� �ε�
        float soundVolume = PlayerPrefs.GetFloat(soundVolumeKey, defaultSoundVolume);
        SetSoundVolume(soundVolume);

        // ���� ���� �ε�
        float musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, defaultMusicVolume);
        SetMusicVolume(musicVolume);

        // �׷��� ǰ�� �ε�
        int graphicsQuality = PlayerPrefs.GetInt(graphicsQualityKey, defaultGraphicsQuality);
        SetGraphicsQuality(graphicsQuality);
    }

    // ���� ���� ���� �޼���
    public void SetSoundVolume(float volume)
    {
        // ���� ���� ���� �ڵ� �ۼ�
        // ����: AudioManager.Instance.SetSoundVolume(volume);

        // ���� ���� ����
        PlayerPrefs.SetFloat(soundVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // ���� ���� ���� �޼���
    public void SetMusicVolume(float volume)
    {
        // ���� ���� ���� �ڵ� �ۼ�
        // ����: AudioManager.Instance.SetMusicVolume(volume);

        // ���� ���� ����
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // �׷��� ǰ�� ���� �޼���
    public void SetGraphicsQuality(int qualityIndex)
    {
        // ���� ���� ����
        PlayerPrefs.SetInt(graphicsQualityKey, qualityIndex);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        LoadGameSettings();
    }
}
