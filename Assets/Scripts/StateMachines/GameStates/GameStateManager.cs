using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : AbstractStateManager 
{
    public static GameStateManager Instance {get; private set; }
    
    [SerializeField] GameState startingState;

    [Space(15)]
    [ReadOnly, SerializeField] bool reRollSpecific = false;
    public bool ReRollSpecific => reRollSpecific;
    [ReadOnly, SerializeField] ElementObject reRollElement = null;
    public ElementObject ReRollElement => reRollElement;

    [System.NonSerialized] public PlayerTurnGameState playerTurnGameState;
    [System.NonSerialized] public EnemyTurnGameState enemyTurnGameState;
    [System.NonSerialized] public MinionTurnGameState minionTurnGameState;
    

    private void Awake()
    {
        Instance = this;

        currentState = startingState;
        playerTurnGameState = GetComponent<PlayerTurnGameState>();
        enemyTurnGameState = GetComponent<EnemyTurnGameState>();
        minionTurnGameState = GetComponent<MinionTurnGameState>();
    }

    public void SetReRollSpecificNextTurn(ElementObject element)
    {
        reRollSpecific = true;
        reRollElement = element;
    }

    public void UnsetReRollSpecificNextTurn()
    {
        reRollSpecific = false;
        reRollElement = null;
    }
}
