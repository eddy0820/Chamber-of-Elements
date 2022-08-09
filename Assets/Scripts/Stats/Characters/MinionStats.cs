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

    public MinionStats(BaseStatsObject _baseStats, GameObject _HPText)
    {
        baseStats = _baseStats;
        InitializeCharacterStats();

        HPText = _HPText;
    }

    public override void Die(Character character)
    {
        base.Die(character);

        Player.Instance.Minion.NullifyMinion();
    }
}
