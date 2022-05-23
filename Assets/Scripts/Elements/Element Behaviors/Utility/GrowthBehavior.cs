using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        int slot = GameManager.Instance.ElementSlotsInv.FindElement(utilityElement.AssociatedElement.ID);

        if(slot > 0)
        {
            GameManager.Instance.ElementSlotsInv.Container.elementSlots[slot].UpdateSlot(new Element());
            character.Stats.Heal(utilityElement.ExtraValue, Player.Instance);
        }

        return true;
    }
}
