using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherStateManager : AbstractStateManager 
{
    public static WeatherStateManager Instance {get; private set; }

    [SerializeField] WeatherObject startingWeather;
    public WeatherObject StartingWeather => startingWeather;
    [ReadOnly, SerializeField] string currState;
    [ReadOnly, SerializeField] int turnTimer = -1;

    SpriteRenderer weatherSprite;

    Dictionary<WeatherObject, WeatherState> getWeatherState = new Dictionary<WeatherObject, WeatherState>();
    public Dictionary<WeatherObject, WeatherState> GetWeatherState => getWeatherState;

    private void Awake()
    {
        Instance = this;
        
        weatherSprite = GetComponentInChildren<SpriteRenderer>();
        getWeatherState = new Dictionary<WeatherObject, WeatherState>();

        foreach(KeyValuePair<string, WeatherObject> pair in GameManager.Instance.WeatherDatabase.GetWeather)
        {
            WeatherState state = gameObject.AddComponent<WeatherState>();
            state.InitWeatherState(pair.Key, pair.Value);
            getWeatherState.Add(pair.Value, state);
        }

        SwitchToNextState(getWeatherState[startingWeather]);

        currState = ((WeatherState) currentState).StateName;
        weatherSprite.sprite = startingWeather.WeatherTexture;
    }

    protected override void OnUpdate()
    {
        if(turnTimer == GameManager.Instance.TurnCounter)
        {
            ClearWeather();
        }   
    }

    public void SwitchWeather(WeatherObject weather)
    {
        SwitchToNextState(getWeatherState[weather]);

        turnTimer = -1;

        currState = ((WeatherState) currentState).StateName;
        weatherSprite.sprite = weather.WeatherTexture;
    }

    public void SwitchWeatherForTurns(WeatherObject weather, int _turnTimer)
    {
        SwitchToNextState(getWeatherState[weather]);

        turnTimer = GameManager.Instance.TurnCounter + _turnTimer + 1;

        currState = ((WeatherState) currentState).StateName;
        weatherSprite.sprite = weather.WeatherTexture;
    }

    public void ExtendWeather(int addedTurnTimer)
    {
        turnTimer += addedTurnTimer;
    }

    public void SwitchAndExtendWeather(WeatherObject weather, int addedturnTimer)
    {
        SwitchToNextState(getWeatherState[weather]);
        ExtendWeather(addedturnTimer);

        currState = ((WeatherState) currentState).StateName;
        weatherSprite.sprite = weather.WeatherTexture;
    }

    public void ClearWeather()
    {
        SwitchToNextState(getWeatherState[startingWeather]);
        turnTimer = -1;

        currState = ((WeatherState) currentState).StateName;
        weatherSprite.sprite = startingWeather.WeatherTexture;
    }

}
