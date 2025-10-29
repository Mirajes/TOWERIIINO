using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     private Transform _player;
    [SerializeField] private SO_Enemy _data;

    [SerializeField] private int _enemyHealth;

    public event Action<SO_Enemy> EnemyDie;

    public void Init(Transform player)
    {
        _player = player;
        _enemyHealth = _data.EnemyHealth;
    }

    private void FixedUpdate()
    {
        if (IsEnemyAlive())
        {
            MoveEnemy();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool IsEnemyAlive() => _enemyHealth > 0;

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _data.EnemySpeed * Time.deltaTime);
    }

    // этот бред работает только с RigidBody
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyDie(_data);
            Destroy(gameObject);
        }
    }
}
