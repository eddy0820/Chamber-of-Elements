using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvalancheBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(GameManager.Instance.WeatherStateManager.currentState.GetType() == GameManager.Instance.WeatherStateManager.GetWeatherState[GameManager.Instance.WeatherDatabase.GetWeather["Hailstorm"]].GetType())
        {
            GameManager.Instance.GameStateManager.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.ExtraValue);
        }

        return true;
    }
}
