using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailstormBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        BehaviorScriptEntries be = arenaElement.BehaviorEntries;

        WeatherStateManager.Instance.SwitchWeatherForTurns(be.Weather1, (int) be.Float1);

        if(GameManager.Instance.Enemy.Passives.Contains(be.Passive1.passive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) be.Passive2.passive);
        }

        return true;
    }
}
