using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeltBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.Stats.TakeDamage(elementalElement.Damage, GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, Player.Instance);
        }

        return true;
    }
}
