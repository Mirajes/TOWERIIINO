using UnityEngine;

[CreateAssetMenu(fileName = "SO_Ability", menuName = "Scriptable Objects/SO_Ability")]
public class SO_Ability : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _goldCost;

    public string Name => _name;
    public int Damage => _damage;
    public float LifeTime => _lifeTime;
    public int GoldCost => _goldCost;
}
