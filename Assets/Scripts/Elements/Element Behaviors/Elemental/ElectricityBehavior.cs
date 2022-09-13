using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricityBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.Stats.TakeDamage(elementalElement.BehaviorEntries.Float1, new Element(elementalElement).AffinityType, Player.Instance);
        }

        return true;
    }
}
