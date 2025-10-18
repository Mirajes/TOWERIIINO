using Unity.Mathematics;
using UnityEngine;

public class SettlementLogic
{
    private Timer _timer = new();

    private int _allUnitCount = 0;

    private float _wheatCount = 0;
    private float _wheatMultiplierDefault = 1f;
    private float _wheatMultiplier = 1f;

    //private bool _isWalking = false;

    public float WheatCount => _wheatCount;
    public int UnitsCount => _allUnitCount;

    // LT - LastTime
    private float _LT_CollectedWheat = 0f;
    private float _LT_AteWheat = 0f;

    private void StandingHandler()
    {
        CollectWheat();


    }

    private void MovingHandler()
    {

    }

    private void EatWheat(bool isWalking)
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

    private void CollectWheat()
    {
        if (_timer.SurvivedTime - _LT_CollectedWheat >= _timer.WheatCollectCD)
        {
            int wheatAmount = CountFarmWheat();
            AddWheat(wheatAmount);
            _LT_CollectedWheat = _timer.SurvivedTime;
        }
    }
    private int CountFarmWheat()
    {
        /* 
          
         ...
         
         */

        // цикл по всем юнитам
        return 10;
    }
    private void AddWheat(float WheatAmount)
    {
        _wheatCount += WheatAmount * _wheatMultiplier;
    }

    private void SubtractWheat(float WheatAmount)
    {
        _wheatCount -= WheatAmount * _wheatMultiplier;
    }


    public void EatWheatHandler(bool isWalking)
    {
        if (_timer.SurvivedTime - _LT_AteWheat >= _timer.WheatEatCD)
            EatWheat(isWalking);
    }
    public void HereWeGo(bool isWalking)
    {
        if (isWalking)
            MovingHandler();
        else
            StandingHandler();
    }


    public void InitializeSettlement()
    {
        _LT_CollectedWheat = _timer.SurvivedTime;
        _LT_AteWheat = _timer.SurvivedTime;
    }
}
