using TMPro;
using UnityEngine;

public class MeleeEnemy : EnemyLogic
{
    [SerializeField] private SO_Enemy _enemyData;
    private Transform _target;
    private Camera _playerCamera;
    private TMP_Text _healthText;
    private RectTransform _healthBox;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _playerCamera = Camera.main;
        _healthText = GetComponentInChildren<TMP_Text>();
        _healthBox = GetComponentInChildren<RectTransform>();

        Init(_enemyData, _healthText);
    }

    private void Update()
    {
        if (GameManager.IsPaused || GameManager.IsDead) return;

        LookAtCamera(_playerCamera, _healthBox);

        if (IsAlive())
            MoveEnemy(_target, _enemyData.EnemySpeed);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EnemyDeath(_enemyData);
        }
    }

}
