using UnityEngine;

public class Timer
{
    // Game
    private float _elapsedTime = 0f;
    private float _gameScale = 1f;

    public float ElapsedTime => _elapsedTime;
    public float GameScale => _gameScale;

    // Wheat
    private float _CD_wheatCollect = 3f;
    private float _CD_mult_wheatCollect = 1f;

    private float _CD_wheatEat = 5f;
    private float _CD_mult_wheatEat = 1f;

    public float CD_WheatCollect => _CD_wheatCollect;
    public float CD_mult_WheatCollect => _CD_mult_wheatCollect;

    public float CD_WheatEat => _CD_wheatEat;
    public float CD_mult_WheatEat => _CD_mult_wheatEat;


    // Unit
    private float _CD_unitHire = 3f;
    private float _CD_mult_unitHire = 1f;

    public float CD_UnitHire => _CD_unitHire;
    public float CD_mult_UnitHire => _CD_mult_unitHire;

    // Enemy
    private float _CD_enemyRespawn = 5f;
    private float _enemy_changeSpawnrateTime = 10f;

    public float Enemy_ChangeSpawnrateTime => _enemy_changeSpawnrateTime;
    public float CD_EnemyRepsawn => _CD_enemyRespawn;

    // UI
    private float _CD_UI_update = 0.2f;

    public float CD_UI_Update => _CD_UI_update;

    // Ability
    private float _LT_abilityArrowsUsed = 0;
    private float _CD_ability_Arrows = 5f;

    public float LT_AbilityArrowsUsed => _LT_abilityArrowsUsed;
    public float CD_Ability_Arrows => _CD_ability_Arrows;

    // Func
    public void RaiseElapsedTime(float deltaTime) => _elapsedTime += deltaTime;
    public void ChangeGameScale(float NewScale)
    {
        _gameScale = NewScale;
        // action throw
    }

    public void ChangeMultUnitHire(float NewMultUnitHire) => _CD_mult_unitHire = NewMultUnitHire;
    public void ChangeMultWheatCollect(float NewMultWheatCollect) => _CD_mult_wheatCollect = NewMultWheatCollect;
    public void ChangeMultWheatEat(float NewMultWheatEat) => _CD_mult_wheatEat = NewMultWheatEat;

    public void SetAbility_LT_Arrows(float elapsedTime) => _LT_abilityArrowsUsed = elapsedTime;
}
