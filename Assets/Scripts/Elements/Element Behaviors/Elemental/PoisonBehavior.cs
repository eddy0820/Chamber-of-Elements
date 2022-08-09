using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        character.AddDynamicPassiveForTurns((DynamicPassiveObject) elementalElement.BehaviorEntries.Passive1.passive, elementalElement.BehaviorEntries.Passive1.value, ((int) elementalElement.BehaviorEntries.Float1) - 1, false);
    
        return true;
    }
}
