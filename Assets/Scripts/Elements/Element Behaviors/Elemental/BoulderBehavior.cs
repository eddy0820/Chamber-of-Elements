using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoulderBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;
        
        List<int> earthPrimals = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.AssociatedElement.ID);

        if(earthPrimals != null)
        {
            foreach(int i in earthPrimals)
            {
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
            }
        }
        
        return true;
    }
}
