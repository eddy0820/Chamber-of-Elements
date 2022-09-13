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
    protected Character character;
    protected float currentHealth;
    protected float currentMana;
    public float CurrentHealth => currentHealth;
    public float CurrentMana => currentMana;
    protected float lastHitDamage;
    public float LastHitDamage => lastHitDamage;

    protected GameObject HPText;

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
    
    public void TakeDamage(float damage, AffinityTypes damageType, Character source)
    {
        TakeDamageNoActions(damage, damageType, source);
        
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

    public void TakeDamageNoActions(float damage, AffinityTypes damageType, Character source)
    {
        if(damage <= 0)
        {
            if(damage < 0)
            {
                Debug.LogWarning("This would heal, just use 'Heal()'");
                return;
            }
            else
            {
                return;
            }
        }

        character.ChangeAttacker(source);

        damage = CalcDamage(damage, damageType, source, (WeatherState) WeatherStateManager.Instance.currentState);

        System.Math.Round(damage, 1);

        currentHealth -= damage;

        HPText.GetComponent<TextMeshProUGUI>().text = currentHealth + " HP";
        HPText.GetComponent<Animator>().SetTrigger("Grow");

        lastHitDamage = damage;

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            DamageIndicatorController.Instance.DoDamageIndicator(damage, character.transform.position);
        }

        HandlePassiveAdditiveRemovalTypes(damageType);
    }

    private float CalcDamage(float damage, AffinityTypes damageType, Character source, WeatherState weather)
    {
        float attackerStength;
        float attackerPotency;
        float attackerSpellPower;
        float defense = stats["Defense"].value;
        float resistance;
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

    public void Heal(float amount)
    {
        if(amount < 0)
        {
            Debug.LogWarning("This would deal damage, just use 'TakeDamage()'");
            return;
        }

        float healScale = stats["HealScale"].value;

        amount = ((healScale / 100) * amount);
        
        if(amount > stats["MaxHealth"].value - currentHealth)
        {
            amount = stats["MaxHealth"].value - currentHealth;
        }

        currentHealth += amount;

        if(currentHealth > stats["MaxHealth"].value)
        {
            currentHealth = stats["MaxHealth"].value;
        }

        HPText.GetComponent<TextMeshProUGUI>().text = currentHealth + " HP";
        HPText.GetComponent<Animator>().SetTrigger("Grow");
    }

    public virtual void Die()
    {
        Debug.Log(character.CharacterObject.Name + " died.");

        foreach(DeathRattleObject dr in character.CharacterObject.DeathRattles)
        {
            dr.Behavior.DoBehavior(character, dr);
        }
    }

    private void HandlePassiveAdditiveRemovalTypes(AffinityTypes damageType)
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
                character.AddDynamicPassive((DynamicPassiveObject) typeEntry.Passive, typeEntry.Value, false, false);
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
