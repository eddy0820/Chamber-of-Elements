using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikesBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        List<int> iceElements = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.BehaviorEntries.Element1.ID);

        float damage = 0;

        if(iceElements != null)
        {
            foreach(int i in iceElements)
            {
                damage += elementalElement.BehaviorEntries.Float1;
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
            }
        }

        character.Stats.TakeDamage(damage, GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, Player.Instance);
    
        return true;
    }
}
