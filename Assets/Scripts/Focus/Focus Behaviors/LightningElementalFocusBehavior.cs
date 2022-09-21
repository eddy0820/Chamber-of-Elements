using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningElementalFocusBehavior : AbstractFocusBehavior
{
    public override void PerformFocus(FocusObject focus, Character character)
    {
        ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, AffinityTypes.None, character);
    }
}
