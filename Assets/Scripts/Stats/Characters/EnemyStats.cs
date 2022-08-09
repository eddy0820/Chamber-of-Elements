using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public EnemyStats()
    {
        baseStats = null;
        stats = new Dictionary<string, Stat>();
    }
    public EnemyStats(BaseStatsObject _baseStats, GameObject _HPText)
    {
        baseStats = _baseStats;
        InitializeCharacterStats();

        HPText = _HPText;
    }
}
