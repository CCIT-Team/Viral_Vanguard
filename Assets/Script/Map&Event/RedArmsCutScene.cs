using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RedArmsCutScene : MonoBehaviour
{
    public Animator redArmsAnimator;
    public CinemachineImpulseSource impulseSource;
    public float moveSpeed;
    
    void Update()
    {
        MoveArms();
    }

    void MoveArms()
    {
        redArmsAnimator.applyRootMotion = true;
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        redArmsAnimator.SetTrigger("Q");
        moveSpeed = 0;
        other.gameObject.SetActive(false);
    }

    void Inpulse()
    {
        impulseSource.GenerateImpulse();
    }

    void Inpulseglow()
    {
        impulseSource.m_DefaultVelocity = new Vector3(-0.5f, 0.55f, -0.5f);
        impulseSource.GenerateImpulse();
    }
}
