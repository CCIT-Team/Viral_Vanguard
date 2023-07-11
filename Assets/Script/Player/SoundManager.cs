using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioSource bgmAudioSource;
    public Sound[] bgmSound;
    public Sound[] sfxSound;

    public static SoundManager instance;
    public Dictionary<string, int> SFX = new Dictionary<string, int>();
    public Dictionary<string, int> BGM = new Dictionary<string, int>();
    public AudioMixer audioMixer;
    public Scrollbar masterScrollbar;
    public Scrollbar bgmScrollbar;
    public Scrollbar sfxScrollbar;

    void Awake() => Init();

    void Init()
    {
        instance = this;
        for (int i = 0; i < sfxSound.Length; i++)
        {
            SFX.Add(sfxSound[i].soundName, i);
        }
        for (int j = 0; j < bgmSound.Length; j++)
        {
            BGM.Add(bgmSound[j].soundName, j);
        }
        DontDestroyOnLoad(this);
    }

    public void OnShot(string SoundName)
    {
        if (instance.SFX.TryGetValue(SoundName, out int index))
            instance.sfxAudioSource.PlayOneShot(instance.sfxSound[index].audio);
    }

    public void PlayBGM(string SoundName)
    {
        if (instance.BGM.TryGetValue(SoundName, out int index))
            instance.bgmAudioSource.clip = instance.bgmSound[index].audio;
            instance.bgmAudioSource.Play();
    }

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterScrollbar.value) * 20);
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BgmVolume", Mathf.Log10(bgmScrollbar.value) * 20);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(sfxScrollbar.value) * 20);
    }

    public void PlayerSoundReset()
    {
        //audioSource.Pause();
        //audioSource.Stop();
    }
}

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip audio;
}