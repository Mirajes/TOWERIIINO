using UnityEngine;

[CreateAssetMenu(fileName = "SOUnit", menuName = "Scriptable Objects/SOUnit")]
public class SOUnit : ScriptableObject
{
    [SerializeField] private string _unitName;
    //[SerializeField] private GameObject _unitPrefab;
    [SerializeField] private int _unitWheatConsume;
    [SerializeField] private int _unitDamage;


    public string UnitName => _unitName;
    public int UnitWheatConsume => _unitWheatConsume;
    public int UnitDamage => _unitDamage;
}
