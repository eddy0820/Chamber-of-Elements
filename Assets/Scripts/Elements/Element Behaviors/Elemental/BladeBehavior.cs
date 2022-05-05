using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BladeBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        int slot = GameManager.Instance.ElementSlotsInv.FindFirstEmptySlot();

        GameManager.Instance.ElementSlotsInv.Container.elementSlots[slot].UpdateSlot(new Element(elementalElement.AssociatedElement));

        return true;
    }
}
