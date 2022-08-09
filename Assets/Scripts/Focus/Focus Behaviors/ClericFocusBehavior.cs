using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClericFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        Player.Instance.Stats.Heal(character.Stats.Stats["FocusValue"].value, Player.Instance);
    }
}
