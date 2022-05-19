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
            stats.Add(baseStat.StatType.Name, new Stat(baseStat.StatType, baseStat.Value));
        }

        currentHealth = stats["MaxHealth"].value;
    }
    
    public void TakeDamage(float damage, AffinityTypes damageType, Character character)
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

        switch(damageType)
        {
            case AffinityTypes.Air:
                damage = Calculate(damage, stats["AirResistance"].value);
                break;
            case AffinityTypes.Earth:
                damage = Calculate(damage, stats["EarthResistance"].value);
                break;
            case AffinityTypes.Fire:
                damage = Calculate(damage, stats["FireResistance"].value);
                break;
            case AffinityTypes.Water:
                damage = Calculate(damage, stats["WaterResistance"].value);
                break;
            case AffinityTypes.None:
                damage = Calculate(damage, stats["GeneralResistance"].value);
                break;
            default:
                Debug.Log("Unrecognized damage type, dealing general damage");
                damage = Calculate(damage, stats["GeneralResistance"].value);
                break;
        }

        System.Math.Round(damage, 1);

        currentHealth -= damage;

        GameObject damageIndicatorUI = GameObject.Instantiate(character.DamageIndicatorPrefab, character.DamageIndicatorPrefab.transform.position, Quaternion.identity, GameManager.Instance.InfoCanvas.transform);
        damageIndicatorUI.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        Debug.Log(character.CharacterObject.Name + " takes " + damage + " " + damageType.ToString() + " damage.");

        lastHitDamage = damage;

        if(currentHealth <= 0)
        {
            Die(character.CharacterObject.Name);
        }

        HandlePassiveAdditiveRemovalTypes(character, damageType);
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

    private float Calculate(float damage, float resistance)
    {
        float num1 = resistance/100;
        float num2 = num1 * damage;
     
        float finalValue = damage - num2;

        if(finalValue < 1)
        {
            return 1;
        }

        return finalValue;
    }

    public void Die(string debugName)
    {
        Debug.Log(debugName + " died.");
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
