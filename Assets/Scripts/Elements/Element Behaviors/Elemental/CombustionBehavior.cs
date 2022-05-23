using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombustionBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, Player.Instance.Stats.LastHitDamage + elementalElement.Damage);
        
        return true;
    }
}
