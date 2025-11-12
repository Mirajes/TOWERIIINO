using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Classes
    private Timer _timer = new Timer();
    private SettlementLogic _settlementLogic = new SettlementLogic();
    private UI _UI = new UI();
    #endregion

    #region Game
    [SerializeField] private Transform _player;
    private static bool _isPaused = false;
    private static bool _isDead = false;

    public static bool IsPaused => _isPaused;
    public static bool IsDead => _isDead;
    #endregion

    #region UI
    [Header("UI")]

    [SerializeField] private TMP_Text _wheatLabel;
    [SerializeField] private TMP_Text _goldLabel;
    [SerializeField] private TMP_Text _farmerLabel;
    [SerializeField] private TMP_Text _warriorLabel;
    [SerializeField] private TMP_Text _builderLabel;

    private IEnumerator UI_Updater()
    {
        while (!_isDead)
        {
            _UI.UI_Updater(_settlementLogic, _farmerUnit, _warriorUnit, _builderUnit);
            yield return new WaitForSeconds(_timer.CD_UI_Update);
        }
    }
    #endregion

    #region Settlements
    [Header("Settlements")]
    [SerializeField] private SO_Unit _farmerUnit;
    [SerializeField] private int _starterFarmers = 25;
    [SerializeField] private SO_Unit _warriorUnit;
    [SerializeField] private int _starterWarriors = 15;
    [SerializeField] private SO_Unit _builderUnit;
    [SerializeField] private int _starterBuilders = 0;

    public void HireUnit(SO_Unit so_unit)
    {
        _settlementLogic.HireUnit(so_unit);
        _UI.UI_Updater(_settlementLogic, _farmerUnit, _warriorUnit, _builderUnit);
    }
    #endregion

    #region Enemy
    [Header("Enemy")]
    [SerializeField] private GameObject _safeZone;
    [SerializeField] private GameObject _dangerZone;
    #endregion

    private void Awake()
    {
        StopAllCoroutines();

        _UI.Init_UI(_wheatLabel, _goldLabel, _farmerLabel, _warriorLabel, _builderLabel);

        _settlementLogic.InitUnit(_farmerUnit, _starterFarmers);
        _settlementLogic.InitUnit(_warriorUnit, _starterWarriors);
        _settlementLogic.InitUnit(_builderUnit, _starterBuilders);
    }

    private void Start()
    {
        //Instantiate(_meleeEnemy, EnemyLogic.RandomEnemyPos(_player, _safeZone.transform, _dangerZone.transform), Quaternion.identity);
        StartCoroutine(UI_Updater());
        StartCoroutine(_settlementLogic.UnitUpdater(_timer));
        StartCoroutine(_settlementLogic.WheatCollectUpdater(_timer));
        StartCoroutine(_settlementLogic.WheatEatUpdater(_timer));
    }

    private void Update()
    {
        if (_isPaused || _isDead) return;

        _timer.RaiseElapsedTime(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E) && CameraController.IsInteract)
            Debug.Log("clicked");
    }
}
