using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardianFocusBehavior : AbstractFocusBehavior
{ 
    public override void PerformFocus(FocusObject focus, Character character)
    {
        if(GameManager.Instance.Enemy.AffinityType == focus.BehaviorEntries.Affinity1)
        {
            ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).SwitchAffinity(focus.BehaviorEntries.Affinity2);
            ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).GetComponentInChildren<Animator>().runtimeAnimatorController = focus.BehaviorEntries.AnimController2;
        }
        else
        {
            ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).SwitchAffinity(focus.BehaviorEntries.Affinity1);
            ElementBehaviorUtil.ConvertCharacterEntry(focus.BehaviorEntries.FocusAffectedCharacters[0]).GetComponentInChildren<Animator>().runtimeAnimatorController = focus.BehaviorEntries.AnimController1;
        }

        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }
}
