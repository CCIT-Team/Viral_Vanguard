using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutScene : MonoBehaviour
{
    bool checkPlayer = false;
    bool eventend = false;
    Transform playerPos;

    public Animator bossAnimator;

    void Start()
    {
        playerPos = null;
    }

    
    void Update()
    {
        
    }

    void GetPlayerPosition(Transform player)
    {
        playerPos = player;
    }

    void PlayCutScene()
    {
        checkPlayer = true;
        bossAnimator.SetTrigger("Play");
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetPlayerPosition(other.transform);
            PlayCutScene();
        }
    }
}
