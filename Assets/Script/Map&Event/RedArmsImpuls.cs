using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//�ִϸ��̼� �����Դϴ�.
public class RedArmsImpuls : MonoBehaviour
{
    public CinemachineImpulseSource cs;

    void WalkImpulse()
    {
        cs.m_DefaultVelocity = new Vector3(0, 1, 0);
        cs.m_ImpulseDefinition.m_ImpulseDuration = 0.5f;
        cs.GenerateImpulse();
    }

    void GrowlImpulse()
    {
        cs.m_DefaultVelocity = new Vector3(0, -1, 0);
        cs.m_ImpulseDefinition.m_ImpulseDuration = 2.75f;
        cs.GenerateImpulse();
    }
}
