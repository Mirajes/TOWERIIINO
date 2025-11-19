using System;
using System.Collections;
using System.Collections.Generic;
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

    public static Action<EnemyLogic> UnitHit;

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
    public void InitUnit(SO_Unit so_unit, int count)
    {
        CreateUnit(so_unit, count);
    }

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
        if (IsEnoughForHire(so_unit, _wheatCount, _goldCount))
        {
            RemoveWheat(so_unit.WheatPrice, 1f);
            RemoveGold(so_unit.GoldPrice);
            AddUnitToOrder(so_unit);
        }
    }

    public void KillUnit(SO_Unit so_unit, int count)
    {
        if (so_unit == null) return;
        _settlementList.Find(x => x.UnitType == so_unit).RemoveUnit(count);

        CheckForEndGame();
    }

    private bool IsEnoughForHire(SO_Unit so_unit, float wheatCount, int goldCount) => wheatCount >= so_unit.UnitWheatFarm
        && goldCount >= so_unit.GoldPrice;
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

    // :)
    private SO_Unit FindAliveUnit()
    {
        SO_Unit aliveUnit = null;

        foreach (var item in _settlementList)
        {
            if (item.UnitType.UnitName == "Warrior" && FindUnitCount(item.UnitType) > 0)
            {
                aliveUnit = item.UnitType;
                break;
            }
            else if (item.UnitType.UnitName == "Farmer" && FindUnitCount(item.UnitType) > 0)
            {
                aliveUnit = item.UnitType;
                break;
            }
            else if (item.UnitType.UnitName == "Builder" && FindUnitCount(item.UnitType) > 0)
            {
                aliveUnit = item.UnitType;
                break;
            }
            else
                aliveUnit = null;
        }

        return aliveUnit;
    }

    public void ExchangeUnitEnemy(EnemyLogic enemy)
    {
        SO_Unit aliveUnit = FindAliveUnit();
        if (aliveUnit != null)
        {
            enemy.TakeDamage(aliveUnit.UnitDamage);
            KillUnit(aliveUnit, 1);
        }
    }

    private void CheckForEndGame()
    {
        int unitCount = 0;
        foreach (var item in _settlementList)
            unitCount += item.Count;

        if (unitCount <= 0)
        {
            GameManager.GameEND?.Invoke();
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

    public IEnumerator GoldenRushUpdater(Timer timer, SO_Unit builder)
    {
        while (!GameManager.IsDead)
        {
            float cd = timer.CD_GoldFarm * timer.CD_mult_GoldFarm;
            if (FindUnitCount(builder) > 0)
            {
                #region PauseChecker
                float time = 0f;
                while (time < cd)
                {
                    yield return new WaitUntil(() => !GameManager.IsPaused);
                    time += Time.deltaTime;
                }
                #endregion

                AddGold(FindUnitCount(builder) * 1); // 1 -- gold farm
            }

            yield return null;
        }
    }
    #endregion
}
