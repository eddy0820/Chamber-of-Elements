using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombustionBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameManager.Instance.GameStateManager.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.Player.Stats.LastHitDamage + elementalElement.Damage);
        
        return true;
    }
}
