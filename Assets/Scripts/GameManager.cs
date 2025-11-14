using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Classes
    private Timer _timer = new Timer();
    private SettlementLogic _settlementLogic = new SettlementLogic();
    private UI _UI = new UI();
    private EnemyController _enemyController = new EnemyController();
    #endregion

    #region Game
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerInput _input;

    private int _score = 0;
    private int _enemiesKilled = 0;
    public int Score => _score;
    public int EnemiesKilled => _enemiesKilled;

    private static bool _isPaused = false;
    private static bool _isDead = false;

    public static bool IsPaused => _isPaused;
    public static bool IsDead => _isDead;
    
    private void SetPause(InputAction.CallbackContext context)
    {
        _isPaused = !_isPaused;
    }
    #endregion

    #region Ability
    private static bool _abilityDebounce = false;

    [Header("Abilities")]
    [SerializeField] private GameObject _arrowsAbility;
    [SerializeField] private SO_Ability _arrowsData;

    public static void ChangeAbilityDebounce() => _abilityDebounce = !_abilityDebounce;

    private void Arrows()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !_abilityDebounce && _timer.ElapsedTime - _timer.LT_AbilityArrowsUsed >= _timer.CD_Ability_Arrows
            && _settlementLogic.GoldCount >= _arrowsData.GoldCost)
        {
            ChangeAbilityDebounce();
            GameObject arrowsAbility = Instantiate(_arrowsAbility);
            AbilityLogic.CreateAbilityZone(arrowsAbility, _arrowsData, _settlementLogic, _timer);
        }
    }
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
            _UI.UI_UpdateTab(_settlementLogic, _farmerUnit, _warriorUnit, _builderUnit);
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
        _UI.UI_UpdateTab(_settlementLogic, _farmerUnit, _warriorUnit, _builderUnit);
    }
    #endregion

    #region Enemy
    [Header("Enemy")]
    [SerializeField] private GameObject _safeZone;
    [SerializeField] private GameObject _dangerZone;
    #endregion

    private void OnEnable()
    {
        _input.actions["ChangeStatement"].performed += _settlementLogic.ChangeStatement;
        _input.actions["SetPause"].performed += SetPause;
    }

    private void Awake()
    {
        StopAllCoroutines();

        EnemyLogic.EnemyDie += OnEnemyDie;

        _UI.Init_UI(_wheatLabel, _goldLabel, _farmerLabel, _warriorLabel, _builderLabel);

        _settlementLogic.InitUnit(_farmerUnit, _starterFarmers);
        _settlementLogic.InitUnit(_warriorUnit, _starterWarriors);
        _settlementLogic.InitUnit(_builderUnit, _starterBuilders);
    }

    private void OnEnemyDie(int gold, int score)
    {
        _settlementLogic.AddGold(gold);
        _score += score;
        _enemiesKilled += 1;
    }

    private void Start()
    {
        StartCoroutine(UI_Updater());
        StartCoroutine(_settlementLogic.UnitUpdater(_timer));
        StartCoroutine(_settlementLogic.WheatCollectUpdater(_timer));
        StartCoroutine(_settlementLogic.WheatEatUpdater(_timer));
    }

    private void Update()
    {
        if (_isPaused || _isDead) return;

        _timer.RaiseElapsedTime(Time.deltaTime);

        _enemyController.RaiseSpawnrate();

        _settlementLogic.MovementHandler(ref _score);

        if (Input.GetKeyDown(KeyCode.E) && CameraController.IsInteract)
            return;

        Arrows();
    }

    private void OnDisable()
    {
        _input.actions["ChangeStatement"].performed -= _settlementLogic.ChangeStatement;
        _input.actions["SetPause"].performed -= SetPause;
    }

    private void OnDestroy()
    {
        EnemyLogic.EnemyDie -= OnEnemyDie;
    }
}
