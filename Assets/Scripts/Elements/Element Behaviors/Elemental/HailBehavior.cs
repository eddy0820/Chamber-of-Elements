using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HailBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() ==WeatherStateManager.Instance.GetWeatherState[GameManager.Instance.WeatherDatabase.GetWeather["Hailstorm"]].GetType())
        {
            Player.Instance.Stats.TakeDamage(elementalElement.ExtraValue, GameManager.Instance.mouseElement.element.AffinityType, Player.Instance);
        }

        if(GameManager.Instance.Enemy.Passives.Contains(elementalElement.AssociatedPassive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive);
        }

        return true;
    }
}
