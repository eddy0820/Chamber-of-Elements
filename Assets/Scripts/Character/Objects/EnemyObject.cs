using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Characters/Enemy")]
public class EnemyObject : CharacterObject 
{
    [Header("Other")]

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] Vector3 position;
    public Vector3 Position => position;
    [SerializeField] FocusObject focus;
    public FocusObject Focus => focus;
    
    [Header("Hit Particles")]

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] GameObject hitParticle;
    public GameObject HitParticle => hitParticle;
    [SerializeField] Sprite hitParticleTexture;
    public Sprite HitParticleTexture => hitParticleTexture;

    [Header("Character Card Settings")]

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] Vector3 cardPosition;
    public Vector3 CardPosition => cardPosition;
    [SerializeField] Vector3 cardScale = Vector3.one;
    public Vector3 CardScale => cardScale;
}
