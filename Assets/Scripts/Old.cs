//using System.Collections.Generic;
//using System;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class GameManager : MonoBehaviour
//{
//    private Timer _timer = new Timer();
//    private SettlementLife _settlementLife = new SettlementLife();
//    private EnemiesLogic _enemiesLogic = new EnemiesLogic();

//    [Header("UnitList")]
//    #region Units
//    [SerializeField] private SOUnit _farmer;
//    [SerializeField] private SOUnit _warrior;
//    #endregion

//    #region StarterUnitsCount
//    private int _starterFarmers = 15;
//    private int _starterWarriors = 10;
//    #endregion

//    #region SettlementVar
//    private int _wheatCount = 0;
//    private bool _isWalking = false;

//    //private int _currentScore = 0;

//    // LT - LastTime
//    private float _LT_WheatCollect;
//    private float _LT_WheatAte;
//    #endregion

//    private void Awake()
//    {
//        _LT_WheatCollect = _timer.TimeSurvived;
//        _LT_WheatAte = _timer.TimeSurvived;
//    }

//    private void Start()
//    {
//        // todo
//        _settlementLife.Initialize(_farmer, _starterFarmers);
//        _settlementLife.Initialize(_warrior, _starterWarriors);
//    }

//    private void FixedUpdate()
//    {
//        _timer.RaiseSurvivedTimer();
//        ActionByTimer();
//        _settlementLife.Update();
//    }

//    private void ActionByTimer()
//    {
//        if (!_isWalking)
//        {
//            if (_timer.TimeSurvived - _LT_WheatCollect >= _timer.WheatCollectCD)
//            {
//                //фулл переписать, чтобы не искать а суммировать кол-во юнитов * их число фарма
//                Unit unit = _settlementLife.Settlements.Find(x => x.UnitType == _farmer);
//                _wheatCount += unit.UnitCount;
//                _LT_WheatCollect = _timer.TimeSurvived;
//                Debug.Log($"pshenitca = {_wheatCount}");
//            }
//        }

//        if (_timer.TimeSurvived - _LT_WheatAte >= _timer.WheatEatCD)
//        {

//            _LT_WheatAte = _timer.TimeSurvived;
//        }


//        // todo
//    }

//    private void isDead()
//    {
//        if (_settlementLife.AllUnitCount <= 0)
//        {
//            Time.timeScale = 0;
//            //EditorApplication.Exit(0);
//        }
//    }

//    /////////////////

//    public void ChangeStatement(InputAction.CallbackContext context)
//    {

//    }

//}

//using UnityEngine;

//public class EnemiesLogic
//{
//    private Timer _timer = new Timer();

//    private float _spawnRate = 1f;

//    public void SpawnEnemy()
//    {

//    }
//}
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//[Serializable]
//public class SettlementLife
//{
//    [SerializeField] private List<Unit> _settlements = new List<Unit>();
//    private int _allUnitCount = 0;


//    [SerializeField] private List<SOUnit> _unitOrder = new List<SOUnit>();
//    private float _timer = 0f;
//    private float _hireTimePerUnit = 3f;

//    public List<Unit> Settlements => _settlements;
//    public int AllUnitCount => _allUnitCount;

//    public void Initialize(SOUnit unitType, int count)
//    {
//        if (_settlements.FindAll(x => x.UnitType == unitType).Count < 1)
//        {
//            _settlements.Add(new Unit(unitType, count));
//            AddUnitToAll(unitType);
//        }


//    }

//    public void HireUnit(SOUnit unit)
//    {
//        //if (_settlements.Contains(unit))
//        _unitOrder.Add(unit);
//    }

//    public void Update()
//    {
//        if (_unitOrder.Count <= 0) return;

//        _timer += Time.deltaTime;

//        if (_timer >= _hireTimePerUnit)
//        {
//            if (_settlements.FindAll(x => x.UnitType == _unitOrder[0]).Count < 0)
//                Initialize(_unitOrder[0], 0);

//            _settlements.Find(x => x.UnitType == _unitOrder[0]).AddUnit(1);
//            AddUnitToAll(_unitOrder[0]);
//            _unitOrder.RemoveAt(0);
//            _timer = 0f;
//        }
//    }

//    private void AddUnitToAll(SOUnit unitType)
//    {
//        int allUnits = 0;
//        foreach (var item in _settlements)
//            allUnits += _settlements.Find(x => x.UnitType == unitType).UnitCount;
//        _allUnitCount = allUnits;
//    }

//    public void SettlementLive(bool isWalking)
//    {
//        if (isWalking)
//        {
//            SettlementWalking();
//        }
//        else
//        {
//            SettlementStaying();
//        }
//    }

//    private void SettlementStaying()
//    {

//    }

//    private void SettlementWalking()
//    {

//    }
//}
//using UnityEngine;

//[CreateAssetMenu(fileName = "SOUnit", menuName = "Scriptable Objects/SOUnit")]
//public class SOUnit : ScriptableObject
//{
//    [SerializeField] private string _unitName;
//    //[SerializeField] private GameObject _unitPrefab;
//    [SerializeField] private int _unitWheatConsume;
//    [SerializeField] private int _unitDamage;

//    [SerializeField] private int _unitWheatFarm;

//    public string UnitName => _unitName;
//    public int UnitWheatConsume => _unitWheatConsume;
//    public int UnitDamage => _unitDamage;
//    public int UnitWheatFarm => _unitWheatFarm;
//}
//using System;

//[Serializable]
//public class Unit
//{
//    private SOUnit _unitType;
//    private int _count = 0;

//    public SOUnit UnitType => _unitType;
//    public int UnitCount => _count;

//    public Unit(SOUnit unitType, int count)
//    {
//        _unitType = unitType;
//        _count = count;
//    }

//    public void AddUnit(int addCount)
//    {
//        _count += addCount;
//    }

//    public void RemoveUnit(int removeCount)
//    {
//        _count -= removeCount;
//    }
//}
//using UnityEngine;

//[CreateAssetMenu(fileName = "SOEnemy", menuName = "Scriptable Objects/SOEnemy")]
//public class SOEnemy : ScriptableObject
//{
//    [SerializeField] private string _enemyName;
//    [SerializeField] private GameObject _enemyPrefab;

//    [SerializeField] private float _enemySpeed;
//    [SerializeField] private int _enemyDamage;

//    // ниже геттер (очень круто)
//    public string EnemyName => _enemyName;
//}
//using UnityEngine;

//public class Timer
//{
//    private float _wheatCollect = 3f;
//    private float _wheatEat = 10f;

//    private float _enemyRespawn = 5f;

//    private float _RNGMoment = 30f;

//    private float _timeSurvived = 0f;

//    public float WheatCollectCD => _wheatCollect;
//    public float WheatEatCD => _wheatEat;
//    public float EnemyRespawnCD => _enemyRespawn;
//    public float RNGMomentCD => _RNGMoment;
//    public float TimeSurvived => _timeSurvived;


//    public void RaiseSurvivedTimer()
//    {
//        _timeSurvived += Time.deltaTime;
//    }

//}
