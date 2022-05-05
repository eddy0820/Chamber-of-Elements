using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        GameManager.Instance.ElementSlotsInv.ReRollElements();
        
        return true;
    }
}
