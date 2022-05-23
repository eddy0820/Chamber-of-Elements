using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flat Passive", menuName = "Passives/Flat")]
public class FlatPassiveObject : PassiveObject
{
    [Header("Flat Passive Specific")]

    [Header("Stat Modifying Passive")]
    
    [SerializeField] float value;

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
        modifier = new StatModifier(value, ModifierType);

        character.Stats.Stats[AffectedStat.Name].AddModifier(modifier);

        if(SecondaryAffectedStat != null)
        {
            character.Stats.Stats[SecondaryAffectedStat.Name].AddModifier(modifier);
        }
    }

    private void RemoveAffectModifier(Character character)
    {
        character.Stats.Stats[AffectedStat.Name].RemoveModifier(modifier);

        if(SecondaryAffectedStat != null)
        {
            character.Stats.Stats[SecondaryAffectedStat.Name].RemoveModifier(modifier);
        }
    }

    private void TakeAffectBehavior(Character character)
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
        }
        else
        {
            character.actionsToDoEveryTurn.Add(behaviorOrder, action);
        }
    }

    private void RemoveAffectBehavior(Character character)
    {
        character.actionsToDoEveryTurn.Remove(behaviorOrder);
    }
}
