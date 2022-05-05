using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Specific")]
    [SerializeField] protected CharacterObject characterObject;
    public CharacterObject CharacterObject => characterObject;

    [Space(15)]
    [ReadOnly, SerializeField] AffinityTypes affinityType;
    public AffinityTypes AffinityType => affinityType;
    [ReadOnly, SerializeField] List<DebugCharacterStat> debugCharacterStats;

    HashSet<PassiveObject> passives = new HashSet<PassiveObject>();
    public HashSet<PassiveObject> Passives => passives;
    protected CharacterStats stats;
    public CharacterStats Stats => stats;
    
    protected void InitCharacter(CharacterStats characterStats)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = characterObject.Sprite;
        GetComponentInChildren<Animator>().runtimeAnimatorController = characterObject.AnimatorController;

        SwitchAffinity(characterObject.AffinityType);

        foreach(PassiveObject passive in characterObject.StartingPassives)
        {
            AddPassive(passive);
        }

        debugCharacterStats = new List<DebugCharacterStat>();

        foreach(KeyValuePair<string, Stat> stat in stats.Stats)
        {
            debugCharacterStats.Add(new DebugCharacterStat(stat.Key, stat.Value.value));
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

    [System.Serializable]
    class DebugCharacterStat
    {
        [SerializeField] string statName;
        [SerializeField] float value;

        public DebugCharacterStat(string _statName, float _value)
        {
            statName = _statName;
            value = _value;
        }
    }
}
