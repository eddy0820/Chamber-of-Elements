using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        List<int> waterPrimals = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.SecondaryAssociatedElement.ID);

        if(waterPrimals != null)
        {
            foreach(int i in waterPrimals)
            {
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element(elementalElement.AssociatedElement));
            }
        }
        
        return true;
    }
}
