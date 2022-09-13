using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObject : ScriptableObject
{
    [Header("Base")]

    [SerializeField] new string name = "New Character Name"; 
    public string Name => name;
    [SerializeField] BaseStatsObject baseStats;
    public BaseStatsObject BaseStats => baseStats;
    [SerializeField] AffinityTypes affinityType;
    public AffinityTypes AffinityType => affinityType;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] List<PassiveEntry> startingPassives = new List<PassiveEntry>();
    public List<PassiveEntry> StartingPassives => startingPassives;

    [Header("Visuals")]

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] RuntimeAnimatorController animationController;
    public RuntimeAnimatorController AnimatorController => animationController;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] DeathRattleObject[] deathRattles;
    public DeathRattleObject[] DeathRattles => deathRattles;
}
