using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chapter", menuName = "Chapter")]
public class ChapterObject : ScriptableObject
{
    [SerializeField] new string name;
    public string Name => name;
    [SerializeField] Sprite background;
    public Sprite Background => background;

    [Space(15)]
    
    [SerializeField] BattleMinMax firstHalfBattleRange;
    public BattleMinMax FirstHalfBattleRange => firstHalfBattleRange;
    [SerializeField] BattleMinMax firstHalfEliteBattleRange;
    public BattleMinMax FirstHalfEliteBattleRange => firstHalfEliteBattleRange;
    [SerializeField] BattleMinMax secondHalfBattleRange;
    public BattleMinMax SecondHalfBattleRange => secondHalfBattleRange;
    [SerializeField] BattleMinMax secondHalfEliteBattleRange;
    public BattleMinMax SecondHalfEliteBattleRange => secondHalfEliteBattleRange;

    [Space(15)]

    [SerializeField] EnemyObject firstBattleEnemy;
    public EnemyObject FirstBattleEnemy => firstBattleEnemy;
    [SerializeField] List<EnemyObject> battleEnemyPool;
    public List<EnemyObject> BattleEnemyPool => battleEnemyPool;
    [SerializeField] List<EnemyObject> eliteBattleEnemyPool;
    public List<EnemyObject> EliteBattleEnemyPool => eliteBattleEnemyPool;
    [SerializeField] List<EnemyObject> miniBossEnemyPool;
    public List<EnemyObject> MiniBossEnemyPool => miniBossEnemyPool;
    [SerializeField] List<EnemyObject> bossEnemyPool;
    public List<EnemyObject> BossEnemyPool => bossEnemyPool;
    
    [System.Serializable]
    public class BattleMinMax
    {
        [SerializeField] int min;
        public int Min => min;

        [SerializeField] int max;
        public int Max => max;
    }
}
