using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningElementalFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        GameManager.Instance.Enemy.Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, GameManager.Instance.Enemy, character);
    }
}
