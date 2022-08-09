using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvalancheBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() == WeatherStateManager.Instance.GetWeatherState[elementalElement.BehaviorEntries.Weather1].GetType())
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
