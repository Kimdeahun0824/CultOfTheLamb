using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IEnemyState m_EnemyState = default;
    public float speed;
    public float damage;
    public float maxHp;
    public float currentHp;
    public bool isDie;
    protected virtual void Move()
    {

    }

    protected virtual void Attack()
    {

    }

    public virtual void Hit(float damage)
    {
        if (currentHp - damage < 0)
        {
            currentHp = 0;
            Die();
        }
        else
        {
            currentHp -= damage;
        }
    }

    protected virtual void Die()
    {

    }

}