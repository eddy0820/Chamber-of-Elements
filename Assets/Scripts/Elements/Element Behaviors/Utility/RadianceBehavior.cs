using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadianceBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(!GameStateManager.Instance.playerTurnGameState.hasUsedRadiance)
        {
            Player.Instance.Stats.Heal(utilityElement.HealAmount, Player.Instance);
            GameStateManager.Instance.playerTurnGameState.hasUsedRadiance = true;

            return true;
        }

        return false;
    }
}
