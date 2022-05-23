using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainsBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        character.AddFlatPassiveForTurns((FlatPassiveObject) elementalElement.TertiaryAssociatedPassive.passive, (int) elementalElement.ExtraValue);

        if(character.Passives.Contains(elementalElement.AssociatedPassive.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive.passive);
        }

        return true;
    }
}
