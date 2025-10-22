using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private float _enemySpeed;

    [SerializeField] private float _enemyRewardGold;

    //[SerializeField] private GameObject _enemyPrefab;

    public string EnemyName => _enemyName;
    public int EnemyDamage => _enemyDamage;
    public float EnemySpeed => _enemySpeed;
    public float EnemyRewardGold => _enemyRewardGold;
}
