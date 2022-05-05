using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        StatModifier modifier = new StatModifier(utilityElement.ExtraValue, StatModifierTypes.Flat);
        
        GameManager.Instance.Player.Stats.Stats["BasicAttack"].AddModifier(modifier);
        
        return true;
    }
}
