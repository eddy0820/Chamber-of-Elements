using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockslideBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        float damageAmount = Random.Range(elementalElement.Damage, elementalElement.BehaviorEntries.Float1);

        GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, damageAmount);

        return true;
    }
}
