using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadianceBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(!GameStateManager.Instance.playerTurnGameState.hasUsedRadiance)
        {
            character.Stats.Heal(utilityElement.HealAmount, character);

            GameStateManager.Instance.playerTurnGameState.hasUsedRadiance = true;

            return true;
        }

        return false;
    }
}
