using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        if(GameManager.Instance.WeatherStateManager.currentState.GetType() == GameManager.Instance.WeatherStateManager.GetWeatherState[GameManager.Instance.WeatherDatabase.GetWeather["Storm"]].GetType())
        {
            GameManager.Instance.Player.Stats.Heal(utilityElement.HealAmount, GameManager.Instance.Player);
        }

        GameManager.Instance.WeatherStateManager.ClearWeather();
        
        return true;
    }
}
