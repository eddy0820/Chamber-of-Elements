using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
       WeatherStateManager.Instance.ClearWeather();
        
        return true;
    }
}
