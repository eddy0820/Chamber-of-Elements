using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStats : CharacterStats
{
    public MinionStats()
    {
        baseStats = null;
        stats = new Dictionary<string, Stat>();
    }

    public MinionStats(BaseStatsObject _baseStats, GameObject _HPText, Character _character)
    {
        baseStats = _baseStats;
        InitializeCharacterStats();
        character = _character;
        HPText = _HPText;
    }

    public override void Die()
    {
        base.Die();

        Player.Instance.Minion.NullifyMinion();
    }
}
