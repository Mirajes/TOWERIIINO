using System.Collections.Generic;
using UnityEngine;

public class Environment
{
    private float _despawnRadius;
    private Transform _envObject;

    public void Init(float DespawnRadius ,Transform EnvObject)
    {
        _despawnRadius = DespawnRadius;
        _envObject = EnvObject;
    }

    public int RandomIndex(List<GameObject> objList) => Random.Range(0, objList.Count);

    public Vector3 GetRandomSpawnPosition(Transform player)
    {
        Vector2 randomPoint = new Vector2(Random.Range(-_envObject.localScale.x, _envObject.localScale.x) / 2, Random.Range(-_envObject.localScale.z, _envObject.localScale.z) / 2);
        Vector3 randomPosition = new Vector3(_envObject.transform.position.x + randomPoint.x, _envObject.transform.position.y, _envObject.position.z + randomPoint.y);
        
        return randomPosition;
    }

    public bool CheckDistance(Transform player, Transform obj)
    {
        float distance = Vector3.Distance(player.position, obj.position);
        return distance > _despawnRadius;
    }
}