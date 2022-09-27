using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSelection
{
    List<Battle> branches;
    public List<Battle> Branches => branches;

    int numBranches;
    public int NumBranches => numBranches;

    ChapterObject chapterObject;
    public ChapterObject ChapterObject => chapterObject;

    BattleTypes branchBattleType;
    public BattleTypes BranchBattleType => branchBattleType;

    //Battle chosenBattle = null;
    //public Battle ChosenBattle => chosenBattle;

    public BattleSelection(int _numBranches, ChapterObject _chapterObject, List<EnemyObject> enemyPool, BattleTypes _branchBattleType, System.Random rand)
    {
        branches = new List<Battle>();

        numBranches = _numBranches;
        chapterObject = _chapterObject;
        branchBattleType = _branchBattleType;

        for(int i = 0; i < numBranches; i++)
        {
            branches.Add(new Battle(enemyPool.ElementAt(rand.Next(enemyPool.Count)), branchBattleType));
        }
    }
}
