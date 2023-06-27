using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager1 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    public void PlayerSoundOneShot(int index, Vector3 position)
    {
        //audioSource.PlayOneShot(clips[index]); //audioSource.PlayClipAtPoint 이걸로 변경 예정
        AudioSource.PlayClipAtPoint(clips[index], position);
    }

    public void PlayerSoundReset()
    {
        audioSource.Pause();
        audioSource.Stop();
    }
}
