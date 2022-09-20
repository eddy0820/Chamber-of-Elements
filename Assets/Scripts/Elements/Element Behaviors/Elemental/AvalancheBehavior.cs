using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvalancheBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        BehaviorScriptEntries be = elementalElement.BehaviorEntries;

        if(WeatherStateManager.Instance.CompareWeatherStates((WeatherState) WeatherStateManager.Instance.currentState, WeatherStateManager.Instance.GetWeatherState[be.Weather1]))
        {
            ElementBehaviorUtil.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.BehaviorEntries.Float1);
        }

        if(character.Passives.Contains(elementalElement.BehaviorEntries.Passive1.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.BehaviorEntries.Passive2.passive);
        }

        return true;
    }
}
