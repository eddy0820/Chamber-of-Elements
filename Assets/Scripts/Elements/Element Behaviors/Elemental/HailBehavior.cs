using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HailBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() == WeatherStateManager.Instance.GetWeatherState[elementalElement.BehaviorEntries.Weather1].GetType())
        {
            character.Stats.TakeDamage(elementalElement.BehaviorEntries.Float1, GameManager.Instance.mouseElement.element.AffinityType, character, Player.Instance);
        }

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.BehaviorEntries.Passive2.passive);
        }

        return true;
    }
}
