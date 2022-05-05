using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthquakeBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameManager.Instance.GameStateManager.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.ExtraValue);
    
        return true;
    }
}
