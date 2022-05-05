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
    StatModifier modifier;

    public override void TakeAffect(CharacterStats stats)
    {
        modifier = new StatModifier(percent, StatModifierTypes.Flat);

        switch(resistanceTo)
        {
            case AffinityTypes.Air:
                stats.Stats["AirResistance"].AddModifier(modifier);
                break;
            case AffinityTypes.Earth:
                stats.Stats["EarthResistance"].AddModifier(modifier);
                break;
            case AffinityTypes.Fire:
                stats.Stats["FireResistance"].AddModifier(modifier);
                break;
            case AffinityTypes.Water:
                stats.Stats["WaterResistance"].AddModifier(modifier);
                break;
            case AffinityTypes.None:
                stats.Stats["GeneralResistance"].AddModifier(modifier);
                break;
            default:
                Debug.Log("Unrecognized damage type, affecting general resistance");
                stats.Stats["GeneralResistance"].AddModifier(modifier);
                break;
        }
    }

    public override void RemoveAffect(CharacterStats stats)
    {
        switch(resistanceTo)
        {
            case AffinityTypes.Air:
                stats.Stats["AirResistance"].RemoveModifier(modifier);
                break;
            case AffinityTypes.Earth:
                stats.Stats["EarthResistance"].RemoveModifier(modifier);
                break;
            case AffinityTypes.Fire:
                stats.Stats["FireResistance"].RemoveModifier(modifier);
                break;
            case AffinityTypes.Water:
                stats.Stats["WaterResistance"].RemoveModifier(modifier);
                break;
            case AffinityTypes.None:
                stats.Stats["GeneralResistance"].RemoveModifier(modifier);
                break;
            default:
                Debug.Log("Unrecognized damage type, affecting general resistance");
                stats.Stats["GeneralResistance"].RemoveModifier(modifier);
                break;
        }
    }
}
