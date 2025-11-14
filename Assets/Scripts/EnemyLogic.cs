using System;
using TMPro;
using UnityEngine;

public abstract class EnemyLogic : MonoBehaviour
{
    private int _enemyCurrentHealth;

    public static event Action<int, int> EnemyDie;

    public void Init(SO_Enemy EnemyData, TMP_Text HealthText)
    {
        _enemyCurrentHealth = EnemyData.EnemyHealth;
        ChangeHealthText(HealthText);
    }

    public bool IsAlive() => _enemyCurrentHealth > 0;

    public virtual void MoveEnemy(Transform Target, float EnemySpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, EnemySpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        _enemyCurrentHealth -= damage;
    }

    public virtual void EnemyDeath(SO_Enemy EnemyData)
    {
        EnemyDie?.Invoke(EnemyData.GoldReward, EnemyData.ScoreReward);
        Destroy(gameObject);
    }

    public void ChangeHealthText(TMP_Text HealthText) => HealthText.text = _enemyCurrentHealth.ToString();

    public virtual void LookAtCamera(Camera PlayerCamera, RectTransform HealthBox)
    {
        HealthBox.LookAt(PlayerCamera.transform.position);
    }

    public static Vector3 RandomEnemyPos(Transform Player, Transform SafeZone, Transform DangerZone)
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float randomDistance = UnityEngine.Random.Range(SafeZone.localScale.x, DangerZone.localScale.x);

        Vector3 spawnPosition = Player.transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * randomDistance;
        return spawnPosition;
    }
}
