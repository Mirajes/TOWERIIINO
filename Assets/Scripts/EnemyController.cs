using System;
using UnityEngine;

[Serializable]
public class EnemyController
{
    private float _baseSpawnrate = 1f;

    private float _currentSpawnrate;
    private float _movingSpawnrate;
    private float _standingSpawnrate;

    public void RaiseSpawnrate()
    {
        _baseSpawnrate += Time.deltaTime / 100;
    }
}
