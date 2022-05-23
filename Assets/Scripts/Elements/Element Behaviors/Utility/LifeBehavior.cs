using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(!GameStateManager.Instance.playerTurnGameState.hasUsedLife)
        {
            character.Stats.Heal(utilityElement.HealAmount, character);
            GameStateManager.Instance.playerTurnGameState.hasUsedLife = true;

            return true;
        }

        return false;
    }
}
