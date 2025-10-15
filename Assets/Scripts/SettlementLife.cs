using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettlementLife
{
    [SerializeField] private List<Unit> _settlements = new List<Unit>();
    private int _allUnitCount = 0;


    [SerializeField] private List<SOUnit> _unitOrder = new List<SOUnit>();
    private float _timer = 0f;
    private float _hireTimePerUnit = 3f;

    public List<Unit> Settlements => _settlements;
    public int AllUnitCount => _allUnitCount;

    public void Initialize(SOUnit unitType, int count)
    {
        if (_settlements.FindAll(x => x.UnitType == unitType).Count < 1)
        {
            _settlements.Add(new Unit(unitType, count));
            AddUnitToAll(unitType);
        }


    }

    public void HireUnit(SOUnit unit)
    {
        //if (_settlements.Contains(unit))
        _unitOrder.Add(unit);
    }

    public void Update()
    {
        if (_unitOrder.Count <= 0) return;

        _timer += Time.deltaTime;

        if (_timer >= _hireTimePerUnit)
        {
            if (_settlements.FindAll(x => x.UnitType == _unitOrder[0]).Count < 0)
                Initialize(_unitOrder[0], 0);

            _settlements.Find(x => x.UnitType == _unitOrder[0]).AddUnit(1);
            AddUnitToAll(_unitOrder[0]);
            _unitOrder.RemoveAt(0);
            _timer = 0f;
        }
    }

    private void AddUnitToAll(SOUnit unitType)
    {
        int allUnits = 0;
        foreach (var item in _settlements)
            allUnits += _settlements.Find(x => x.UnitType == unitType).UnitCount;
        _allUnitCount = allUnits;
    }

    public void SettlementLive(bool isWalking)
    {
        if (isWalking)
        {
            SettlementWalking();
        }
        else
        {
            SettlementStaying();
        }
    }

    private void SettlementStaying()
    {

    }

    private void SettlementWalking()
    {

    }
}
