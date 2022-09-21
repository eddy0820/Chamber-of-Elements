using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deal30PlayerDRBehavior : AbstractDeathRattle
{ 
    public override void DoBehavior(Character character, DeathRattleObject deathRattle)
    {
        Player.Instance.Stats.TakeDamage(deathRattle.BehaviorEntries.Float1, character.AffinityType, AffinityTypes.None, null);
    }
}
