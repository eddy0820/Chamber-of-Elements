using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Stats", menuName = "Stats/Base Stats")]
public class BaseStatsObject : ScriptableObject
{
    [SerializeField] List<BaseStat> stats = new List<BaseStat>();
    [SerializeField] List<BaseStat> focusStats = new List<BaseStat>();
    [SerializeField] List<BaseStat> resistanceStats = new List<BaseStat>();
    [SerializeField] List<BaseStat> potencyStats = new List<BaseStat>();
    [SerializeField] List<BaseStat> spellPowerStats = new List<BaseStat>();
    [SerializeField] List<BaseStat> weatherAffinityStats = new List<BaseStat>();
    [SerializeField] List<BaseStat> weatherPotencyStats = new List<BaseStat>();

    public List<BaseStat> Stats => stats.Concat(focusStats).Concat(resistanceStats).Concat(potencyStats).Concat(spellPowerStats).Concat(weatherAffinityStats).Concat(weatherPotencyStats).ToList();

    [Serializable]
    public class BaseStat
    {
        [SerializeField] StatTypeObject statType = null;

        [SerializeField] float value;

        public StatTypeObject StatType => statType;
        public float Value => value;

        /*public BaseStat(StatTypeObject _statType)
        {
            statType = _statType;
            value = -1;
        }

        public override bool Equals(System.Object obj)
        {
            if(obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else 
            {
                StatTypeObject statTypeIn = ((BaseStat) obj).statType;

                return statType.Name.Equals(statTypeIn.Name);
            }
        }

        public override int GetHashCode()
        {
            throw new Exception("Sorry I don't know what GetHashCode should do for BaseStats");
        }*/
    }
}

