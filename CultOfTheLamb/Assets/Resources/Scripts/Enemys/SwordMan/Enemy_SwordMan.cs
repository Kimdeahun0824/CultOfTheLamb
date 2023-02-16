using UnityEngine;
public class Enemy_SwordMan : Enemy
{
    public GameObject attackCollider = default;
    private void Start()
    {
        player = GameObject.Find("Player");
        enemyState = new EnemyDieState();
        currentHp = maxHp;
    }

    private void Update()
    {

    }
    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void Move()
    {
        base.Move();
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
    }
}
