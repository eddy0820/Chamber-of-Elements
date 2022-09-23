using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField, ReadOnly] PlayerObject player = null;
    public PlayerObject Player => player;
    GameObject playerGO;
    public GameObject PlayerGO => playerGO;

    [SerializeField, ReadOnly] EnemyObject enemy = null;
    public EnemyObject Enemy => enemy;
    GameObject enemyGO;
    public GameObject EnemyGO => enemyGO;

    [SerializeField, ReadOnly] GameModes mode;
    public GameModes Mode => mode;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        mode = GameModes.Battle;
    }

    public void SetPlayer(GameObject go)
    {
        playerGO = go;
        player = playerGO.GetComponent<CharacterSelectionCardHolder>().CharacterObject as PlayerObject;
    }

    public void SetPlayer(PlayerObject _player)
    {
        player = _player;
    }

    public void UnSetPlayer()
    {
        playerGO = null;
        player = null;
    }

    public void SetEnemy(GameObject go)
    {
        enemyGO = go;
        enemy = enemyGO.GetComponent<CharacterSelectionCardHolder>().CharacterObject as EnemyObject;
    }

    public void SetEnemy(EnemyObject _enemy)
    {
        enemy = _enemy;
    }

    public void UnSetEnemy()
    {
        enemyGO = null;
        enemy = null;
    }

    public void SetGameMode(GameModes _mode)
    {
        mode = _mode;
    }
}
