using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherStateManager : AbstractStateManager 
{
    [SerializeField] WeatherObject startingWeather;
    [ReadOnly, SerializeField] string currState;
    [ReadOnly] public int turnsTillClearWeather = 0;
    [ReadOnly, SerializeField] int turnTimer = -1;
    Dictionary<WeatherObject, WeatherState> getWeatherState = new Dictionary<WeatherObject, WeatherState>();
    public Dictionary<WeatherObject, WeatherState> GetWeatherState => getWeatherState;

    private void Awake()
    {
        getWeatherState = new Dictionary<WeatherObject, WeatherState>();

        foreach(KeyValuePair<string, WeatherObject> pair in GameManager.Instance.WeatherDatabase.GetWeather)
        {
            WeatherState state = gameObject.AddComponent<WeatherState>();
            state.InitWeatherState(pair.Key);
            getWeatherState.Add(pair.Value, state);
        }

        currentState = getWeatherState[startingWeather];
        currState = ((WeatherState) currentState).StateName;
    }

    protected override void OnUpdate()
    {
        if(turnTimer == turnsTillClearWeather - 1)
        {
            ClearWeather();
        }

        currState = ((WeatherState) currentState).StateName;
    }

    public void SwitchWeather(WeatherObject weather)
    {
        SwitchToNextState(getWeatherState[weather]);
        turnTimer = -1;
        turnsTillClearWeather = 0;
    }

    public void SwitchWeatherForTurns(WeatherObject weather, int _turnTimer)
    {
        SwitchToNextState(getWeatherState[weather]);
        turnTimer = _turnTimer;
        turnsTillClearWeather = 0;
    }

    public void ExtendWeather(int addedTurnTimer)
    {
        turnTimer += addedTurnTimer;
    }

    public void SwitchAndExtendWeather(WeatherObject weather, int addedturnTimer)
    {
        SwitchToNextState(getWeatherState[weather]);
        ExtendWeather(addedturnTimer);
    }

    public void ClearWeather()
    {
        SwitchToNextState(getWeatherState[startingWeather]);
        turnTimer = -1;
        turnsTillClearWeather = 0;
    }

}
