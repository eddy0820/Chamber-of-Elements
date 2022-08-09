using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.BehaviorEntries.Passive2.passive);
        }

        return true;
    }
}
