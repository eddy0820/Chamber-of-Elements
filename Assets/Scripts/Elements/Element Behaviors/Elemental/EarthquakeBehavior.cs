using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthquakeBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameStateManager.Instance.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.ExtraValue);
    
        return true;
    }
}
