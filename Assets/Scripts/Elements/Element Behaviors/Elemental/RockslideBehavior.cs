using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockslideBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        float damageAmount = Random.Range(elementalElement.Damage, elementalElement.ExtraValue);

        GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, damageAmount);

        return true;
    }
}
