using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance {get; private set; }

    [Header("Player Specific")]
    [SerializeField] Sprite affinityNoneSprite; 
    SpriteRenderer affinitySprite;
    
    private void Awake()
    {
        Instance = this;
        
        stats = new PlayerStats(characterObject.BaseStats);
        affinitySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        passivesInterface = GameManager.Instance.InterfaceCanvas.transform.GetChild(2).GetComponent<PassivesInterface>();

        InitCharacter();
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
