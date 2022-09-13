using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlimeFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        GameManager.Instance.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, character);
        DoParticleEffectEnemy(GameManager.Instance.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]));

        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
    }
}
