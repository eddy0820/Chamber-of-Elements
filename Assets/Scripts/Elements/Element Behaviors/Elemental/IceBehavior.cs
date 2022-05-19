using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(GameManager.Instance.Enemy.Passives.Contains(elementalElement.AssociatedPassive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive);
        }

        return true;
    }
}
