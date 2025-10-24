using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    //private float _score

    #region Classes
    private Timer _timer = new Timer();
    private SettlementLogic _settlement = new SettlementLogic();
    private Environment _environment = new Environment();
    private UI _UI = new UI();
    #endregion

    #region Settlements
    [SerializeField] private bool _isWalking = true;
    private Vector2 _moveDirection = new Vector2(-1, 0);
    private float _playerSpeed = 2f;

    // LT - LastTime
    private float _LT_CollectedWheat = 0f;
    private float _LT_AteWheat = 0f;

    #endregion

    #region Envinronment
    [Header("Envinronment Settings")]
    [SerializeField] private Transform _player;
    [SerializeField] private List<GameObject> _environmentObjs = new List<GameObject>();
    [SerializeField] private int _maxObjects = 15;
    [SerializeField] private float _despawnRadius = 30f;

    [SerializeField] private Transform _envFolder;
    [SerializeField] private Transform _envSpawnPlace;

    private float _LT_CheckedObj = 0f;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void CreateEnvinronment()
    {
        for (int i = 0; i < _maxObjects; ++i)
            SpawnObject();
    }

    private void SpawnObject()
    {
        int objIndex = _environment.RandomIndex(_environmentObjs);
        Vector3 spawnPosition = _environment.GetRandomSpawnPosition(_player.transform);

        GameObject newObject = Instantiate(_environmentObjs[objIndex], spawnPosition, Quaternion.identity, _envFolder);
        _spawnedObjects.Add(newObject);
    }

    private void MaintainEnvinronmentCount()
    {
        while (_spawnedObjects.Count < _maxObjects)
            SpawnObject();
    }

    private void CheckAndDestroyObjects()
    {
        for (int i = _spawnedObjects.Count - 1; i >= 0; --i)
        {
            if (_spawnedObjects[i] == null)
            {
                _spawnedObjects.RemoveAt(i);
                continue;
            }

            if (_environment.CheckDistance(_player, _spawnedObjects[i].transform))
            {
                Destroy(_spawnedObjects[i]);
                _spawnedObjects.RemoveAt(i);
            }
        }
    }

    private void MoveEnvinronment()
    {
        for (int i = 0; i < _spawnedObjects.Count; ++i)
        {
            _spawnedObjects[i].transform.position = new Vector3(
                _spawnedObjects[i].transform.position.x + _moveDirection.x * _playerSpeed * Time.deltaTime,
                _spawnedObjects[i].transform.position.y,
                _spawnedObjects[i].transform.position.z + _moveDirection.y * _playerSpeed * Time.deltaTime
                );
        }
    }
    #endregion

    #region Enemy
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();
    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    [SerializeField] private Transform _enemyFolder;

    [SerializeField] private Transform _safeZone;
    [SerializeField] private Transform _dangerZone;
    private float _enemySpawnRate = 1f;

    private void SpawnEnemy()
    {
        #region EnemyPosition
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(_safeZone.localScale.x, _dangerZone.localScale.x);

        Vector3 spawnPosition = _player.transform.position + new Vector3(
            randomDirection.x, 0, randomDirection.y) * randomDistance;

        int rndIndex = Random.Range(0, _enemyList.Count);
        #endregion

        Enemy enemy = Instantiate(_enemyList[rndIndex], spawnPosition, Quaternion.identity, _enemyFolder);
        enemy.Init(_player);

        _spawnedEnemies.Add(enemy);
    }
    #endregion

    #region BuyManager
    #endregion

    private void Awake()
    {
        _environment.Init(_despawnRadius, _envSpawnPlace);
    }

    private void OnEnable()
    {
        // делегат :o
        _input.actions["ChangeStatement"].performed += ChangeStatement;
    }

    private void Start()
    {
        CreateEnvinronment();
        StartCoroutine(EnemySpawner(10));
    }

    private void ChangeStatement(InputAction.CallbackContext context)
    {
        _isWalking = !_isWalking;
    }

    private void FixedUpdate()
    {
        _timer.RaiseSurvivedTime();

        #region Settlements
        _settlement.HereWeGo(_isWalking);
        // кушать через корутину нужно чтобы нельзя было прожить по кд проблел для получения пшеницы
        if (_timer.SurvivedTime - _LT_AteWheat >= _timer.WheatEatCD)
            _settlement.EatWheat(_isWalking);
        if (!_isWalking && _timer.SurvivedTime - _LT_CollectedWheat >= _timer.WheatCollectCD)
        {
            _settlement.CollectWheatRaw();
            _LT_CollectedWheat = _timer.SurvivedTime;
        }
        #endregion

        #region EnvironmentUpdate
        // Env
        if (_timer.SurvivedTime - _LT_CheckedObj >= _timer.RespawnCheckIntervalCD)
        {
            CheckAndDestroyObjects();
            MaintainEnvinronmentCount();
            _LT_CheckedObj = _timer.SurvivedTime;
        }
        if (_isWalking)
            MoveEnvinronment();
        #endregion



    }

    private void OnDisable()
    {
        _input.actions["ChangeStatement"].performed -= ChangeStatement;
    }

    #region Coroutines
    private IEnumerator EnemySpawner(int enemyCount)
    {
        int currentEnemies = 0;
        while (currentEnemies < enemyCount)
        {
            currentEnemies += 1;
            SpawnEnemy();
            yield return new WaitForSeconds(_enemySpawnRate);
        }

    }
    #endregion
}
