using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GaleBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        character.AddDynamicPassiveForTurns((DynamicPassiveObject) elementalElement.AssociatedPassive.passive, elementalElement.AssociatedPassive.value, ((int) elementalElement.ExtraValue) - 1, false);
    
        return true;
    }
}
