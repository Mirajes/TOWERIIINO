using UnityEngine;

[CreateAssetMenu(fileName = "SOEnemy", menuName = "Scriptable Objects/SOEnemy")]
public class SOEnemy : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float _enemySpeed;
    [SerializeField] private int _enemyDamage;

    // ниже геттер (очень круто)
    public string EnemyName => _enemyName;
}
