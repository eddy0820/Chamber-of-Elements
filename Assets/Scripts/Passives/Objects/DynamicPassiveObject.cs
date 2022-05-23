using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Passive", menuName = "Passives/Dynamic")]
public class DynamicPassiveObject : PassiveObject 
{
    [Header("Dynamic Passive Specific")]

    [Header("Stat Modifying Passive")]
    [SerializeField] bool statModifyingPassive;
    public bool StatModifyingPassive => statModifyingPassive;
    [SerializeField] StatTypeObject affectedStat;
    public StatTypeObject AffectedStat => affectedStat;
    [SerializeField] StatTypeObject secondaryAffectedStat;
    public StatTypeObject SecondaryAffectedStat => secondaryAffectedStat;
    [SerializeField] StatModifierTypes modifierType;
    public StatModifierTypes ModifierType => modifierType;

    [Header("Behavior Every Turn Passive")]
    [SerializeField] bool behaviorEveryTurnPassive;
    public bool BehaviorEveryTurnPassive => behaviorEveryTurnPassive;
    [SerializeField] PassiveBehaviorTypes behaviorType;
    public PassiveBehaviorTypes BehaviorType => behaviorType;
    [SerializeField] int behaviorOrder;
    public int BehaviorOrder => behaviorOrder;
    [SerializeField] AffinityTypes affinityTypeForDamageBehavior;
    public AffinityTypes AffinityTypeForDamageBehavior => affinityTypeForDamageBehavior;
    float value = 0;
    public float Value => value;
    bool initPassive;

    public void InitDynamicPassive(float _value)
    {
        if(StatModifyingPassive)
        {
            modifier = new StatModifier(_value, ModifierType);
        }

        value = _value;  

        initPassive = true;
    }

    public override void TakeAffect(Character character)
    {
        if(StatModifyingPassive)
        {   
            TakeAffectModifier(character);
        } 
        else if(BehaviorEveryTurnPassive)
        {
            TakeAffectBehavior(character);
        }
    }

    public override void RemoveAffect(Character character)
    {
        if(StatModifyingPassive)
        {
            RemoveAffectModifier(character);
        } 
        else if(BehaviorEveryTurnPassive)
        {
            RemoveAffectBehavior(character);
        }
    }

    private void TakeAffectModifier(Character character)
    {
        if(initPassive)
        {
            character.Stats.Stats[AffectedStat.Name].AddModifier(modifier);
            
            if(SecondaryAffectedStat != null)
            {
                character.Stats.Stats[SecondaryAffectedStat.Name].AddModifier(modifier);
            }

            initPassive = false;
        }
        else
        {
            Debug.Log("Dynamic Modifier has not been created for " + Name);
        }
    }

    private void RemoveAffectModifier(Character character)
    {
        character.Stats.Stats[AffectedStat.Name].RemoveModifier(modifier);

        if(SecondaryAffectedStat != null)
        {
            character.Stats.Stats[SecondaryAffectedStat.Name].RemoveModifier(modifier);
        }

        initPassive = false;
    }

    private void TakeAffectBehavior(Character character)
    {
        if(initPassive)
        {
            switch(BehaviorType)
            {
                case PassiveBehaviorTypes.Damage:
                    action = ()=> character.Stats.TakeDamage(value, AffinityTypeForDamageBehavior, character);
                    break;
                case PassiveBehaviorTypes.Heal:
                    action = ()=> character.Stats.Heal(value, character); 
                    break;
            }
            
            if(character.actionsToDoEveryTurn.ContainsKey(behaviorOrder))
            {
                Debug.Log("'ActionsToDoEveryTurn' already contains key " + behaviorOrder + ". Source: " + this.Name + ".");
                initPassive = false;
            }
            else
            {
                character.actionsToDoEveryTurn.Add(behaviorOrder, action);
                initPassive = false;
            }
            
        }
        else
        {
            Debug.Log("Dynamic Behavior has not been created for " + Name);
        }
    }

    private void RemoveAffectBehavior(Character character)
    {
        character.actionsToDoEveryTurn.Remove(behaviorOrder);
        initPassive = false;
    }
}
