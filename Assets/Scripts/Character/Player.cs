using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    SpriteRenderer affinitySprite;

    private void Awake()
    {
        stats = new PlayerStats(characterObject.BaseStats);
        affinitySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        InitCharacter(stats);
    }

    public void UpdateAffinitySprite(AffinityTypes type)
    {
        affinitySprite.sprite = GameManager.Instance.AffinityDatabase.GetAffinity[type].Sprite;
    }
}
