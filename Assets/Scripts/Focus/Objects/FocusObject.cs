using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Focus", menuName = "Focus")]
public class FocusObject : ScriptableObject
{
    [SerializeField] bool doHeal;

    [Space(15)]
    [SerializeField] bool affectPlayer;
    [SerializeField] bool affectMinion;
    [SerializeField] bool affectEnemy;
    [SerializeField] ElementObject[] elementsToConsume;

    public void PerformFocus(Character character)
    {
        if(affectPlayer)
        {
            Perform(character, Player.Instance);
        }

        if(affectMinion)
        {
            Perform(character, Player.Instance.Minion);
        }

        if(affectEnemy)
        {
            Perform(character, GameManager.Instance.Enemy);
        }
    }

    private void Perform(Character character, Character affected)
    {
        if(doHeal)
        {
            affected.Stats.Heal(character.Stats.Stats["FocusValue"].value, affected);
        }
        else
        {
            affected.Stats.TakeDamage(character.Stats.Stats["FocusValue"].value, character.AffinityType, affected, character);
        }
    }
}
