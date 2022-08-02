using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEarth : MonoBehaviour
{
    [SerializeField] Gradient gradient;
    ParticleSystem particle;
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        var color = particle.colorOverLifetime;
        color.color = gradient;  
    }
}
