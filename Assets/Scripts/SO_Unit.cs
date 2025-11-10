using UnityEngine;

[CreateAssetMenu(fileName = "SO_Unit", menuName = "Scriptable Objects/SO_Unit")]
public class SO_Unit : ScriptableObject
{
    [SerializeField] private string _unitName;
    [SerializeField] private string _unitDescription;

    [SerializeField] private int _unitDamage;

    [SerializeField] private int _unitWheatPrice;
    [SerializeField] private int _unitWheatFarm;
    [SerializeField] private int _unitWheatConsumption; // potreblenie
    
    public string UnitName => _unitName;
    public string UnitDescription => _unitDescription;
    public int UnitDamage => _unitDamage;
    public int WheatPrice => _unitWheatPrice;
    public int UnitWheatFarm => _unitWheatFarm;
    public int UnitWheatConsumption => _unitWheatConsumption;
}