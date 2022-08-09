using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        BehaviorScriptEntries be = arenaElement.BehaviorEntries;

        if(WeatherStateManager.Instance.currentState.GetType() == WeatherStateManager.Instance.GetWeatherState[be.Weather1].GetType())
        {
            WeatherStateManager.Instance.SwitchWeather(be.Weather2);
        }
        
        WeatherStateManager.Instance.ExtendWeather((int) be.Float1);

        if(GameManager.Instance.Enemy.Passives.Contains(be.Passive1.passive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) be.Passive2.passive);
        }
        
        return true;
    }
}
