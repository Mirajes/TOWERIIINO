using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyController
{
    #region Spawnrate
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
                _currentSpawnrate += Time.deltaTime / timer.Enemy_SpawnrateTimeToChange;
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
                _currentSpawnrate -= Time.deltaTime / timer.Enemy_SpawnrateTimeToChange;
            }
            else
            {
                _currentSpawnrate = _standingSpawnrate;
            }
        }
    }
    #endregion

    #region Spawn

    public Vector3 EnemyPos(Transform player, Transform safeZone, Transform dangerZone)
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float randomDistance = UnityEngine.Random.Range(safeZone.localScale.x, dangerZone.localScale.x);

        Vector3 spawnPosition = player.transform.position + new Vector3(
            randomDirection.x, 0, randomDirection.y) * randomDistance;

        return spawnPosition;
    }

    public int RandomIndex(List<GameObject> enemiesList)
    {
        return UnityEngine.Random.Range(0, enemiesList.Count);
    }

    #endregion
}
