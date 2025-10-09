using System;

[Serializable]
public class Unit
{
    private SOUnit _unitType;
    private int _count = 0;

    public SOUnit UnitType => _unitType;
    public int UnitCount => _count;

    public Unit(SOUnit unitType, int count)
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
        _count -= removeCount;
    }
}
