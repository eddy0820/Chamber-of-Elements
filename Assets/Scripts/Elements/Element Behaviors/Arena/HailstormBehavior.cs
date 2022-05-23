using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailstormBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ArenaElementObject arenaElement = (ArenaElementObject) element;

        WeatherStateManager.Instance.SwitchWeatherForTurns(arenaElement.AffectedWeather, arenaElement.TurnTimer);

        if(GameManager.Instance.Enemy.Passives.Contains(arenaElement.AssociatedPassive.passive))
        {
            GameManager.Instance.Enemy.AddFlatPassive((FlatPassiveObject) arenaElement.SecondaryAssociatedPassive.passive);
        }

        return true;
    }
}
