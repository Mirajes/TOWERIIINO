using System;
using System.Collections;
using UnityEngine;

public abstract class AbilityLogic : MonoBehaviour
{
    private static GameObject _ability;
    private static SO_Ability _abilityData;

    private float _lifeTime;
    protected bool _active = false;

    private static SettlementLogic _settlement;
    private static Timer _timer;

    public static void CreateAbilityZone(GameObject abilityZone, SO_Ability abilityData, SettlementLogic settlement, Timer timer)
    {
        _ability = abilityZone;
        _abilityData = abilityData;
        _settlement = settlement;
        _timer = timer;
    }

    public void AbilityInit(Collider hitboxTrigger, float LifeTime)
    {
        hitboxTrigger.enabled = true;
        hitboxTrigger.isTrigger = true;
        _lifeTime = LifeTime;
    }

    public void AbilityOnRelease()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !_active)
        {
            _active = true;
            _settlement.RemoveGold(_abilityData.GoldCost);
            _timer.Set_LT_AbilityArrowsUsed(_timer.ElapsedTime);
            AbilityInit(_ability.GetComponent<Collider>(), _abilityData.LifeTime);
            StartCoroutine(AbilityIsWorking(_ability, _lifeTime));
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Destroy(gameObject);
            GameManager.ChangeAbilityDebounce();
        }


    }

    private IEnumerator AbilityIsWorking(GameObject ability, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        GameManager.ChangeAbilityDebounce();
        Destroy(ability);
    }

    private void Update()
    {
        AbilityOnRelease();
    }
}
