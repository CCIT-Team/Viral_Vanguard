using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager1 : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioSource bgmAudioSource;
    public AudioClip[] clips;
    public Sound[] bgmSound;
    public Sound[] sfxSound;

    public void PlayerSoundOneShot(int index, Vector3 position)
    {
        //audioSource.PlayOneShot(clips[index]); //audioSource.PlayClipAtPoint 이걸로 변경 예정
        //AudioSource.PlayClipAtPoint(sfxClips[index], position);
    }

    public static SoundManager1 instance;
    public Dictionary<string, int> SFX = new Dictionary<string, int>();
    public Dictionary<string, int> BGM = new Dictionary<string, int>();
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

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

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
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