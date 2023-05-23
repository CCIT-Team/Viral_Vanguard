using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource[] sounds; // 효과음
    public AudioSource[] musics; // 배경음
}
