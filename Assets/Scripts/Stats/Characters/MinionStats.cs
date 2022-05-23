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

    public MinionStats(BaseStatsObject _baseStats)
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
