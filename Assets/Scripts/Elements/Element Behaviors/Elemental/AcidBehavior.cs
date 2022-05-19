using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcidBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameManager.Instance.Enemy.AddDynamicPassiveForTurns((DynamicPassiveObject) elementalElement.AssociatedPassive, elementalElement.ExtraValue, (int) elementalElement.SecondaryExtraValue, false);

        return true;
    }
}
