using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : AbstractStateManager 
{
    [SerializeField] GameState startingState;

    [Space(15)]
    [ReadOnly, SerializeField] bool reRollSpecific = false;
    public bool ReRollSpecific => reRollSpecific;
    [ReadOnly, SerializeField] ElementObject reRollElement = null;
    public ElementObject ReRollElement => reRollElement;

    [System.NonSerialized] public PlayerTurnGameState playerTurnGameState;
    [System.NonSerialized] public EnemyTurnGameState enemyTurnGameState;
    

    private void Awake()
    {
        currentState = startingState;
        playerTurnGameState = GetComponent<PlayerTurnGameState>();
        enemyTurnGameState = GetComponent<EnemyTurnGameState>();
    }

    public void DealDamageToEverything(AffinityTypes damageType, float value)
    {
        GameManager.Instance.Player.Stats.TakeDamage(value, damageType, GameManager.Instance.Player);
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, GameManager.Instance.Enemy);
        ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
        // minion damage
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
