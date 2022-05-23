using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsoonBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        WeatherStateManager.Instance.SwitchWeatherForTurns(arenaElement.AffectedWeather, arenaElement.TurnTimer);

        return true;
    }
}
