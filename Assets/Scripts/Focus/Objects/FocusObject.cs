using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Focus", menuName = "Focus")]
public class FocusObject : ScriptableObject
{
    [SerializeField] bool damagePlayer;
    [SerializeField] bool damageMinion;
    [SerializeField] bool damageEnemy;
    [SerializeField] ElementObject[] elementsToConsume;

    public void PerformFocus(Character character)
    {
        if(damagePlayer)
        {
            PerformPlayer(character);
        }

        if(damageMinion)
        {
            PerformMinion(character);
        }

        if(damageEnemy)
        {
            PerformEnemy(character);
        }
    }

    private void PerformPlayer(Character character)
    {
        Player.Instance.Stats.TakeDamage(character.Stats.Stats["FocusDamage"].value, character.AffinityType, Player.Instance, character);
    }

    private void PerformMinion(Character character)
    {
        Player.Instance.Minion.Stats.TakeDamage(character.Stats.Stats["FocusDamage"].value, character.AffinityType, Player.Instance.Minion, character);
    }

    private void PerformEnemy(Character character)
    {
        GameManager.Instance.Enemy.Stats.TakeDamage(character.Stats.Stats["FocusDamage"].value, character.AffinityType, GameManager.Instance.Enemy, character);
    }
}
