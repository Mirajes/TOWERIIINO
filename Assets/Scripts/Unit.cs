using UnityEngine;

public class Unit
{
    private SO_Unit _unitType;
    private int _count = 0;

    public SO_Unit UnitType => _unitType;
    public int UnitCount => _count;

    public Unit(SO_Unit unitType, int count)
    {
        _unitType = unitType;
        _count = count;
    }

    public void AddUnit(int addCount)
    {
        _count += addCount;
    }

    public void RemoveUnit(int removeCount)
    {
        _count += removeCount;
    }
}
