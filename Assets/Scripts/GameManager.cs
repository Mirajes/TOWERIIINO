using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Camera _playerCamera;
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
    #region Units
    [Header("UnitList")]
    [SerializeField] private SO_Unit _farmer;
    [SerializeField] private int _starterFarmers;
    [SerializeField] private SO_Unit _warrior;
    [SerializeField] private int _starterWarriors;
    [SerializeField] private SO_Unit _builder;
    [SerializeField] private int _starterBuilders;

    private float _hireMultiplier = 2f;

    private SO_Unit Unit_FindAlive()
    {
        SO_Unit aliveUnit;
        if (_settlement.FindUnitCount(_warrior) > 0)
            aliveUnit = _warrior;
        else if (_settlement.FindUnitCount(_builder) > 0)
            aliveUnit = _builder;
        else if (_settlement.FindUnitCount(_farmer) > 0)
            aliveUnit = _farmer;
        else
            return null;

            return aliveUnit;
    }

    private int Unit_ConsumeEnemyHealth(SO_Unit so_unit, int enemyHP)
    {
        if (so_unit == _warrior)
            return enemyHP - so_unit.UnitDamage;
        else if (so_unit == _builder)
            return enemyHP - so_unit.UnitDamage;
        else if (so_unit == _farmer)
            return enemyHP - so_unit.UnitDamage;
        else return 0;
    }
    #endregion

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

    [SerializeField] private float _enemyRespawnCD = 10f;
    private float _enemySpawnRate = 1f;
    private int _enemyBundle = 2;

    private void SpawnEnemy()
    {
        #region EnemyPosition
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(_safeZone.localScale.x, _dangerZone.localScale.x);

        Vector3 spawnPosition = _player.transform.position + new Vector3(
            randomDirection.x, 0, randomDirection.y) * randomDistance;
        #endregion

        int rndIndex = Random.Range(0, _enemyList.Count);

        Enemy enemy = Instantiate(_enemyList[rndIndex], spawnPosition, Quaternion.identity, _enemyFolder);
        enemy.Init(_player, _playerCamera);

        enemy.EnemyDie += OnEnemyDie;

        _spawnedEnemies.Add(enemy);
    }

    private void OnEnemyDie(SO_Enemy so_enemy, GameObject enemy, int enemyHealth)
    {
        int currentEnemyHealth = enemyHealth;

        while (currentEnemyHealth > 0)
        {

            SO_Unit unit_to_kill = Unit_FindAlive();
            int consumeEnemyHealth = Unit_ConsumeEnemyHealth(unit_to_kill, currentEnemyHealth);

            currentEnemyHealth = consumeEnemyHealth;
            _settlement.KillUnit(unit_to_kill, 1);

            GameEndCheck();
        }

        _settlement.AddGold(so_enemy.EnemyRewardGold);
        Destroy(enemy);
    }

    #region ChangeSpawnRate

    private void RaiseSpawnRate()
    {
        _enemySpawnRate += Time.deltaTime / 100 ;
        //print(_enemySpawnRate);
    }

    private float CurrentEnemySpawnCD() => _enemyRespawnCD / _enemySpawnRate;

    #endregion

    #endregion

    #region BuyManager

    public void BuyFarmer() => _settlement.HireUnit(_farmer);
    public void BuyWarrior() => _settlement.HireUnit(_warrior);
    public void BuyBuiler() => _settlement.HireUnit(_builder);

    #endregion

    #region UI
    [Header("UI")]
    [SerializeField] private TMP_Text _goldLabel;
    [SerializeField] private TMP_Text _wheatLabel;
    [SerializeField] private TMP_Text _farmerLabel;
    [SerializeField] private TMP_Text _warriorLabel;
    [SerializeField] private TMP_Text _builderLabel;

    private float _UI_CD = 0.2f;
    private float _LT_UIUpdate = 0f;

    private void UpdateUI()
    {
        _goldLabel.text = _settlement.GoldValue.ToString();
        _wheatLabel.text =  _settlement.WheatCount.ToString();
        _farmerLabel.text = _settlement.FindUnitCount(_farmer).ToString();
        _warriorLabel.text = _settlement.FindUnitCount(_warrior).ToString();
        _builderLabel.text = _settlement.FindUnitCount(_builder).ToString();
    }

    [SerializeField] private GameObject _endScreen;
    #endregion

    #region Game

    private void GameEndCheck()
    {
        int unitCount = _settlement.CountAllUnits();
        if (unitCount <= 0)
        {
            Time.timeScale = 0f;
            _endScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        StopAllCoroutines();

        SceneManager.LoadScene("Game");
        InitGame();
        Time.timeScale = 1f;

    }

    private void InitGame()
    {
        CreateEnvinronment();
        StartCoroutine(EnemySpawner());
        StartCoroutine(UnitHirer());
        _settlement.Init(_farmer, _starterFarmers);
        _settlement.Init(_warrior, _starterWarriors);
        _settlement.Init(_builder, _starterBuilders);
    }

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
        InitGame();
    }

    private void ChangeStatement(InputAction.CallbackContext context)
    {
        _isWalking = !_isWalking;
    }

    private void FixedUpdate()
    {
        _timer.RaiseSurvivedTime();

        #region Settlements
        //_settlement.HereWeGo(_isWalking);

        #region Wheat
        // кушать через корутину нужно чтобы нельзя было прожить по кд проблел для получения пшеницы
        if (_timer.SurvivedTime - _LT_AteWheat >= _timer.WheatEatCD)
        {
            _LT_AteWheat = _timer.SurvivedTime;
            _settlement.EatWheat(_isWalking);
        }

        if (!_isWalking && _timer.SurvivedTime - _LT_CollectedWheat >= _timer.WheatCollectCD)
        {
            _LT_CollectedWheat = _timer.SurvivedTime;
            _settlement.CollectWheatRaw();
        }
        #endregion

        // coroutine
        // unit hire in coroutine
        #endregion

        #region Enemy
        RaiseSpawnRate();
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

        #region UI
        if (_timer.SurvivedTime - _LT_UIUpdate >= _UI_CD)
        {
            _LT_UIUpdate = _timer.SurvivedTime;
            UpdateUI();
        }
        #endregion
    }

    private void OnDisable()
    {
        _input.actions["ChangeStatement"].performed -= ChangeStatement;
    }

    #region Coroutines
    private IEnumerator EnemySpawner()
    {
        while (true)
        {
            for (int i = 0; i < _enemyBundle; i++)
                SpawnEnemy();

            yield return new WaitForSeconds(CurrentEnemySpawnCD());
        }
    }

    private IEnumerator UnitHirer()
    {
        while (true)
        {
            if (!_isWalking)
            {
                yield return new WaitForSeconds(_timer.HireUnitCD / _hireMultiplier);
                _settlement.UnitOrderCheck();
            }

            else if (_isWalking)
            {
                yield return new WaitForSeconds(_timer.HireUnitCD);
                _settlement.UnitOrderCheck();
            }

        }
    }
    #endregion
}
