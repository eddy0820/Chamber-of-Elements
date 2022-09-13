using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() == WeatherStateManager.Instance.GetWeatherState[utilityElement.BehaviorEntries.Weather1].GetType())
        {
           character.Stats.Heal(utilityElement.HealAmount);
        }

       WeatherStateManager.Instance.ClearWeather();
        
        return true;
    }
}
