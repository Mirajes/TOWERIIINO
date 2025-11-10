using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementLogic
{
    private float _wheatCount = 150;
    private float _wheatMultiplier = 1f;
    private int _goldCount = 0;

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



    #endregion

    #region Unit
    public IEnumerator UnitUpdater(Timer timer)
    {
        while (!GameManager.IsDead)
        {
            if (_orderList.Count > 0)
            {
                float cd = timer.CD_UnitHire / timer.CD_mult_UnitHire;

                yield return new WaitForSeconds(cd);
                SO_Unit so_unit = _orderList[0];
                _orderList.RemoveAt(0);
                CreateUnit(so_unit, 1);
            }

            yield return null; // fix
            //yield return new WaitUntil(GameManager.IsPaused == false);
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

    public IEnumerator WheatUpdater(Timer timer)
    {
        while (!GameManager.IsDead)
        {
            if (!_isWalking)
            {
                float cd = timer.CD_WheatCollect * timer.CD_mult_WheatCollect;
                yield return new WaitForSeconds(cd);
                
                if (_isWalking) yield return null;

                CollectWheat(_wheatMultiplier);
            }

            yield return null; // until
        }
    }

    public void CollectWheat(float wheatCollectMultiplier) => AddWheat(CountRawWheat(), wheatCollectMultiplier);
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
}
