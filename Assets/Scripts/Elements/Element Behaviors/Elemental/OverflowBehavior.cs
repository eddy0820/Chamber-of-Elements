using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverflowBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        List<int> waterPrimals = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.BehaviorEntries.Element1.ID);

        float damage = 0;

        if(waterPrimals != null)
        {
            foreach(int i in waterPrimals)
            {
                damage += elementalElement.Damage;
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
            }
        }

        GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, damage);

        return true;
    }
}
