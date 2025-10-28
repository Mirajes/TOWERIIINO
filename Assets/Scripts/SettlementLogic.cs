using UnityEngine;
using System.Collections.Generic;

public class SettlementLogic
{
    private int _allUnitCount = 0;
    private List<Unit> _settlements = new List<Unit>();

    private List<SO_Unit> _unitOrder = new List<SO_Unit>();

    private float _wheatCount = 150;
    private float _wheatMultiplierDefault = 1f;
    private float _wheatMultiplier = 1.2f;

    private float _goldValue = 0f;

    public List<Unit> SettlementsList => _settlements;
    public float WheatCount => _wheatCount;
    public int AllUnitsCount => _allUnitCount;
    public float GoldValue => _goldValue;

    #region Movement
    private void StandingHandler()
    {


    }

    private void MovingHandler()
    {

    }

    public void HereWeGo(bool isWalking)
    {
        if (isWalking)
            MovingHandler();
        else
            StandingHandler();
    }
    #endregion

    #region Wheat

    private int CountRawFarmWheat()
    {
        int wheat = 0;

        foreach (Unit unit in _settlements)
        {
            wheat += unit.UnitCount * unit.UnitType.UnitWheatFarm;
        }

        return wheat;
    }

    public void CollectWheatRaw()
    {
        int wheatAmount = CountRawFarmWheat();
        AddWheat(wheatAmount);
    }

    private void AddWheat(float WheatAmount)
    {
        _wheatCount += WheatAmount * _wheatMultiplier;
        Debug.Log($"пшеницы после фарма - {_wheatCount}");
    }

    private void SubtractWheat(float WheatAmount)
    {
        _wheatCount -= WheatAmount * _wheatMultiplier;
        Debug.Log($"пшеницы после съедания - {_wheatCount}");
    }


    public void EatWheat(bool isWalking)
    {
        float wheat = 0;

        foreach (Unit unit in _settlements)
        {
            wheat += unit.UnitCount * unit.UnitType.UnitWheatConsumption;
        }

        if (isWalking)
        {
            float wheatAmount = wheat * _wheatMultiplier;
            SubtractWheat(wheatAmount);
        }
        else
        {
            float wheatAmount = wheat * _wheatMultiplierDefault;
            SubtractWheat(wheatAmount);
        }
    }

    #endregion

    #region Units

    public void Init(SO_Unit so_unit, int count) => CreateUnit(so_unit, count);

    public void HireUnit(SO_Unit so_unit)
    {
        if (_wheatCount >= so_unit.WheatPrice)
        {
            //if (_settlements.Contains(x => x.UnitType == so_unit))
            _unitOrder.Add(so_unit);
        } 
        // else -- сказать юй "у вас недостаток" вы урод
    }
    public int FindUnitCount(SO_Unit so_unit)
    {
        return _settlements.Find(x => x.UnitType == so_unit).UnitCount;
    }

    public void UnitOrderUpdate()
    {
        if (_unitOrder.Count > 0)
        {
            CreateUnit(_unitOrder[0], 1);
            _unitOrder.RemoveAt(0);
        }
    }

    private void CreateUnit(SO_Unit so_unit, int count)
    {
        if (_settlements.FindAll(x => x.UnitType == so_unit).Count > 0)
        {
            _settlements.Find(x => x.UnitType == so_unit).AddUnit(count);
        }
        else
        {
            _settlements.Add(new Unit(so_unit, count));
        }
    }

    private int CountAllUnits()
    {
        int allCount = 0;

        foreach (var item in _settlements)
            allCount += item.UnitCount;

        return allCount;
    }
    #endregion

    #region Builder

    #endregion
}

