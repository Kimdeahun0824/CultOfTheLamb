
using UnityEngine;
public class Enemy_SwordMan : Enemy
{
    private new void Start()
    {
        enemyType = EnemyType.SWORDMAN;
        player = GameObject.Find("Player");
        currentHp = maxHp;

        base.Start();
    }

    private new void Update()
    {
        base.Update();
        //transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);

    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
    }
}
