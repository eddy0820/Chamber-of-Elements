using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(character.Passives.Contains(elementalElement.AssociatedPassive.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive.passive);
        }

        return true;
    }
}
