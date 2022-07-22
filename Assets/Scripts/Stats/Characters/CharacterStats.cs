using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public abstract class CharacterStats
{
    protected BaseStatsObject baseStats;
    protected Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
    public Dictionary<string, Stat> Stats => stats;
    Dictionary<StatTypeObject, Stat> getStat = new Dictionary<StatTypeObject, Stat>();
    public Dictionary<StatTypeObject, Stat> GetStat = new Dictionary<StatTypeObject, Stat>();
    protected float currentHealth;
    protected float currentMana;
    public float CurrentHealth => currentHealth;
    public float CurrentMana => currentMana;
    protected float lastHitDamage;
    public float LastHitDamage => lastHitDamage;

    protected void InitializeCharacterStats()
    {
        foreach(BaseStatsObject.BaseStat baseStat in baseStats.Stats)
        {
            Stat stat = new Stat(baseStat.StatType, baseStat.Value);
            stats.Add(baseStat.StatType.Name, stat);
            getStat.Add(baseStat.StatType, stat);
        }

        currentHealth = stats["MaxHealth"].value;
    }
    
    public void TakeDamage(float damage, AffinityTypes damageType, Character character, Character source)
    {
        TakeDamageNoActions(damage, damageType, character, source);
        
        if(damage <= 0)
        {
            return;
        }
     
        if(currentHealth > 0)
        {
            foreach(KeyValuePair<int, System.Action> action in character.actionsToDoOnHit) 
            {
                action.Value.Invoke();
            }
        } 
    }

    public void TakeDamageNoActions(float damage, AffinityTypes damageType, Character character, Character source)
    {
        if(damage <= 0)
        {
            if(damage < 0)
            {
                Debug.Log("This would heal, just use 'Heal()'");
                return;
            }
            else
            {
                return;
            }
        }

        character.ChangeAttacker(source);

        damage = CalcDamage(damage, damageType, character, source, (WeatherState) WeatherStateManager.Instance.currentState);

        System.Math.Round(damage, 1);

        currentHealth -= damage;

        GameObject damageIndicatorUI = GameObject.Instantiate(character.DamageIndicatorPrefab, character.DamageIndicatorPrefab.transform.position, Quaternion.identity, GameManager.Instance.InfoCanvas.transform);
        damageIndicatorUI.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        Debug.Log(character.CharacterObject.Name + " takes " + damage + " " + damageType.ToString() + " damage.");
        Debug.Log(currentHealth);

        lastHitDamage = damage;

        if(currentHealth <= 0)
        {
            Die(character);
        }

        HandlePassiveAdditiveRemovalTypes(character, damageType);
    }

    private float CalcDamage(float damage, AffinityTypes damageType, Character character, Character source, WeatherState weather)
    {
        float attackerStength;
        float attackerPotency;
        float attackerSpellPower;
        float defense = stats["Defense"].value;
        float resistance;
        if(source == null)
        {
            Debug.Log("source is null");
        }
        float attackerWeatherAffinity = source.Stats.getStat[((WeatherState) WeatherStateManager.Instance.currentState).WeatherObject.WeatherAffinity].value;
        float attackerWeatherPotency = source.Stats.getStat[((WeatherState) WeatherStateManager.Instance.currentState).WeatherObject.WeatherPotency].value;

        if(source == null)
        {
            attackerStength = 0;
            attackerPotency = 0;
            attackerSpellPower = 0;

            if(damageType == AffinityTypes.None)
            {
                resistance = 0;
            }
            else
            {
                resistance = getStat[GameManager.Instance.AffinityDatabase.GetAffinity[damageType].AffinityResistance].value;
            }
        }
        else
        {
            attackerStength = source.Stats.stats["Strength"].value;

            if(damageType == AffinityTypes.None)
            {
                attackerPotency = 0;
                attackerSpellPower = 0;
                resistance = 0;
            }
            else
            {
                attackerPotency = source.Stats.getStat[GameManager.Instance.AffinityDatabase.GetAffinity[damageType].AffinityStrength].value;
                attackerSpellPower = source.Stats.getStat[GameManager.Instance.AffinityDatabase.GetAffinity[damageType].AffinitySpellPower].value;
                resistance = getStat[GameManager.Instance.AffinityDatabase.GetAffinity[damageType].AffinityResistance].value;
            }
        }

        return ((damage + attackerStength + attackerPotency + attackerWeatherPotency) - defense) + ((attackerWeatherAffinity/100) * damage) + ((attackerSpellPower/100) * damage) - ((resistance/100) * damage);
    }

    public void Heal(float amount, Character character)
    {
        if(amount < 0)
        {
            Debug.Log("This would deal damage, just use 'TakeDamage()'");
            return;
        }
        
        if(amount > stats["MaxHealth"].value - currentHealth)
        {
            amount = stats["MaxHealth"].value - currentHealth;
        }

        currentHealth += amount;

        Debug.Log(character.CharacterObject.Name + " heals for " + amount + ".");

        if(currentHealth > stats["MaxHealth"].value)
        {
            currentHealth = stats["MaxHealth"].value;
        }

        Debug.Log(character.CharacterObject.Name + " now has " + currentHealth + " health.");
    }

    public virtual void Die(Character character)
    {
        Debug.Log(character.CharacterObject.Name + " died.");

        foreach(DeathRattleObject dr in character.CharacterObject.DeathRattles)
        {
            dr.Behavior.DoBehavior(character, dr);
        }
    }

    private void HandlePassiveAdditiveRemovalTypes(Character character, AffinityTypes damageType)
    {
        List<PassiveObject.ChangeTypeEntry> passivesToAdd = new List<PassiveObject.ChangeTypeEntry>();
        List<PassiveObject> passivesToRemove = new List<PassiveObject>();
        
        foreach(PassiveObject passive in character.Passives)
        {
            foreach(PassiveObject.ChangeTypeEntry typeEntry in passive.AdditiveTypes)
            {
                if(typeEntry.AffinityType == damageType)
                {
                    if(typeEntry.Passive != null)
                    {
                        passivesToAdd.Add(typeEntry);
                    }
                }
            }

            foreach(PassiveObject.ChangeTypeEntry typeEntry in passive.RemovalTypes)
            {
                if(typeEntry.AffinityType == damageType)
                {
                    passivesToRemove.Add(passive);

                    if(typeEntry.Passive != null)
                    {
                        passivesToAdd.Add(typeEntry);
                    }
                }
            } 
        }  
        
        foreach(PassiveObject.ChangeTypeEntry typeEntry in passivesToAdd)
        {
            if(typeEntry.Passive is FlatPassiveObject)
            {
                character.AddFlatPassive((FlatPassiveObject) typeEntry.Passive);
            }
            else if(typeEntry.Passive is DynamicPassiveObject)
            {
                character.AddDynamicPassive((DynamicPassiveObject) typeEntry.Passive, typeEntry.Value, false);
            }
            else if(typeEntry.Passive is ImmunityPassiveObject)
            {
                character.AddImmunityPassive((ImmunityPassiveObject) typeEntry.Passive);
            }  
        }

        foreach(PassiveObject passive in passivesToRemove)
        {
            character.RemovePassive(passive);
        }  
    }
}
