using UnityEngine;

public abstract class EnemyLogic : MonoBehaviour
{
    protected int _enemyHealth;
    protected SO_Enemy _enemyData;

    public virtual void Init(SO_Enemy EnemyData)
    {
        _enemyData = EnemyData;
        _enemyHealth = _enemyData.EnemyHealth;
    }

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
        _enemyHealth -= damage;
    }

    public void Move(Transform destination, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
