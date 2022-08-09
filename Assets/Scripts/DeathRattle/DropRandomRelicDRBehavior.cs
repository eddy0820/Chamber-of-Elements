using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropRandomRelicDRBehavior : AbstractDeathRattle
{ 
    public override void DoBehavior(Character character, DeathRattleObject deathRattle)
    {
        int arraySize = deathRattle.BehaviorEntries.RelicList.Length;
        int relic = Random.Range(0, arraySize);
        
        Player.Instance.Relic.CreateRelic(deathRattle.BehaviorEntries.RelicList[relic]);
    }
}
