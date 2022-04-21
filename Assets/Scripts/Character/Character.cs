using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterObject characterObject;
    public CharacterObject CharacterObject => characterObject;
    [ReadOnly, SerializeField] AffinityTypes affinityType;
    public AffinityTypes AffinityType => affinityType;
    HashSet<PassiveObject> passives = new HashSet<PassiveObject>();
    public HashSet<PassiveObject> Passives => passives;
    [SerializeField] protected bool testStats;
    protected CharacterStats stats;
    public CharacterStats Stats => stats;

    protected void InitCharacter(CharacterStats characterStats)
    {
        if(testStats)
        {
            characterStats.DebugTestStats();
        }  

        GetComponentInChildren<SpriteRenderer>().sprite = characterObject.Sprite;
        GetComponentInChildren<Animator>().runtimeAnimatorController = characterObject.AnimatorController;

        SwitchAffinity(characterObject.AffinityType);

        foreach(PassiveObject passive in characterObject.StartingPassives)
        {
            AddPassive(passive);
        }
    }

    public void SwitchAffinity(AffinityTypes type)
    {
        affinityType = type;
    }

    public bool AddPassive(PassiveObject passive)
    {
        bool added = passives.Add(passive);

        if(added && passive != null)
        {  
            passive.TakeAffect(stats);
        }
        
        return added;
    }

    public bool RemovePassive(PassiveObject passive)
    {
        bool removed = passives.Remove(passive);
        
        if(removed)
        {
            /////// implement removing passives
            passive.RemoveAffect(stats);
        }

        return removed;
    }

    
}
