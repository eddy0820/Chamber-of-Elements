using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultElementBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        Debug.Log("No Behavior.");
        
        return true;
    }
}
