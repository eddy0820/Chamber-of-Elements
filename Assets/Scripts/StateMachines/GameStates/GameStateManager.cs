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
    

    private void Awake()
    {
        Instance = this;

        currentState = startingState;
        playerTurnGameState = GetComponent<PlayerTurnGameState>();
        enemyTurnGameState = GetComponent<EnemyTurnGameState>();
    }

    public void DealDamageToEverything(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, Player.Instance);
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, GameManager.Instance.Enemy);
        GameManager.Instance.Minion.Stats.TakeDamage(value, damageType, GameManager.Instance.Minion);
        ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
        // minion damage
    }

    //  This needs to be addressed when theres more minion slot and enemy slots, you need to come up with a way to know which enemy class is which enemy
    //  Damage needs to be done passing in a "source" character
    public void DealDamageToEverythingExceptEnemy(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, Player.Instance);
        GameManager.Instance.Minion.Stats.TakeDamage(value, damageType, GameManager.Instance.Minion);
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
