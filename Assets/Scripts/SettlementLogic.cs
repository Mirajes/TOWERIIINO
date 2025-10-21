using UnityEngine;
using System.Collections.Generic;
public class SettlementLogic
{
    private int _allUnitCount = 0;
    [SerializeField] private List<Unit> _settlements = new List<Unit>();

    private int _unitInOrder = 0;

    private float _wheatCount = 0;
    private float _wheatMultiplierDefault = 1f;
    private float _wheatMultiplier = 1f;

    public float WheatCount => _wheatCount;
    public int UnitsCount => _allUnitCount;

    private void StandingHandler()
    {


    }

    private void MovingHandler()
    {

    }

    private int CountFarmWheat()
    {
        int wheat = 0;

        foreach (Unit unit in _settlements)
        {
            wheat += unit.UnitCount * unit.UnitType.UnitWheatFarm;
        }

        return wheat;
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

    public void HereWeGo(bool isWalking)
    {
        if (isWalking)
            MovingHandler();
        else
            StandingHandler();
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

    public void CollectWheatRaw()
    {
        int wheatAmount = CountFarmWheat();
        AddWheat(wheatAmount);
    }

    
}

