using System;
using System.Linq;
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

    [Space(15)]
    [SerializeField] GameObject damageIndicatorPrefab;
    public GameObject DamageIndicatorPrefab => damageIndicatorPrefab;

    HashSet<PassiveObject> passives = new HashSet<PassiveObject>();
    public HashSet<PassiveObject> Passives => passives;
    protected PassivesInterface passivesInterface;
    public PassivesInterface PassivesInterface => passivesInterface;
    [System.NonSerialized] public SortedList<int, Action> actionsToDoEveryTurn;
    Dictionary<string, PassiveWithTurnsInfo> passivesWithTurns;

    [System.NonSerialized] public Dictionary<AffinityTypes, ImmunityPassiveObject> immunityAffinityTypes;
    [System.NonSerialized] public Dictionary<PassiveObject, ImmunityPassiveObject> immunityPassives;
    [System.NonSerialized] public Dictionary<ElementObject, ImmunityPassiveObject> immunityElements;

    protected CharacterStats stats;
    public CharacterStats Stats => stats;

    private int turnTimer;
    
    protected void InitCharacter()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = characterObject.Sprite;
        GetComponentInChildren<Animator>().runtimeAnimatorController = characterObject.AnimatorController;

        SwitchAffinity(characterObject.AffinityType);

        passives = new HashSet<PassiveObject>();

        actionsToDoEveryTurn = new SortedList<int, Action>();
        actionsToDoEveryTurn.Add(1000, ()=> PassiveTurnCalcRemove());
        passivesWithTurns = new Dictionary<string, PassiveWithTurnsInfo>();

        immunityAffinityTypes = new Dictionary<AffinityTypes, ImmunityPassiveObject>();
        immunityPassives = new Dictionary<PassiveObject, ImmunityPassiveObject>();
        immunityElements = new Dictionary<ElementObject, ImmunityPassiveObject>();

        foreach(PassiveEntry passive in characterObject.StartingPassives)
        {
            if(passive.passive is FlatPassiveObject)
            {
                AddFlatPassive((FlatPassiveObject) passive.passive);
            }
            else if(passive.passive is DynamicPassiveObject)
            {
                AddDynamicPassive(((DynamicPassiveObject) passive.passive), passive.value, false);
            }
            else if(passive.passive is ImmunityPassiveObject)
            {
                AddImmunityPassive((ImmunityPassiveObject) passive.passive);
            }
        }   
    }

    private void PassiveTurnCalcRemove()
    {
        List<string> passiveGUIDsToRemoveFromTurns = new List<string>();

        foreach(KeyValuePair<string, PassiveWithTurnsInfo> passive in passivesWithTurns)
        {
            if(passive.Value.turnCounter == GameManager.Instance.turnCounter)
            {
                if(!(passive.Value.passive  is DynamicPassiveObject))
                {
                    RemovePassive(passive.Value.passive);
                }
                else
                {
                    SubtractDynamicPassive((DynamicPassiveObject) passive.Value.passive, passive.Value.addedValue);
                }
                
                passiveGUIDsToRemoveFromTurns.Add(passive.Value.GUID);
            }
        }

        foreach(string guid in passiveGUIDsToRemoveFromTurns)
        {
            passivesWithTurns.Remove(guid);
        }
    }

    public void SwitchAffinity(AffinityTypes type)
    {
        affinityType = type;
    }

    public bool AddFlatPassive(FlatPassiveObject passive)
    {
        foreach(KeyValuePair<PassiveObject, ImmunityPassiveObject> passiveObject in immunityPassives)
        {
            if(passive.Name == passiveObject.Key.Name)
            {
                return false;
            }
        }

        bool added = passives.Add(passive);

        if(added)
        {  
            passive.TakeAffect(this);
            passivesInterface.InitPassiveSlotUI(passive);
        }
  
        return added;
    }

    public bool AddFlatPassiveForTurns(FlatPassiveObject passive, int turns)
    {
        bool added = AddFlatPassive(passive);

        if(added)
        {
            PassiveWithTurnsInfo pwti = new PassiveWithTurnsInfo(passive, turns);
            passivesWithTurns.Add(pwti.GUID, pwti);
        }
    
        return added;
    }

    public bool AddImmunityPassive(ImmunityPassiveObject passive)
    {
        foreach(KeyValuePair<PassiveObject, ImmunityPassiveObject> passiveObject in immunityPassives)
        {
            if(passive.Name == passiveObject.Key.Name)
            {
                return false;
            }
        }

        bool added = passives.Add(passive);

        if(added)
        {  
            if(passive.ImmuneToPassives)
            {
                foreach(PassiveObject passiveObject in passive.ImmuneToPassivesList)
                {
                    RemovePassive(passiveObject);

                    List<string> passivesWithTurnsToRemove = new List<string>();

                    foreach(KeyValuePair<string, PassiveWithTurnsInfo> pwtiEntry in passivesWithTurns)
                    { 
                        if(pwtiEntry.Value.passive.Name == passiveObject.Name)
                        {
                            passivesWithTurnsToRemove.Add(pwtiEntry.Key);
                        }
                    }

                    foreach(string GUID in passivesWithTurnsToRemove)
                    {
                        passivesWithTurns.Remove(GUID);
                    }
                }
            }

            passive.TakeAffect(this);
            passivesInterface.InitPassiveSlotUI(passive);
        }

        return added;
    }

    public bool AddImmununityPassiveForTurns(ImmunityPassiveObject passive, int turns)
    {
        bool added = AddImmunityPassive(passive);

        if(added)
        {
            PassiveWithTurnsInfo pwti = new PassiveWithTurnsInfo(passive, turns);
            passivesWithTurns.Add(pwti.GUID, pwti);
        }
    
        return added;
    }

    public bool AddDynamicPassive(DynamicPassiveObject passive, float value, bool replace)
    {
        foreach(KeyValuePair<PassiveObject, ImmunityPassiveObject> passiveObject in immunityPassives)
        {
            if(passive.Name == passiveObject.Key.Name)
            {
                return false;
            }
        }

        bool added = false;

        if(replace)
        {
            if(!passives.Remove(passive))
            {
                added = true;
            }

            passives.Add(passive);

            passive.InitDynamicPassive(value);
            passive.TakeAffect(this);

            if(added)
            {
                passivesInterface.InitPassiveSlotUI(passive);
            }
            else
            {
                passivesInterface.UpdateDynamicPassiveUI(passive);
            }
        }
        else
        {
            added = passives.Add(passive);

            if(added)
            {
                passive.InitDynamicPassive(value);
                passive.TakeAffect(this);
                passivesInterface.InitPassiveSlotUI(passive);
            }
            else
            {
                foreach(PassiveObject p in passives)
                {
                    if(p.Name == passive.Name)
                    {
                        CombineDynamicPassives((DynamicPassiveObject) p, value); 
                    }
                } 
            }
        }
        
        return added;
    }

    public bool AddDynamicPassiveForTurns(DynamicPassiveObject passive, float value, int turns, bool replace)
    {
        foreach(KeyValuePair<PassiveObject, ImmunityPassiveObject> passiveObject in immunityPassives)
        {
            if(passive.Name == passiveObject.Key.Name)
            {
                return false;
            }
        }

        bool added = AddDynamicPassive(passive, value, replace);

        if(replace)
        {
            List<string> passivesWithTurnsToRemove = new List<string>();

            foreach(KeyValuePair<string, PassiveWithTurnsInfo> pwtiEntry in passivesWithTurns)
            { 
                if(pwtiEntry.Value.passive.Name == passive.Name)
                {
                    passivesWithTurnsToRemove.Add(pwtiEntry.Key);
                }
            }

            foreach(string GUID in passivesWithTurnsToRemove)
            {
                passivesWithTurns.Remove(GUID);
            }
        }

        PassiveWithTurnsInfo pwti = new PassiveWithTurnsInfo(passive, value, turns);
        passivesWithTurns.Add(pwti.GUID, pwti);
        
        return added;
    }

    public void CombineDynamicPassives(DynamicPassiveObject passive, float value)
    {
        passive.RemoveAffect(this);
        passive.InitDynamicPassive(passive.Value + value);
        passive.TakeAffect(this);
        passivesInterface.UpdateDynamicPassiveUI(passive);
    }

    public void SubtractDynamicPassive(DynamicPassiveObject passive, float value)
    {
        if(passive.Value - value == 0)
        {
            RemovePassive(passive);
        }
        else
        {
            passive.RemoveAffect(this);
            passive.InitDynamicPassive(passive.Value - value);
            passive.TakeAffect(this);
            passivesInterface.UpdateDynamicPassiveUI(passive);
        } 
    }

    public bool RemovePassive(PassiveObject passive)
    {
        bool removed = passives.Remove(passive);

        if(removed && passive != null)
        {
            passive.RemoveAffect(this);
            passivesInterface.RemovePassiveSlotUI(passive);
        }

        return removed;
    }

    [System.Serializable]
    class PassiveWithTurnsInfo
    {
        public PassiveObject passive;
        public int turnCounter;
        public float addedValue;
        public int addedTurns;
        public string GUID;

        public PassiveWithTurnsInfo()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public PassiveWithTurnsInfo(PassiveObject _passive, float _addedValue, int _addedTurns) : this()
        {
            passive = _passive;
            turnCounter = GameManager.Instance.turnCounter + _addedTurns;
            addedValue = _addedValue;
            addedTurns = _addedTurns;
        }

        public PassiveWithTurnsInfo(PassiveObject _passive, int _addedTurns) : this()
        {
            passive = _passive;
            turnCounter = GameManager.Instance.turnCounter + _addedTurns;
            addedValue = -1;
            addedTurns = -1;
        }
    }
}
