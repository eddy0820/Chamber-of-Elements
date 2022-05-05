using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        GameManager.Instance.WeatherStateManager.SwitchAndExtendWeather(arenaElement.AffectedWeather, arenaElement.TurnTimer);
        
        return true;
    }
}
