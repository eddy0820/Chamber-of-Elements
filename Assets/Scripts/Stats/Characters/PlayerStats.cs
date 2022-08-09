using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public PlayerStats()
    {
        baseStats = null;
        stats = new Dictionary<string, Stat>();
    }
    public PlayerStats(BaseStatsObject _baseStats, GameObject _HPText)
    {
        baseStats = _baseStats;
        InitializeCharacterStats();

        HPText = _HPText;
    }
}
