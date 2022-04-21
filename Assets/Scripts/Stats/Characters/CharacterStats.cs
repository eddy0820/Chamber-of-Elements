using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats
{
    protected BaseStatsObject baseStats;
    protected Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
    public Dictionary<string, Stat> Stats => stats;
    protected int currentHealth;
    protected int currentMana;
    public int generalResistance;
    public int airResistance;
    public int earthResistance;
    public int fireResistance;
    public int waterResistance;
    public int CurrentHealth => currentHealth;
    public int CurrentMana => currentMana;

    protected void InitializeCharacterStats()
    {
        foreach(BaseStatsObject.BaseStat baseStat in baseStats.Stats)
        {
            stats.Add(baseStat.StatType.Name, new Stat(baseStat.StatType, baseStat.Value));
        }

        currentHealth = (int)stats["MaxHealth"].value;
        generalResistance = 0;
        airResistance = 0;
        earthResistance = 0;
        fireResistance = 0;
        waterResistance = 0;
    }
    
    public void TakeDamage(int damage, AffinityTypes damageType, Character characterType)
    {
        switch(damageType)
        {
            case AffinityTypes.Air:
                damage = Calculate(damage, airResistance);
                break;
            case AffinityTypes.Earth:
                damage = Calculate(damage, earthResistance);
                break;
            case AffinityTypes.Fire:
                damage = Calculate(damage, fireResistance);
                break;
            case AffinityTypes.Water:
                damage = Calculate(damage, waterResistance);
                break;
            case AffinityTypes.None:
                damage = Calculate(damage, generalResistance);
                break;
            default:
                Debug.Log("Unrecognized damage type, dealing general damage");
                damage = Calculate(damage, generalResistance);
                break;
        }

        currentHealth -= damage;

        GameObject damageIndicatorUI;

        if(characterType.GetType() == typeof(Player))
        {
            damageIndicatorUI = GameManager.Instance.InfoCanvas.transform.GetChild(2).gameObject;
        }
        else if(characterType.GetType() == typeof(Enemy))
        {
            damageIndicatorUI = GameManager.Instance.InfoCanvas.transform.GetChild(3).gameObject;
        }

        //damageindicator

        Debug.Log(characterType.CharacterObject.Name + " takes " + damage + " " + damageType.ToString() + " damage.");

        if(currentHealth <= 0)
        {
            Die(characterType.CharacterObject.Name);
        }
    }

    private int Calculate(int damage, int resistance)
    {
        //Re do this when deciding on float or int
        float num1 = ((float)resistance)/100;
        float num2 = num1 * (float)damage;
     
        int finalValue = damage - (int)num2;
        return finalValue;
    }

    public void Die(string debugName)
    {
        Debug.Log(debugName + " died.");
    }

    public void DebugTestStats()
    {
        DebugPrintCharacterStats();

        StatModifier modifier = new StatModifier(10, StatModifierTypes.Flat);
        
        DebugTestAddModifiers(modifier);
        DebugPrintCharacterStats();

        DebugTestRemoveModifiers(modifier);
        DebugPrintCharacterStats();
    }  

    private void DebugPrintCharacterStats()
    {
        foreach(KeyValuePair<string, Stat> stat in stats)
        {
            Debug.Log(stat.Key + " value: " + stat.Value.value);
        }
    }

    private void DebugTestAddModifiers(StatModifier modifier)
    {
        foreach(KeyValuePair<string, Stat> stat in stats)
        {
            stat.Value.AddModifier(modifier);
        }
    }

    private void DebugTestRemoveModifiers(StatModifier modifier)
    {    
        foreach(KeyValuePair<string, Stat> stat in stats)
        {
            stat.Value.RemoveModifier(modifier);
        }
    }
}
