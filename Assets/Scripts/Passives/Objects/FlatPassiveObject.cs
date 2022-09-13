using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flat Passive", menuName = "Passives/Flat")]
public class FlatPassiveObject : PassiveObject
{
    [Header("Stat Modifying Passive")]
    [Header("Flat Passive Specific")]

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
    [SerializeField] bool behaviorPassive;
    public bool BehaviorPassive => behaviorPassive;

    [Space(15)]
    [SerializeField] bool startOfTurnBehavior;
    public bool StartOfTurnBehavior => startOfTurnBehavior;
    [SerializeField] bool endOfTurnBehavior;
    public bool EndOfTurnBehavior => endOfTurnBehavior;
    [SerializeField] bool onHitBehavior;
    public bool OnHitBehavior => onHitBehavior;

    [Space(15)]
    [SerializeField] PassiveBehaviorTypes behaviorType;
    public PassiveBehaviorTypes BehaviorType => behaviorType;
    [SerializeField] int behaviorOrder;
    public int BehaviorOrder => behaviorOrder;
    [SerializeField] AffinityTypes affinityTypeForDamageSelfBehavior;
    public AffinityTypes AffinityTypeForDamageSelfBehavior => affinityTypeForDamageSelfBehavior;

    public override void TakeAffect(Character character)
    {
        if(StatModifyingPassive)
        {   
            TakeAffectModifier(character);
        } 
        else if(BehaviorPassive)
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
        else if(BehaviorPassive)
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
            case PassiveBehaviorTypes.DamageToSelf:

                if(OnHitBehavior)
                {
                    action = ()=> character.Stats.TakeDamageNoActions(value, AffinityTypeForDamageSelfBehavior, null);
                }
                else
                {
                    action = ()=> character.Stats.TakeDamage(value, AffinityTypeForDamageSelfBehavior, null);
                }
                
                break;
            case PassiveBehaviorTypes.DamageToAttacker:
                if(OnHitBehavior)
                {
                    action = ()=> character.Attacker.Stats.TakeDamageNoActions(value, character.AffinityType, null);
                }
                else
                {
                    action = ()=> character.Attacker.Stats.TakeDamage(value, character.AffinityType, null);
                }

                break;
            case PassiveBehaviorTypes.Heal:
                action = ()=> character.Stats.Heal(value);
                break;
            case PassiveBehaviorTypes.Enlightenment:
                action = ()=> GameManager.Instance.InterfaceCanvas.transform.GetChild(0).GetComponent<ElementSlotsInterface>().DoEnlightenment();
                break;
        }

        if(StartOfTurnBehavior)
        {
            if(character.actionsToDoStartOfEveryTurn.ContainsKey(behaviorOrder))
            {
                Debug.Log("'ActionsToDoStartOfEveryTurn' already contains key " + behaviorOrder + ". Source: " + this.Name + ".");
            }
            else
            {
                character.actionsToDoStartOfEveryTurn.Add(behaviorOrder, action);
            }
        }
        else if(EndOfTurnBehavior)
        {
            if(character.actionsToDoEndOfEveryTurn.ContainsKey(behaviorOrder))
            {
                Debug.Log("'ActionsToDoEndOfEveryTurn' already contains key " + behaviorOrder + ". Source: " + this.Name + ".");
            }
            else
            {
                character.actionsToDoEndOfEveryTurn.Add(behaviorOrder, action);
            }
        }
        else if(OnHitBehavior)
        {
            if(character.actionsToDoOnHit.ContainsKey(behaviorOrder))
            {
                Debug.Log("'ActionsToDoOnHit' already contains key " + behaviorOrder + ". Source: " + this.Name + ".");
            }
            else
            {
                character.actionsToDoOnHit.Add(behaviorOrder, action);
            }
        }
    }

    private void RemoveAffectBehavior(Character character)
    {
        if(StartOfTurnBehavior)
        {
            character.actionsToDoStartOfEveryTurn.Remove(behaviorOrder);
        }
        else if(EndOfTurnBehavior)
        {
            character.actionsToDoEndOfEveryTurn.Remove(behaviorOrder);
        }
        else if(OnHitBehavior)
        {
            character.actionsToDoOnHit.Remove(behaviorOrder);
        }
    }
}
