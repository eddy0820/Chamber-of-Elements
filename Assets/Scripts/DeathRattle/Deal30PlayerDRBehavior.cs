using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deal30PlayerDRBehavior : AbstractDeathRattle
{ 
    public override void DoBehavior(Character character, DeathRattleObject deathRattle)
    {
        Player.Instance.Stats.TakeDamage(deathRattle.AssociatedValue, character.AffinityType, Player.Instance, null);
    }
}
