using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;
        
        Player.Instance.AddDynamicPassive((DynamicPassiveObject) utilityElement.AssociatedPassive, utilityElement.ExtraValue, false);

        return true;
    }
}
