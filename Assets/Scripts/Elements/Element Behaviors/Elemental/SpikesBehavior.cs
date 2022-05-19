using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikesBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        List<int> iceElements = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.AssociatedElement.ID);

        float damage = 0;

        if(iceElements != null)
        {
            foreach(int i in iceElements)
            {
                damage += elementalElement.ExtraValue;
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
            }
        }

        Player.Instance.Stats.TakeDamage(damage, GameManager.Instance.mouseElement.element.AffinityType, Player.Instance);
    
        return true;
    }
}
