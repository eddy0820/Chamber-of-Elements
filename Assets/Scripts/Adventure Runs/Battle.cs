using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    EnemyObject enemy;
    public EnemyObject Enemy => enemy;
    BattleTypes battleType;
    public BattleTypes BattleType => battleType;

    public Battle(EnemyObject _enemy, BattleTypes _battleType)
    {
        enemy = _enemy;
        battleType = _battleType;
    }
}
