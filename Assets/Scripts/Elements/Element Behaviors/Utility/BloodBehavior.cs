using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;
        
        character.AddDynamicPassive((DynamicPassiveObject) utilityElement.BehaviorEntries.Passive1.passive, utilityElement.BehaviorEntries.Passive1.value, false, false);
        
        return true;
    }
}
