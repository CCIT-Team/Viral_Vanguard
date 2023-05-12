using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingManager : Singleton<GameSettingManager>
{
    // 게임 셋팅을 저장할 키(Key) 상수 정의
    private const string soundVolumeKey = "SoundVolume";
    private const string musicVolumeKey = "MusicVolume";
    private const string graphicsQualityKey = "GraphicsQuality";

    // 기본 게임 셋팅 값
    private const float defaultSoundVolume = 0.5f;
    private const float defaultMusicVolume = 0.5f;
    private const int defaultGraphicsQuality = 2;

    // 게임 셋팅을 로드하는 메서드
    public void LoadGameSettings()
    {
        // 사운드 볼륨 로드
        float soundVolume = PlayerPrefs.GetFloat(soundVolumeKey, defaultSoundVolume);
        SetSoundVolume(soundVolume);

        // 음악 볼륨 로드
        float musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, defaultMusicVolume);
        SetMusicVolume(musicVolume);

        // 그래픽 품질 로드
        int graphicsQuality = PlayerPrefs.GetInt(graphicsQualityKey, defaultGraphicsQuality);
        SetGraphicsQuality(graphicsQuality);
    }

    // 사운드 볼륨 설정 메서드
    public void SetSoundVolume(float volume)
    {
        // 사운드 볼륨 설정 코드 작성
        // 예시: AudioManager.Instance.SetSoundVolume(volume);

        // 게임 셋팅 저장
        PlayerPrefs.SetFloat(soundVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // 음악 볼륨 설정 메서드
    public void SetMusicVolume(float volume)
    {
        // 음악 볼륨 설정 코드 작성
        // 예시: AudioManager.Instance.SetMusicVolume(volume);

        // 게임 셋팅 저장
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // 그래픽 품질 설정 메서드
    public void SetGraphicsQuality(int qualityIndex)
    {
        // 게임 셋팅 저장
        PlayerPrefs.SetInt(graphicsQualityKey, qualityIndex);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        LoadGameSettings();
    }
}
