using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettlementLogic
{
    private float _wheatCount = 150;
    private float _wheatMultiplier = 1f;
    private int _goldCount = 50;

    private float _tempScore = 0f;

    private bool _isWalking = false;

    private List<Unit> _settlementList = new List<Unit>();
    private List<SO_Unit> _orderList = new List<SO_Unit>();

    public float WheatCount => _wheatCount;
    public float WheatMultiplier => _wheatMultiplier;
    public int GoldCount => _goldCount;
    public bool IsWalking => _isWalking;

    public void InitUnit(SO_Unit so_unit, int count)
    {
        CreateUnit(so_unit, count);
    }

    #region Movement

    public void MovementHandler(ref int score)
    {
        if (_isWalking)
        {
            #region Score
            _tempScore += Time.deltaTime;
            if (Mathf.Round(_tempScore) >= 1)
            {
                score += 1;
                _tempScore -= 1;
            }
            #endregion


        }
        else
        {

        }
    }

    public void ChangeStatement(InputAction.CallbackContext context)
    {
        _isWalking = !_isWalking;
    }

    #endregion

    #region Unit
    public IEnumerator UnitUpdater(Timer timer)
    {
        while (!GameManager.IsDead)
        {
            float cd = timer.CD_UnitHire / timer.CD_mult_UnitHire;
            if (_orderList.Count > 0)
            {
                #region PauseChecker
                float time = 0f;
                while (time < cd)
                {
                    yield return new WaitUntil(() => !GameManager.IsPaused);
                    time += Time.deltaTime;
                }
                #endregion
                SO_Unit so_unit = _orderList[0];
                _orderList.RemoveAt(0);
                CreateUnit(so_unit, 1);
            }

            yield return null;
        }
    }

    public int FindUnitCount(SO_Unit so_unit) => _settlementList.Find(x => x.UnitType == so_unit).Count;

    public void HireUnit(SO_Unit so_unit)
    {
        if (IsEnoughForHire(so_unit, _wheatCount))
        {
            RemoveWheat(so_unit.WheatPrice, 1f);
            AddUnitToOrder(so_unit);
        }
    }

    public void KillUnit(SO_Unit so_unit, int count)
    {
        if (so_unit == null) return;
        _settlementList.Find(x => x.UnitType == so_unit).RemoveUnit(count);
    }

    private bool IsEnoughForHire(SO_Unit so_unit, float wheatCount) => wheatCount >= so_unit.UnitWheatFarm;
    private void AddUnitToOrder(SO_Unit so_unit) => _orderList.Add(so_unit);
    private void CreateUnit(SO_Unit so_unit, int count)
    {
        // что это
        if (_settlementList.FindAll(x => x.UnitType == so_unit).Count > 0)
        {
            _settlementList.Find(x => x.UnitType == so_unit).AddUnit(count);
        }
        else
        {
            _settlementList.Add(new Unit(so_unit, count));
        }
    }
    #endregion

    #region Wheat

    public IEnumerator WheatCollectUpdater(Timer timer)
    {
        bool debounce = false;

        while (!GameManager.IsDead)
        {
            if (!debounce && !_isWalking)
            {
                debounce = true;
                float cd = timer.CD_WheatCollect * timer.CD_mult_WheatCollect;
                #region PauseChecker
                float time = 0f;
                while (time < cd)
                {
                    yield return new WaitUntil(() => !GameManager.IsPaused);
                    if (_isWalking)
                    {
                        yield return null;
                        debounce = false;
                        time = 0f;
                    }

                    time += Time.deltaTime;
                }

                #endregion

                CollectWheat(_wheatMultiplier);
                debounce = false;
                yield return null;
            } else
            {
                yield return null;
            }

        }
    }

    public IEnumerator WheatEatUpdater(Timer timer)
    {
        bool debounce = false;

        while (!GameManager.IsDead)
        {
            if (!debounce)
            {
                debounce = true;

                float cd = timer.CD_WheatEat * timer.CD_mult_WheatEat;
                float time = 0f;

                while (time < cd)
                {
                    yield return new WaitUntil(() => !GameManager.IsPaused);
                    time += Time.deltaTime;
                }

                RemoveWheat(CountRawWheat(), 1f);

                debounce = false;
                yield return null;;
            }
            else
            {
                yield return null;
            }
        }
    }

    private void CollectWheat(float wheatCollectMultiplier) => AddWheat(CountRawWheat(), wheatCollectMultiplier);
    public void ChangeWheatMultiplier(float NewWheatMultiplier) => _wheatMultiplier = NewWheatMultiplier;
    public void EatWheat(float wheatEatMultiplier)
    {
        float wheatToEat = 0f;

        foreach (Unit unit in _settlementList)
            wheatToEat += unit.Count * unit.UnitType.UnitWheatConsumption;

        RemoveWheat(wheatToEat, wheatEatMultiplier);
    }

    private float CountRawWheat()
    {
        float wheat = 0;

        foreach (Unit unit in _settlementList)
            wheat += unit.Count * unit.UnitType.UnitWheatFarm;
        
        return wheat;
    }

    private void AddWheat(float wheatToAdd, float wheatAddMultiplier) => _wheatCount += wheatToAdd * wheatAddMultiplier;

    private void RemoveWheat(float wheatToEat, float wheatEatMultiplier) => _wheatCount -= wheatToEat * wheatEatMultiplier;



    #endregion

    #region Gold
    public void AddGold(int goldAmount) => _goldCount += goldAmount;
    
    public void RemoveGold(int goldAmount) => _goldCount -= goldAmount;
    #endregion
}
