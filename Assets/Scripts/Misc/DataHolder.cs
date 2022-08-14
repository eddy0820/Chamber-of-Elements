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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetPlayer(GameObject go)
    {
        playerGO = go;
        player = playerGO.GetComponent<CharacterSelectionCardHolder>().characterObject as PlayerObject;
    }

    public void UnSetPlayer()
    {
        playerGO = null;
        player = null;
    }

    public void SetEnemy(GameObject go)
    {
        enemyGO = go;
        enemy = enemyGO.GetComponent<CharacterSelectionCardHolder>().characterObject as EnemyObject;
    }

    public void UnSetEnemy()
    {
        enemyGO = null;
        enemy = null;
    }
}