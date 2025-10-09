using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SettlementLife
{
    [SerializeField] private List<SOUnit> _settlements = new List<SOUnit>();

    [SerializeField] private List<SOUnit> _unitOrder = new List<SOUnit>();
    private float _timer = 0f;
    private float _hireTimePerUnit = 3f;


    private bool _isWalking = false;

    public void Unitialize(SOUnit unitType, int count)
    {
        Unit test = new(unitType, count);
    }

    public void HireUnit(SOUnit unit)
    {
        if (_settlements.Contains(unit))
        _unitOrder.Add(unit);
    }

    public void Update()
    {
        if (_unitOrder.Count <= 0) return;

        _timer += Time.deltaTime;

        if (_timer >= _hireTimePerUnit)
        {
            _settlements.Add(_unitOrder[0]);
            _unitOrder.RemoveAt(0);
            _timer = 0f;
        }
    }

    public void SettlementLive()
    {
        if (_isWalking)
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

    public void ChangeSettlementState() => _isWalking = !_isWalking;
}
