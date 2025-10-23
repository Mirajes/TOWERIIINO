using UnityEngine;
using System.Collections.Generic;
public class SettlementLogic
{
    private int _allUnitCount = 0;
    private List<Unit> _settlements = new List<Unit>();

    private float _wheatCount = 0;
    private float _wheatMultiplierDefault = 1f;
    private float _wheatMultiplier = 1f;

    private float _goldValue = 0f;

    private int _unitInOrder = 0;

    public float WheatCount => _wheatCount;
    public int AllUnitsCount => _allUnitCount;

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
        Debug.Log(_wheatCount);
    }

    private void SubtractWheat(float WheatAmount)
    {
        _wheatCount -= WheatAmount * _wheatMultiplier;
    }


    public void EatWheat(bool isWalking)
    {
        if (isWalking)
        {
            _wheatMultiplier = 1.5f;
            float wheatAmount = _allUnitCount * _wheatMultiplier;
            SubtractWheat(wheatAmount);
        }
        else
        {
            _wheatMultiplier = _wheatMultiplierDefault;
            float wheatAmount = _allUnitCount * _wheatMultiplier;
            SubtractWheat(wheatAmount);
        }
    }


    #endregion

    #region Hiring
    public void HireUnit(SO_Unit so_unit)
    {
        if (_wheatCount >= so_unit.WheatPrice)
        {
            //if (_settlements.Contains(x => x.UnitType == so_unit))
        }
    }

    private void CountAllUnits()
    {

    }
    #endregion

    #region Builder

    #endregion
}

