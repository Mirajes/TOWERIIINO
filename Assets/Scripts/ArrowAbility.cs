using UnityEngine;

public class ArrowAbility : AbilityLogic
{
    [SerializeField] private SO_Ability _abilityData;

    [SerializeField] private Transform _hitboxPos;
    [SerializeField] private Collider _hitboxCol;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyLogic>().TakeDamage(_abilityData.Damage);
        }
    }

    private void Update()
    {
        if (_active) return;

        _hitboxPos.transform.position = CameraController.GetWorldMousePos();
        AbilityOnRelease();
    }
}
