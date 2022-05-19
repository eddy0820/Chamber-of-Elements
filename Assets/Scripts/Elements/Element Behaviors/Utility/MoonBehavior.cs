using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() ==WeatherStateManager.Instance.GetWeatherState[GameManager.Instance.WeatherDatabase.GetWeather["Storm"]].GetType())
        {
            Player.Instance.Stats.Heal(utilityElement.HealAmount, Player.Instance);
        }

       WeatherStateManager.Instance.ClearWeather();
        
        return true;
    }
}
