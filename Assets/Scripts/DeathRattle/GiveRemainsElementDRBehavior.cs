using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveRemainsElementDRBehavior : AbstractDeathRattle
{ 
    public override void DoBehavior(Character character, DeathRattleObject deathRattle)
    {
        GameManager.Instance.ElementSlotsInv.Container.elementSlots[0].UpdateSlot(new Element(deathRattle.BehaviorEntries.Element1));
    }
}
