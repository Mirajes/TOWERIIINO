using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Timer
{
    [SerializeField] private float _gameScale = 1f;
    [SerializeField] private float _timeSurvived = 0f;

    [SerializeField] private float _respawnCheckInterval = 2f;

    [SerializeField] private float _wheatCollectCD = 5f;
    [SerializeField] private float _wheatEatCD = 4f;
    [SerializeField] private float _unitHireCD = 3f;

    [SerializeField] private float _enemyRespawnCD = 5f;

    [SerializeField] private float _RNG_MomentCD = 60f;

    // CD - Cooldown
    public float GameScale => _gameScale;
    public float SurvivedTime => _timeSurvived;
    public float RespawnCheckIntervalCD => _respawnCheckInterval;
    public float WheatCollectCD => _wheatCollectCD;
    public float WheatEatCD => _wheatEatCD;
    public float HireUnitCD => _unitHireCD;
    public float EnemyRespawnCD => _enemyRespawnCD;
    public float RNGMomentCD => _RNG_MomentCD;
    
    public void RaiseSurvivedTime()
    {
        _timeSurvived += Time.deltaTime * _gameScale;
    }
}
