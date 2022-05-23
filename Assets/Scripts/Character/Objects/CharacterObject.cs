using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObject : ScriptableObject
{
    [SerializeField] new string name = "New Character Name"; 
    public string Name => name;
    [SerializeField] BaseStatsObject baseStats;
    public BaseStatsObject BaseStats => baseStats;
    [SerializeField] AffinityTypes affinityType;
    public AffinityTypes AffinityType => affinityType;
    [SerializeField] List<PassiveEntry> startingPassives = new List<PassiveEntry>();
    public List<PassiveEntry> StartingPassives => startingPassives;
    [SerializeField] Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] RuntimeAnimatorController animationController;
    public RuntimeAnimatorController AnimatorController => animationController;
}
