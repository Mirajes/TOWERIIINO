using UnityEngine;

[CreateAssetMenu(fileName = "Balance", menuName = "Scriptable Objects/Balance")]
public class Balance : ScriptableObject
{
    [SerializeField] private string name = "";
    [SerializeField] private int a = 0;
}
