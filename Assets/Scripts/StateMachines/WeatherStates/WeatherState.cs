using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherState : State
{
    [ReadOnly, SerializeField] string stateName;
    public string StateName => stateName;
    
    public override State RunCurrentState()
    {
        return this;
    }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public void InitWeatherState(string _stateName)
    {
        stateName = _stateName;
    }
}
