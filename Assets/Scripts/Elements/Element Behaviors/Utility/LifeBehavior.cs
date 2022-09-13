using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(character is Player)
        {
            if(!GameStateManager.Instance.playerTurnGameState.hasUsedLife)
            {
                character.Stats.Heal(utilityElement.HealAmount);
                GameStateManager.Instance.playerTurnGameState.hasUsedLife = true;

                return true;
            }
        }
        else if(character is Minion)
        {
            if(!GameStateManager.Instance.minionTurnGameState.hasUsedLife)
            {
                character.Stats.Heal(utilityElement.HealAmount);
                GameStateManager.Instance.minionTurnGameState.hasUsedLife = true;

                return true;
            }
        }
        

        return false;
    }
}
