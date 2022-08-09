using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Characters/Enemy")]
public class EnemyObject : CharacterObject 
{
    [SerializeField] Vector3 position;
    public Vector3 Position => position;
    [SerializeField] FocusObject focus;
    public FocusObject Focus => focus;

    [Header("Hit Particles")]
    [SerializeField] GameObject hitParticle;
    public GameObject HitParticle => hitParticle;
    [SerializeField] Sprite hitParticleTexture;
    public Sprite HitParticleTexture => hitParticleTexture;
}
