using UnityEngine;

[CreateAssetMenu(fileName = "SO_Structure", menuName = "Scriptable Objects/SO_Structure")]
public class SO_Structure : ScriptableObject
{
    [SerializeField] private int _structureHealth;
    [SerializeField] private int _structureDamage;

    public int StructureHealth => _structureHealth;
    public int StructureDamage => _structureDamage;
}
