using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurityBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;
        
        foreach(PassiveObject passive in character.Passives)
        {
            if(passive.IsPositiveEffect == false)
            {
                character.RemovePassive(passive);
                return true;
            }
        }

        return true;
    }
}
