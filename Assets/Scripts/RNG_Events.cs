using UnityEngine;

public class RNG_Events
{
    private float _multBase_wheatCollect = 1f;
    private float _mult_wheatCollectBad = 0.5f;
    private float _mult_wheatCollectGood = 1.5f;

    private float _multBase_enemySpawnRate = 1f;
    private float _mult_enemySpawnRate = 1f;

    private int _enemySpawnCount = 1;

    public float MultBase_WheatCollect => _multBase_wheatCollect;
    public float Mult_WheatCollect => _mult_wheatCollectGood;
    public float MultBase_EnemySpawnrate => _multBase_enemySpawnRate;
    public float Mult_EnemySpawnrate => _mult_enemySpawnRate;
    public int EnemySpawnCount => _enemySpawnCount;

    public void ChangeSpawnRate(float mult) => _multBase_enemySpawnRate += mult;
    public void ChangeEnemySpawnCount(int count) => _enemySpawnCount += count;


    public void HumanWillBornAgain(SettlementLogic settlement)
    {

    }
    public void Plague(SettlementLogic settlement)
    {
        //settlement.KillUnit()
    }

    public void HarvestSeason(SettlementLogic settlement)
    {
        settlement.ChangeWheatMultiplier(_mult_wheatCollectGood);
    }

    public void DroughtSeason(SettlementLogic settlement)
    {
        settlement.ChangeWheatMultiplier(_mult_wheatCollectBad);
    }

    public void ResetSeason(SettlementLogic settlement)
    {
        settlement.ChangeWheatMultiplier(_multBase_wheatCollect);
    }
}
