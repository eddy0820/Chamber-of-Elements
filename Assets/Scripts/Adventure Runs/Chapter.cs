using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Chapter
{
    ChapterObject chapterObject;
    public ChapterObject ChapterObject => chapterObject;

    int firstHalfNumBattles;
    int firstHalfNumEliteBattles;
    int secondHalfNumBattles;
    int secondHalfNumEliteBattles;

    Queue<Battle> battles;
    public Queue<Battle> Battles => battles;

    public Chapter(ChapterObject _chapterObject, int _firtHalfNumBattles, int _firtHalfNumEliteBattles, int _secondHalfNumBattles, int _secondHalfNumEliteBattles)
    {
        System.Random rand = new System.Random();

        chapterObject = _chapterObject;
        firstHalfNumBattles = _firtHalfNumBattles;
        firstHalfNumEliteBattles = _firtHalfNumEliteBattles;
        secondHalfNumBattles = _secondHalfNumBattles;
        secondHalfNumEliteBattles = _secondHalfNumEliteBattles;

        battles = new Queue<Battle>();

        battles.Enqueue(new Battle(chapterObject.FirstBattleEnemy, BattleTypes.Starting));

        for(int i = 0; i < firstHalfNumBattles; i++)
        {
            battles.Enqueue(new Battle(chapterObject.BattleEnemyPool.ElementAt(rand.Next(chapterObject.BattleEnemyPool.Count)), BattleTypes.Battle));
        }

        for(int i = 0; i < firstHalfNumEliteBattles; i++)
        {
            battles.Enqueue(new Battle(chapterObject.EliteBattleEnemyPool.ElementAt(rand.Next(chapterObject.EliteBattleEnemyPool.Count)), BattleTypes.EliteBattle));
        }

        battles.Enqueue(new Battle(chapterObject.MiniBossEnemyPool.ElementAt(rand.Next(chapterObject.MiniBossEnemyPool.Count)), BattleTypes.MiniBoss));

        for(int i = 0; i < secondHalfNumBattles; i++)
        {
            battles.Enqueue(new Battle(chapterObject.BattleEnemyPool.ElementAt(rand.Next(chapterObject.BattleEnemyPool.Count)), BattleTypes.BattlePlus));
        }

        for(int i = 0; i < secondHalfNumEliteBattles; i++)
        {
            battles.Enqueue(new Battle(chapterObject.EliteBattleEnemyPool.ElementAt(rand.Next(chapterObject.EliteBattleEnemyPool.Count)), BattleTypes.EliteBattlePlus));
        }

        battles.Enqueue(new Battle(chapterObject.BossEnemyPool.ElementAt(rand.Next(chapterObject.BossEnemyPool.Count)), BattleTypes.Boss));
    }
}
