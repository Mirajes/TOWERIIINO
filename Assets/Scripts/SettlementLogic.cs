using Unity.Mathematics;
using UnityEngine;

public class SettlementLogic
{
    private Timer _timer = new();

    private int _allUnitCount = 0;

    private int _wheatCount = 0;
    private float _wheatMultiplier = 1f;

    //private bool _isWalking = false;

    public int WheatCount => _wheatCount;
    public int UnitsCount => _allUnitCount;

    // LT - LastTime
    private float _LT_CollectedWheat = 0f;
    private float _LT_AteWheat = 0f;

    private void StandingHandler()
    {
        if (_timer.SurvivedTime - _LT_CollectedWheat >= _timer.WheatCollectCD)
        {
            int wheatAmount = CollectFarmWheat();
            AddWheat(wheatAmount);
            _LT_CollectedWheat = _timer.SurvivedTime;
        }


    }

    private void MovingHandler()
    {

    }

    private void EatWheat(bool isWalking)
    {
        if (isWalking)
        {

        }
        else
        {

        }
    }
    private int CollectFarmWheat()
    {
        /* 
          
         ...
         
         */

        // цикл по всем юнитам
        return 10;
    }
    private void AddWheat(int WheatAmount)
    {
        _wheatCount += (int) math.round(_wheatCount * _wheatMultiplier);
    }

    private void SubtractWheat(int WheatAmount)
    {
        _wheatCount -= (int)math.round(_wheatCount * _wheatMultiplier);
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
