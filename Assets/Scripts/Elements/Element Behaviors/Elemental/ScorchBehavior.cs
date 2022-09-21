using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        int slot = GameManager.Instance.ElementSlotsInv.FindElement(element.BehaviorEntries.Element1.ID);

        if(slot > 0)
        {
            GameManager.Instance.ElementSlotsInv.Container.elementSlots[slot].UpdateSlot(new Element());
            character.Stats.TakeDamage(elementalElement.Damage, GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, Player.Instance);
        }

        return true;
    }
}
