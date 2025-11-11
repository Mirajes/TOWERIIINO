using UnityEngine;

public abstract class StructureLogic : MonoBehaviour
{
    private int _structureHealth;

    public void Init(SO_Structure StructureData)
    {
        _structureHealth = StructureData.StructureHealth;
    }

    public virtual void Attack(SO_Structure StructureData)
    {
        
    }

    public bool IsAlive() => _structureHealth > 0;
}
