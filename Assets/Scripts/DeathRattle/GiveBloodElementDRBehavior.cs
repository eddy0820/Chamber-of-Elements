using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveBloodElementDRBehavior : AbstractDeathRattle
{ 
    public override void DoBehavior(Character character, DeathRattleObject deathRattle)
    {
        GameManager.Instance.ElementSlotsInv.Container.elementSlots[0].UpdateSlot(new Element(deathRattle.AssociatedElement));
    }
}
