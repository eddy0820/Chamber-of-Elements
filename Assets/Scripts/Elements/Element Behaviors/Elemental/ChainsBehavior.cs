using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainsBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameManager.Instance.GameStateManager.enemyTurnGameState.cannotFocusCounter = (int) elementalElement.ExtraValue;

        return true;
    }
}
