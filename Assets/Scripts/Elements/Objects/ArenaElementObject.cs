using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Arena Element", menuName = "Elements/Arena")]
public class ArenaElementObject : ElementObject
{
    [Header("Weather")]
    [SerializeField] WeatherObject affectedWeather;
    public WeatherObject AffectedWeather => affectedWeather;
    [SerializeField] int turnTimer = -1;
    public int TurnTimer => turnTimer;

    public override void OnAwake()
    {
        SetType(ElementTypes.Arena);
    }
}
