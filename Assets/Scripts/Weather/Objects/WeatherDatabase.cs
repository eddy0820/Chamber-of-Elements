using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weather Database", menuName = "Databases/Weather")]
public class WeatherDatabase : ScriptableObject
{
    [SerializeField] WeatherObject[] weathers;
    Dictionary<string, WeatherObject> getWeather = new Dictionary<string, WeatherObject>();
    public Dictionary<string, WeatherObject> GetWeather => getWeather;

    public void InitWeathers()
    {
        getWeather = new Dictionary<string, WeatherObject>();

        for(int i = 0; i < weathers.Length; i++)
        {
            getWeather.Add(weathers[i].Name, weathers[i]);
        }
    }
}
