using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IllusionBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        List<int> airPrimals = GameManager.Instance.ElementSlotsInv.FindElements(utilityElement.BehaviorEntries.Element1.ID);

        if(airPrimals != null)
        {
            int counter = 0;
            foreach(int i in airPrimals)
            {
                if(counter < utilityElement.BehaviorEntries.Float1)
                {
                    GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
                    GameManager.Instance.Enemy.AddDynamicPassiveForTurns((DynamicPassiveObject) utilityElement.BehaviorEntries.Passive1.passive,  utilityElement.BehaviorEntries.Passive1.value, ((int) utilityElement.BehaviorEntries.Float2) - 1, false);
                    
                    counter++;
                }
                
            }
        }

        return true;
    }
}
