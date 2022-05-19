using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

       WeatherStateManager.Instance.SwitchWeatherForTurns(arenaElement.AffectedWeather, arenaElement.TurnTimer);
        
        return true;
    }
}
