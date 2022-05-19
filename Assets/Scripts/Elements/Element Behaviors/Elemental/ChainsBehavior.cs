using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainsBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        GameManager.Instance.Enemy.AddFlatPassiveForTurns((FlatPassiveObject) elementalElement.TertiaryAssociatedPassive, (int) elementalElement.ExtraValue);

        if(GameManager.Instance.Enemy.Passives.Contains(elementalElement.AssociatedPassive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive);
        }

        return true;
    }
}
