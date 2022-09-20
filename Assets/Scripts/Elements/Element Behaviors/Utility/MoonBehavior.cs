using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        BehaviorScriptEntries be = utilityElement.BehaviorEntries;

        if(WeatherStateManager.Instance.CompareWeatherStates((WeatherState) WeatherStateManager.Instance.currentState, WeatherStateManager.Instance.GetWeatherState[be.Weather1]))
        {
           character.Stats.Heal(utilityElement.HealAmount);
        }

       WeatherStateManager.Instance.ClearWeather();
        
        return true;
    }
}
