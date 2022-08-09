using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        WeatherStateManager.Instance.SwitchWeatherForTurns(arenaElement.BehaviorEntries.Weather1, (int) arenaElement.BehaviorEntries.Float1);
        
        return true;
    }
}
