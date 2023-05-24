using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RedArmsCutScene : MonoBehaviour
{
    public Animator redArmsAnimator;
    public CinemachineImpulseSource impulseSource;
    public CinemachineImpulseSource impulseSourceGrowl;
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
        impulseSourceGrowl.m_DefaultVelocity = new Vector3(0, 1, 0);
        impulseSourceGrowl.m_ImpulseDefinition.m_ImpulseDuration = 2.75f;
        impulseSourceGrowl.GenerateImpulse();
    }
}
