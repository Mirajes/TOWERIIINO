using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Timer _timer = new Timer();
    private SettlementLife _settlementLife = new SettlementLife();

    [Header("UnitList")]
    #region Units
    [SerializeField] private SOUnit _farmer;
    [SerializeField] private SOUnit _warrior;
    #endregion

    #region StarterUnitsCount
    private int _starterFarmers = 15;
    private int _starterWarriors = 10;
    #endregion

    private int _wheatCount = 0;

    // LT - LastTime
    private float _LT_WheatCollect;
    private float _LT_WheatAte;

    private void Awake()
    {
        _LT_WheatCollect = _timer.TimeSurvived;
        _LT_WheatAte = _timer.TimeSurvived;
    }

    private void Start()
    {
        // todo
        _settlementLife.Initialize(_farmer, _starterFarmers);
        _settlementLife.Initialize(_warrior, _starterWarriors);
    }

    private void FixedUpdate()
    {
        _timer.RaiseSurvivedTimer();
        TimerAction();
        _settlementLife.Update();
    }

    private void TimerAction()
    {
        if (_timer.TimeSurvived - _LT_WheatCollect >= _timer.WheatCollectCD)
        {
            _wheatCount += _settlementLife.Settlements.Find(x => x.UnitType == _farmer).UnitCount * 1;
            _LT_WheatCollect = _timer.TimeSurvived;
            Debug.Log($"pshenitca = {_wheatCount}");
        }

        if (_timer.TimeSurvived - _LT_WheatAte >= _timer.WheatEatCD)
        {

            _LT_WheatAte = _timer.TimeSurvived;
        }
        // todo
    }

    private void isDead()
    {
        if (_settlementLife.AllUnitCount <= 0)
        {
            Time.timeScale = 0;
            //EditorApplication.Exit(0);
        }
    }


}
