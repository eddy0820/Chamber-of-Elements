using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeltBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(character.Passives.Contains(elementalElement.AssociatedPassive.passive))
        {
            character.Stats.TakeDamage(elementalElement.Damage, new Element(elementalElement).AffinityType, character, Player.Instance);
        }

        return true;
    }
}
