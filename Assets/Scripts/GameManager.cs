using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Classes
    private Timer _timer = new Timer();
    private SettlementLogic _settlement = new SettlementLogic();
    private Environment _environment = new Environment();
    #endregion

    #region Settlements
    private bool _isWalking = true;
    private Vector2 _moveDirection = new Vector2(-1, 0);
    private float _playerSpeed = 2f;

    // LT - LastTime
    private float _LT_CollectedWheat = 0f;
    private float _LT_AteWheat = 0f;
    #endregion

    #region Envinronment
    [Header("Envinronment Settings")]
    [SerializeField] private Transform _player;
    [SerializeField] private List<GameObject> _environmentObj = new List<GameObject>();
    [SerializeField] private int _maxObjects = 15;

    private float _LT_CheckedObj = 0f;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void CreateEnvinronment()
    {
        for (int i = 0; i < _maxObjects; ++i)
            SpawnObject();
    }

    private void SpawnObject()
    {
        int objIndex = _environment.RandomIndex(_environmentObj);
        Vector3 spawnPosition = _environment.GetRandomSpawnPosition(_player.transform);

        GameObject newObject = Instantiate(_environmentObj[objIndex], spawnPosition, Quaternion.identity);
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

    private void Awake()
    {

    }

    private void Start()
    {
        CreateEnvinronment();
    }

    private void FixedUpdate()
    {
        _timer.RaiseSurvivedTime();

        // Settlements 
        _settlement.HereWeGo(_isWalking);
        if (_timer.SurvivedTime - _LT_AteWheat >= _timer.WheatEatCD)
            _settlement.EatWheat(_isWalking);
        if (!_isWalking && _timer.SurvivedTime - _LT_CollectedWheat >= _timer.WheatCollectCD)
        {
            _settlement.CollectWheat();
            _LT_CollectedWheat = _timer.SurvivedTime;
        }

        // Env
        if (_timer.SurvivedTime - _LT_CheckedObj >= _timer.RespawnCheckIntervalCD)
        {
            CheckAndDestroyObjects();
            MaintainEnvinronmentCount();
            _LT_CheckedObj = _timer.SurvivedTime;
        }
        if (_isWalking)
            MoveEnvinronment();
    }
}
