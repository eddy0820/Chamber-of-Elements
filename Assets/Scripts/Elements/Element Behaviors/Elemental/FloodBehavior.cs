using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;
        
        GameManager.Instance.GameStateManager.SetReRollSpecificNextTurn(elementalElement.AssociatedElement);

        return true;
    }
}
