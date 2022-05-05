using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        UtilityElementObject utilityElement = (UtilityElementObject) element;

        int healAmount = Random.Range((int) utilityElement.HealAmount, ((int) utilityElement.ExtraValue) + 1);

        GameManager.Instance.Player.Stats.Heal(healAmount, GameManager.Instance.Player);
        
        return true;
    }
}
