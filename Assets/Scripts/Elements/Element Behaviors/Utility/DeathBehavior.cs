using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        if(character is Minion)
        {
            Player.Instance.Minion.NullifyMinion();
            return true;
        }
        else
        {
            return false;
        }

        
    }
}
