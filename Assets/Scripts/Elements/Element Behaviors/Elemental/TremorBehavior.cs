using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremorBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        GameManager.Instance.ElementSlotsInv.ClearElements();
        
        return true;
    }
}
