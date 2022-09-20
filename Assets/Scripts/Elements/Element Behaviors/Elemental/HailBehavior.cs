using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HailBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        BehaviorScriptEntries be = elementalElement.BehaviorEntries;

        if(WeatherStateManager.Instance.CompareWeatherStates((WeatherState) WeatherStateManager.Instance.currentState, WeatherStateManager.Instance.GetWeatherState[be.Weather1]))
        {
            character.Stats.TakeDamage(elementalElement.BehaviorEntries.Float1, GameManager.Instance.mouseElement.element.AffinityType, Player.Instance);
        }

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.BehaviorEntries.Passive2.passive);
        }

        return true;
    }
}
