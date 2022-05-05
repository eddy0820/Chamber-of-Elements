using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsoonBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        GameManager.Instance.WeatherStateManager.SwitchWeatherForTurns(arenaElement.AffectedWeather, arenaElement.TurnTimer);

        return true;
    }
}
