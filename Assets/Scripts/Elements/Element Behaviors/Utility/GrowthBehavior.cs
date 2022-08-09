using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        int slot = GameManager.Instance.ElementSlotsInv.FindElement(utilityElement.BehaviorEntries.Element1.ID);

        if(slot > 0)
        {
            GameManager.Instance.ElementSlotsInv.Container.elementSlots[slot].UpdateSlot(new Element());
            character.Stats.Heal(utilityElement.BehaviorEntries.Float1, Player.Instance);
        }

        return true;
    }
}
