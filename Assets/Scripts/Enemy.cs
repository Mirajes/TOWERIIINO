using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private SO_Enemy _data;

    private int _enemyHealth;

    public void Init(Transform player, SO_Enemy data)
    {
        _player = player;
        _data = data;
    }

    private void FixedUpdate()
    {
        if (IsEnemyAlive())
        {
            MoveEnemy();
        }
    }

    private bool IsEnemyAlive() => _enemyHealth >= 0;

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _data.EnemySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
