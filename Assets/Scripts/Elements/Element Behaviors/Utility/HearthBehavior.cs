using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HearthBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        Player.Instance.Stats.Heal(utilityElement.HealAmount);
        
        if(Player.Instance.MinionExists)
        {
            Player.Instance.Minion.Stats.Heal(element.BehaviorEntries.Float1);
        }

        return true;
    }
}
