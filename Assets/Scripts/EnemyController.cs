using System;
using UnityEngine;

[Serializable]
public class EnemyController
{
    private float _baseSpawnrate = 1f;

    private float _currentSpawnrate;
    private float _movingSpawnrate;
    private float _standingSpawnrate;

    private float _mult_spawnrate = 30f;
    private float _mult_movingSpawnrate = 0.75f;
    private float _mult_standingSpawnrate = 1.5f;

    public float CurrentSpawnrate => _currentSpawnrate;

    public void Init() => _currentSpawnrate = _baseSpawnrate;

    public void UpdateSpawnrates()
    {
        _movingSpawnrate = _baseSpawnrate * _mult_movingSpawnrate;
        _standingSpawnrate = _baseSpawnrate * _mult_standingSpawnrate;
    }

    public void RaiseSpawnrate() => _baseSpawnrate += Time.deltaTime / _mult_spawnrate;

    public void RaiseCurrentSpawnrate(bool isWalking, Timer timer)
    {
        if (isWalking)
        {
            if (_currentSpawnrate < _movingSpawnrate)
            {
                _currentSpawnrate += Time.deltaTime / timer.Enemy_ChangeSpawnrateTime;
            }
            else
            {
                _currentSpawnrate = _movingSpawnrate;
            }
        }
        else
        {
            if (_currentSpawnrate > _standingSpawnrate)
            {
                _currentSpawnrate -= Time.deltaTime / timer.Enemy_ChangeSpawnrateTime;
            }
            else
            {
                _currentSpawnrate = _standingSpawnrate;
            }
        }
    }
}
