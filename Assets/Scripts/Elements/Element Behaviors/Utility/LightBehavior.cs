using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        int healAmount = Random.Range((int) utilityElement.HealAmount, ((int) utilityElement.BehaviorEntries.Float1) + 1);

        character.Stats.Heal(healAmount);
        
        return true;
    }
}
