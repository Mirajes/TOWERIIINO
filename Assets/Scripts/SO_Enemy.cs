using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private string _enemyType;

    [SerializeField] private int _enemyHealth;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private int _goldReward;

    public string EnemyName => _enemyName;
    public string EnemyType => _enemyType;
    public int EnemyHealth => _enemyHealth;
    public float EnemySpeed => _enemySpeed;
    public int EnemyDamage => _enemyDamage;
    public int GoldReward => _goldReward;
}