using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Database", menuName = "Databases/Character")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] PlayerObject[] playerCharacters;
    public PlayerObject[] PlayerCharacters => playerCharacters;
    [SerializeField] EnemyObject[] enemyCharacters;
    public EnemyObject[] EnemyCharacters => enemyCharacters;
}
