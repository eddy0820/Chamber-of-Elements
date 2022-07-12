using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance {get; private set; }

    [Header("Player Specific")]
    [SerializeField] Sprite affinityNoneSprite; 
    SpriteRenderer affinitySprite;

    [ReadOnly] bool minionExists;
    public bool MinionExists => minionExists;

    Minion minion;
    public Minion Minion => minion;
    
    private void Awake()
    {
        Instance = this;
        
        stats = new PlayerStats(characterObject.BaseStats);
        affinitySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        InitCharacter();
        ChangeAttacker(GameManager.Instance.Enemy);

        minion = GameObject.FindWithTag("Minion").GetComponent<Minion>();
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

    public void SetMinionExists(bool exists)
    {
        minionExists = exists;
    }
}
