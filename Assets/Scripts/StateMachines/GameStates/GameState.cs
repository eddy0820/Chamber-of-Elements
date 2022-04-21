using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : State 
{
    protected GameStateManager gameStateManager;

    private void Awake()
    {
        gameStateManager = GetComponent<GameStateManager>();
    }
}
