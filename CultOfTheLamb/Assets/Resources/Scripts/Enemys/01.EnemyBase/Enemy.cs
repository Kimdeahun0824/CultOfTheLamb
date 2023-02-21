using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyState currentState = default;
    public EnemyType enemyType = default;

    #region Inspector
    [Header("Components")]
    public GameObject attackCollider = default;
    public GameObject player = default;
    public float speed = default;
    public float damage = default;
    public float maxHp = default;
    public float currentHp = default;
    public bool isDie = default;
    public bool isHit = default;
    public bool isAttack = default;
    #endregion

    Vector3[] path;
    int targetIndex;
    public Vector3 currentWayPoint = default;
    public Vector3 currentPlayerPos = default;

    public float attackDistance = default;
    public float distance = default;

    [Header("Spine")]
    public EnemyAnimationController enemyAnimationController = default;

    public void Start()
    {
        currentState = new EnemyPatrolState(this);
        attackCollider.SetActive(false);
        Debug.Log($"attackCollider Check : {attackCollider}");
        enemyAnimationController = GetComponent<EnemyAnimationController>();
    }
    public void Update()
    {
        Debug.Log($"currentState : {currentState}");
        distance = (player.transform.position - transform.position).magnitude;
        currentState.UpdateState();
        currentState.Action();
    }

    public virtual void Move()
    {

    }

    protected virtual void Attack()
    {

    }

    public virtual void Hit(float damage)
    {
        currentState.Hit();
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

    protected void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        currentWayPoint = path[0];
        while (true)
        {
            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (path.Length <= targetIndex)
                {
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }
            //Debug.Log("yield return null");
            //Move();
            yield return null;
        }
    }

    public void PathFindingPlayer()
    {
        AStarPathRequestManager.Instance.RequestPath(transform.position, player.transform.position, OnPathFound);
        currentPlayerPos = player.transform.position;
    }

    public void SetState(EnemyState state)
    {
        currentState = state;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Hit(other.GetComponent<Player>().Damage);
        }
    }


}