using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricityBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(character.Passives.Contains(elementalElement.AssociatedPassive.passive))
        {
            character.Stats.TakeDamage(elementalElement.ExtraValue, new Element(elementalElement).AffinityType, GameManager.Instance.Enemy);
        }

        return true;
    }
}