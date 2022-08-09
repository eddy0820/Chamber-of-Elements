using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;
        
        GameStateManager.Instance.SetReRollSpecificNextTurn(elementalElement.BehaviorEntries.Element1);

        return true;
    }
}
