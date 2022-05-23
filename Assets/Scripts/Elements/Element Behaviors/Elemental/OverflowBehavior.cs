using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverflowBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        List<int> waterPrimals = GameManager.Instance.ElementSlotsInv.FindElements(elementalElement.AssociatedElement.ID);

        float damage = 0;

        if(waterPrimals != null)
        {
            foreach(int i in waterPrimals)
            {
                damage += elementalElement.Damage;
                GameManager.Instance.ElementSlotsInv.Container.elementSlots[i].UpdateSlot(new Element());
            }
        }

        GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, damage);

        return true;
    }
}
