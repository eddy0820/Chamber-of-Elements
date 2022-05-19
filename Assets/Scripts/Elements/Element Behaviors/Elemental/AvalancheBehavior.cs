using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvalancheBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() ==WeatherStateManager.Instance.GetWeatherState[GameManager.Instance.WeatherDatabase.GetWeather["Hailstorm"]].GetType())
        {
            GameStateManager.Instance.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.ExtraValue);
        }

        if(GameManager.Instance.Enemy.Passives.Contains(elementalElement.AssociatedPassive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive);
        }

        return true;
    }
}
