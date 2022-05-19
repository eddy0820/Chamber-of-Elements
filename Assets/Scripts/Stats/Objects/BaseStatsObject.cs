using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Stats", menuName = "Stats/Base Stats")]
public class BaseStatsObject : ScriptableObject
{
    [SerializeField] List<BaseStat> stats = new List<BaseStat>();

    public List<BaseStat> Stats => stats;

    [Serializable]
    public class BaseStat
    {
        [SerializeField] StatTypeObject statType = null;
        [SerializeField] float value;

        public StatTypeObject StatType => statType;
        public float Value => value;
    }
}

