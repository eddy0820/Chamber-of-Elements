using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        List<int> waterPrimals = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.BehaviorEntries.Element2.ID);

        if(waterPrimals != null)
        {
            foreach(int i in waterPrimals)
            {
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element(elementalElement.BehaviorEntries.Element1));
            }
        }

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.BehaviorEntries.Passive2.passive);
        }
        
        return true;
    }
}
