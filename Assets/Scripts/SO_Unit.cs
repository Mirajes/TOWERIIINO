using UnityEngine;

[CreateAssetMenu(fileName = "SO_Unit", menuName = "Scriptable Objects/SO_Unit")]
public class SO_Unit : ScriptableObject
{
    [SerializeField] private string _unitName;
    //[SerializeField] private GameObject _unitPrefab;

    [SerializeField] private int _wheatPrice;
    [SerializeField] private int _wheatConsumption; // consumption - потребление
    [SerializeField] private int _wheatFarm;

    [SerializeField] private int _unitDamage;

    public string UnitName => _unitName;
    public int WheatPrice => _wheatPrice;
    public int UnitWheatConsumption => _wheatConsumption;
    public int UnitWheatFarm => _wheatFarm;
    public int UnitDamage => _unitDamage;
}
