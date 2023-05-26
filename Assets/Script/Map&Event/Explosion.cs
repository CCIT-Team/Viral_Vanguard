using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject CarFire;

    void PlayExplosion()
    {
        explosion.Play();
    }

    void TurnOnFire()
    {
        CarFire.SetActive(true);
    }
}
