using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deal30PlayerDRBehavior : AbstractDeathRattle
{ 
    public override void DoBehavior(Character character, DeathRattleObject deathRattle)
    {
        Character player = GameManager.Instance.ConvertCharacterEntry(deathRattle.AssociatedCharacter);
        player.Stats.TakeDamage(deathRattle.AssociatedValue, character.AffinityType, player, null);
    }
}
