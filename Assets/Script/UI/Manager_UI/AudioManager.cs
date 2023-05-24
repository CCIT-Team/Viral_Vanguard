using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;



    public void SetMusicVolume(float volume)
    {
        if(volume < -80f) volume = -80f;
        else if(volume > 20) volume = 20;
        musicMixer.audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        if (volume < -80f) volume = -80f;
        else if (volume > 20) volume = 20;
        musicMixer.audioMixer.SetFloat("SfxVolume", volume);
    }
}
