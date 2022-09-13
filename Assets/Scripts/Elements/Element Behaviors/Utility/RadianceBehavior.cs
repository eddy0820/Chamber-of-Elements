using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadianceBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(character is Player)
        {
            if(!GameStateManager.Instance.playerTurnGameState.hasUsedRadiance)
            {
                character.Stats.Heal(utilityElement.HealAmount);

                GameStateManager.Instance.playerTurnGameState.hasUsedRadiance = true;

                return true;
            }
        }
        else if(character is Minion)
        {
            if(!GameStateManager.Instance.minionTurnGameState.hasUsedRadiance)
            {
                character.Stats.Heal(utilityElement.HealAmount);

                GameStateManager.Instance.minionTurnGameState.hasUsedRadiance = true;

                return true;
            }
        }
        
        return false;
    }
}
