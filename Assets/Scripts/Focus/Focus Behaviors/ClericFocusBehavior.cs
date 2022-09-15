using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClericFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).Stats.Heal(character.Stats.Stats["FocusValue"].value);
    }
}
