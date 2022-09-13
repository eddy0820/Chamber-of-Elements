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
    public PlayerStats(BaseStatsObject _baseStats, GameObject _HPText, Character _character)
    {
        baseStats = _baseStats;
        InitializeCharacterStats();
        character = _character;
        HPText = _HPText;
    }

    public override void Die()
    {
        base.Die();

        FlashScreenBehavior.Instance.DoFlash();
        GameManager.Instance.LoseCanvas.SetActive(true);
        character.SpriteObject.SetActive(false);
    }
}
