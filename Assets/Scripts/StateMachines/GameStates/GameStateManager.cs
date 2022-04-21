using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : AbstractStateManager 
{
    [System.NonSerialized] public PlayerTurnGameState playerTurnGameState;
    [System.NonSerialized] public EnemyTurnGameState enemyTurnGameState;

    private void Awake()
    {
        playerTurnGameState = (PlayerTurnGameState) currentState;
        enemyTurnGameState = GetComponent<EnemyTurnGameState>();
    }
}
