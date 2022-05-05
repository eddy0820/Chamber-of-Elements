using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weather", menuName = "Weather")]
public class WeatherObject : ScriptableObject
{
    [SerializeField] new string name = "New Weather Name"; 
    public string Name => name;
}
