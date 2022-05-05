using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        float damageAmount = Random.Range(elementalElement.Damage, elementalElement.ExtraValue);

        GameManager.Instance.GameStateManager.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, damageAmount);

        return true;
    }
}