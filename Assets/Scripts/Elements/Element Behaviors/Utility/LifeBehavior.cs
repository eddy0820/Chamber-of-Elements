using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(!GameStateManager.Instance.playerTurnGameState.hasUsedLife)
        {
            Player.Instance.Stats.Heal(utilityElement.HealAmount, Player.Instance);
            GameStateManager.Instance.playerTurnGameState.hasUsedLife = true;

            return true;
        }

        return false;
    }
}
