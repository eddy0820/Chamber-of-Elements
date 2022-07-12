using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvalancheBehavior : AbstractElementBehavior
{ 
    public override bool DoBehavior(ElementObject element, Character character)
    {
        ElementalElementObject elementalElement = (ElementalElementObject) element;

        if(WeatherStateManager.Instance.currentState.GetType() == WeatherStateManager.Instance.GetWeatherState[GameManager.Instance.WeatherDatabase.GetWeather["Hailstorm"]].GetType())
        {
            ElementBehaviorUtil.DealDamageToEverything(GameManager.Instance.mouseElement.element.AffinityType, elementalElement.ExtraValue);
        }

        if(character.Passives.Contains(elementalElement.AssociatedPassive.passive))
        {
            character.AddFlatPassive((FlatPassiveObject) elementalElement.SecondaryAssociatedPassive.passive);
        }

        return true;
    }
}
