using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flat Passive", menuName = "Passives/Flat")]
public class FlatPassiveObject : PassiveObject
{
    [SerializeField] int percent;
    [SerializeField] AffinityTypes resistanceTo;
    [SerializeField] AffinityTypes[] removalTypes;
    public AffinityTypes[] RemovalTypes => removalTypes;

    public override void TakeAffect(CharacterStats stats)
    {
        switch(resistanceTo)
        {
            // actual percent calculation todo
            case AffinityTypes.Air:
                stats.airResistance += percent;
                break;
            case AffinityTypes.Earth:
                stats.earthResistance += percent;
                break;
            case AffinityTypes.Fire:
                stats.fireResistance += percent;
                break;
            case AffinityTypes.Water:
                stats.waterResistance += percent;
                break;
            case AffinityTypes.None:
                stats.generalResistance += percent;
                break;
            default:
                Debug.Log("Unrecognized damage type, affecting general resistance");
                stats.generalResistance += percent;
                break;
        }
    }

    public override void RemoveAffect(CharacterStats stats)
    {
        switch(resistanceTo)
        {
            case AffinityTypes.Air:
                stats.airResistance -= percent;
                break;
            case AffinityTypes.Earth:
                stats.earthResistance -= percent;
                break;
            case AffinityTypes.Fire:
                stats.fireResistance -= percent;
                break;
            case AffinityTypes.Water:
                stats.waterResistance -= percent;
                break;
            case AffinityTypes.None:
                stats.generalResistance -= percent;
                break;
            default:
                Debug.Log("Unrecognized damage type, affecting general resistance");
                stats.generalResistance += percent;
                break;
        }
    }
}
