using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthquakeBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        ElementBehaviorUtil.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.mouseElement.element.SecondaryAffinityType, elementalElement.BehaviorEntries.Float1);
    
        return true;
    }
}
