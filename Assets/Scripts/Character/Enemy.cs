using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{ 
    private void Awake()
    {
        stats = new EnemyStats(characterObject.BaseStats);
    
        InitCharacter();
        ChangeAttacker(Player.Instance);
    }
}
