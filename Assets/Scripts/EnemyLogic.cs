using UnityEngine;

public class EnemyLogic
{
    private Timer _timer = new ();
    private float _enemySpawnRate = 1f;

    private float _LT_enemySpawned = 0f;

    public void SpawnEnemyGroup()
    {
        if (_timer.SurvivedTime - _LT_enemySpawned >= _timer.EnemyRespawnCD / _enemySpawnRate)
        {

        }
    }
}
