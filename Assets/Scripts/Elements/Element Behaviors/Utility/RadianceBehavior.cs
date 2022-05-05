using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadianceBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(!GameManager.Instance.GameStateManager.playerTurnGameState.hasUsedRadiance)
        {
            GameManager.Instance.Player.Stats.Heal(utilityElement.HealAmount, GameManager.Instance.Player);
            GameManager.Instance.GameStateManager.playerTurnGameState.hasUsedRadiance = true;

            return true;
        }

        return false;
    }
}
