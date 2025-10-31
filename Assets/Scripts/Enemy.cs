using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _player;
    private Camera _playerCamera;
    [SerializeField] private SO_Enemy _data;

    [SerializeField] private int _enemyHealth;
    private RectTransform _healthPanel;

    public event Action<SO_Enemy, GameObject, int> EnemyDie;

    public void Init(Transform player, Camera PlayerCamera)
    {
        _player = player;
        _enemyHealth = _data.EnemyHealth;
        _playerCamera = PlayerCamera;

        _healthPanel = gameObject.GetComponentInChildren<RectTransform>();
        gameObject.GetComponentInChildren<TMP_Text>().text = _data.EnemyHealth.ToString();
    }

    private void FixedUpdate()
    {
        if (IsEnemyAlive())
        {
            MoveEnemy();
            LookAtCamera();
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
            EnemyDie(_data, gameObject, _enemyHealth);
            gameObject.SetActive(false);
        }
    }

    private void LookAtCamera()
    {
        _healthPanel.LookAt(_playerCamera.transform);
    }
}
