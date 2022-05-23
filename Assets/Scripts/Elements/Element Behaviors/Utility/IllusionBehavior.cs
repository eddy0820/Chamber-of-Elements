using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IllusionBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        List<int> airPrimals = GameManager.Instance.ElementSlotsInv.FindElements(utilityElement.AssociatedElement.ID);

        if(airPrimals != null)
        {
            int counter = 0;
            foreach(int i in airPrimals)
            {
                if(counter < utilityElement.ExtraValue)
                {
                    GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
                    GameManager.Instance.Enemy.AddDynamicPassiveForTurns((DynamicPassiveObject) utilityElement.AssociatedPassive.passive,  utilityElement.AssociatedPassive.value, ((int) utilityElement.SecondaryExtraValue) - 1, false);
                    
                    counter++;
                }
                
            }
        }

        return true;
    }
}
