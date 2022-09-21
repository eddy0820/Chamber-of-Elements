using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, AffinityTypes.None, character);
        DoParticleEffectEnemy(ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]));

        ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[1]).Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, AffinityTypes.None, character);
        DoParticleEffectEnemy(ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[1]));

        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
    }
}
