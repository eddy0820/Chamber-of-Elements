using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcidBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        BehaviorScriptEntries be = elementalElement.BehaviorEntries;

        character.AddDynamicPassiveForTurns((DynamicPassiveObject) be.Passive1.passive, be.Passive1.value, ((int) be.Float1) - 1, false);

        return true;
    }
}
