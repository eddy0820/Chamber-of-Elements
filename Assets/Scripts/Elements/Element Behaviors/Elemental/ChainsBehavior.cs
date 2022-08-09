using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainsBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        character.AddFlatPassiveForTurns((FlatPassiveObject) elementalElement.BehaviorEntries.Passive3.passive, (int) elementalElement.BehaviorEntries.Float1);

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.BehaviorEntries.Passive2.passive);
        }

        return true;
    }
}
