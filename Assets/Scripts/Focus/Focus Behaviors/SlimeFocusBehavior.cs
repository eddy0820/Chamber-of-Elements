using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlimeFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        Player.Instance.Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, Player.Instance, character);
        DoParticleEffectEnemy(Player.Instance);

        ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
    }
}
