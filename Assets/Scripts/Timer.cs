using UnityEngine;

public class Timer
{
    private float _wheatCollect = 3f;
    private float _wheatEat = 10f;

    private float _enemyRespawn = 5f;

    private float _RNGMoment = 30f;

    private float _timeSurvived = 0f;

    public float WheatCollectCD => _wheatCollect;
    public float WheatEatCD => _wheatEat;
    public float EnemyRespawnCD => _enemyRespawn;
    public float RNGMomentCD => _RNGMoment;
    public float TimeSurvived => _timeSurvived;


    public void RaiseSurvivedTimer()
    {
        _timeSurvived += Time.deltaTime;
    }

}
