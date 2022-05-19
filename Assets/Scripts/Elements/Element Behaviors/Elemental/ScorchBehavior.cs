using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        int slot = GameManager.Instance.ElementSlotsInv.FindElement(element.AssociatedElement.ID);

        if(slot > 0)
        {
            GameManager.Instance.ElementSlotsInv.Container.elementSlots[slot].UpdateSlot(new Element());
            GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.Damage);
        }

        return true;
    }
}
