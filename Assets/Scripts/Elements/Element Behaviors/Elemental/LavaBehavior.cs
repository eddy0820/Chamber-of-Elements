using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        float damageAmount = Random.Range(elementalElement.Damage, elementalElement.BehaviorEntries.Float1);

        GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, damageAmount);

        return true;
    }
}
