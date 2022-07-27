using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;
        
        character.AddDynamicPassive((DynamicPassiveObject) utilityElement.AssociatedPassive.passive, utilityElement.AssociatedPassive.value, false, false);

        return true;
    }
}
