using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        ElementBehaviorUtil.DealDamageToEverythingExceptEnemy(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, elementalElement.BehaviorEntries.Float1);
        
        return true;
    }
}
