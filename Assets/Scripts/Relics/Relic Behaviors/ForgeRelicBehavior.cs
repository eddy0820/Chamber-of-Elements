using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForgeRelicBehavior : AbstractRelicBehavior
{ 
    public override void OnBattleBegin(RelicObject relic) {}

    public override void OnRelicEquip(RelicObject relic)
    {
        Player.Instance.UnlockRecipeSet(relic.AssociatedRecipeSet);
    }

    public override void OnRelicUnEquip(RelicObject relic)
    {
        Player.Instance.LockRecipeSet(relic.AssociatedRecipeSet);
    }
}
