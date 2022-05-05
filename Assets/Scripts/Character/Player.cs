using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player Specific")]
    [SerializeField] Sprite affinityNoneSprite; 
    SpriteRenderer affinitySprite;
    
    private void Awake()
    {
        stats = new PlayerStats(characterObject.BaseStats);
        affinitySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        InitCharacter(stats);
    }

    public void UpdateAffinitySprite(AffinityTypes type)
    {
        if(type == AffinityTypes.None)
        {
            affinitySprite.sprite = affinityNoneSprite;
        }
        else
        {
            affinitySprite.sprite = GameManager.Instance.AffinityDatabase.GetAffinity[type].Sprite;
        }
    }
}
