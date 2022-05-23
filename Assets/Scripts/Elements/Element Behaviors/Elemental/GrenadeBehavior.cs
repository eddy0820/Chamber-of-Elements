using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameStateManager.Instance.DealDamageToEverythingExceptEnemy(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.Damage);
        
        return true;
    }
}
