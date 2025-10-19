using System.Collections.Generic;
using UnityEngine;

public class Environment
{
    //private float _nonSpawnRadius = 5f; // - how
    private float spawnRadius = 50f;
    private float _despawnRadius = 50f;

    public int RandomIndex(List<GameObject> objList) => Random.Range(0, objList.Count);

    public Vector3 GetRandomSpawnPosition(Transform player)
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        return new Vector3(randomCircle.x + player.transform.position.x, player.transform.position.y, randomCircle.y + player.transform.position.z);
    }

    public bool CheckDistance(Transform player, Transform obj)
    {
        float distance = Vector3.Distance(player.position, obj.position);
        return distance > _despawnRadius;
    }
}