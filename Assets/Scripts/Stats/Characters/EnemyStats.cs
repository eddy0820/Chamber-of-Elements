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
    public EnemyStats(BaseStatsObject _baseStats)
    {
        baseStats = _baseStats;
        InitializeCharacterStats();

        if(stats.ContainsKey("MaxMana"))
        {
            currentMana = (int)stats["MaxMana"].value;
        }
        else
        {
            currentMana = 0;
        }
    }
}
