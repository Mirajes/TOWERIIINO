using System;

[Serializable]
public class Unit
{
    private SOUnit _unitType;
    private int _count = 0;

    public Unit(SOUnit unitType, int count)
    {
        _unitType = unitType;
        _count = count;
    }
}
