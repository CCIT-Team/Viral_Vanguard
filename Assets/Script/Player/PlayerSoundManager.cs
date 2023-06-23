using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    public void PlayerSoundOneShot(int index, Vector3 position)
    {
        audioSource.PlayOneShot(clips[index]); //audioSource.PlayClipAtPoint �̰ɷ� ���� ����
    }

    public void PlayerSoundReset()
    {
        audioSource.Pause();
        audioSource.Stop();
    }
}
