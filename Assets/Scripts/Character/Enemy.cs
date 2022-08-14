using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{ 
    public void DoAwake(EnemyObject enemyObject)
    {
        if(enemyObject != null)
        {
            characterObject = enemyObject;
        }

        stats = new EnemyStats(characterObject.BaseStats, HPText);
    
        InitCharacter();
        ChangeAttacker(Player.Instance);
        transform.position = ((EnemyObject) characterObject).Position;
    }
}
