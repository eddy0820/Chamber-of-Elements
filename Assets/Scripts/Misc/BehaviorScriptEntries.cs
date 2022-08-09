using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BehaviorScriptEntries
{
    [SerializeField] float float1 = -1;
    public float Float1 => float1;
    [SerializeField] float float2 = -1;
    public float Float2 => float2;
    [SerializeField] float float3 = -1;
    public float Float3 => float3;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] ElementObject element1;
    public ElementObject Element1 => element1;
    [SerializeField] ElementObject element2;
    public ElementObject Element2 => element2;
    [SerializeField] ElementObject element3;
    public ElementObject Element3 => element3;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] PassiveEntry passive1;
    public PassiveEntry Passive1 => passive1;
    [SerializeField] PassiveEntry passive2;
    public PassiveEntry Passive2 => passive2;
    [SerializeField] PassiveEntry passive3;
    public PassiveEntry Passive3 => passive3;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] RelicObject relic1;
    public RelicObject Relic1 => relic1;
    [SerializeField] RelicObject relic2;
    public RelicObject Relic2 => relic2;
    [SerializeField] RelicObject relic3;
    public RelicObject Relic3 => relic3;

    [Space(10)]

    [SerializeField] RelicObject[] relicList;
    public RelicObject[] RelicList => relicList;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] RecipeSet recipeSet1;
    public RecipeSet RecipeSet1 => recipeSet1;
    [SerializeField] RecipeSet recipeSet2;
    public RecipeSet RecipeSet2 => recipeSet2;
    [SerializeField] RecipeSet recipeSet3;
    public RecipeSet RecipeSet3 => recipeSet3;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] WeatherObject weather1;
    public WeatherObject Weather1 => weather1;
    [SerializeField] WeatherObject weather2;
    public WeatherObject Weather2 => weather2;
    [SerializeField] WeatherObject weather3;
    public WeatherObject Weather3 => weather3;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] RuntimeAnimatorController animController1;
    public RuntimeAnimatorController AnimController1 => animController1;
    [SerializeField] RuntimeAnimatorController animController2;
    public RuntimeAnimatorController AnimController2 => animController2;

    [HorizontalLine(color: EColor.Gray, height: 2)]

    [SerializeField] AffinityTypes affinity1 = AffinityTypes.None;
    public AffinityTypes Affinity1 => affinity1;
    [SerializeField] AffinityTypes affinity2 = AffinityTypes.None;
    public AffinityTypes Affinity2 => affinity2;
    [SerializeField] AffinityTypes affinity3 = AffinityTypes.None;
    public AffinityTypes Affinity3 => affinity3;
}
