using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using State;

public class Player : MonoBehaviour
{
    //public SkeletonAnimationHandler skeletonAnimationHandler;
    private GameObject m_AttackCollider = default;
    public GameObject AttackColloder
    {
        get { return m_AttackCollider; }
        private set { m_AttackCollider = value; }
    }
    private IPlayerState m_PlayerState = default;
    public void SetState(IPlayerState state)
    {
        m_PlayerState = state;
    }

    public IPlayerState GetState()
    {
        return m_PlayerState;
    }

    private Vector3 m_Position = default;
    public void SetPosition(Vector3 pos)
    {
        m_Position = pos;
    }
    public Vector3 GetPosition()
    {
        return m_Position;
    }

    [Space(5)]
    [Header("PlayerStat")]

    public float MaxHp = default;
    // public float MaxHp
    // {
    //     get;
    //     private set;
    // }
    public float CurrentHp = default;
    // public float CurrentHp
    // {
    //     get;
    //     private set;
    // }
    public float Default_Speed = 500f;
    public float Speed = default;
    // public float Speed
    // {
    //     get;
    //     set;
    // }

    public float Damage = default;
    // public float Damage
    // {
    //     get;
    //     private set;
    // }

    private float m_ActionDelay = default;

    private Rigidbody m_Rigidbody = default;

    public bool IsAttack;
    // public bool IsAttack
    // {
    //     get;
    //     set;
    // }

    private bool m_IsRolling;
    public bool IsRolling
    {
        get;
        set;
    }

    private bool m_IsHit;
    public bool IsHit
    {
        get;
        set;
    }

    private bool m_IsDie;
    public bool IsDie
    {
        get;
        private set;
    }

    private Direction m_direction;
    public void SetDirection(Direction direction)
    {
        m_direction = direction;
    }
    public Direction GetDirection()
    {
        return m_direction;
    }


    void Start()
    {
        //skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerState = new IdleState();
        m_AttackCollider = transform.GetChild(1).gameObject;
        m_AttackCollider.SetActive(false);
        Speed = Default_Speed;
        CurrentHp = MaxHp;
        Damage = 1f;
    }

    void Update()
    {
        m_PlayerState.Action(this);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //m_Rigidbody.MovePosition(transform.localPosition + m_Position * Speed * Time.deltaTime);
        m_Rigidbody.velocity = m_Position * Speed * Time.deltaTime;
    }

    public void Hit()
    {
        m_PlayerState.Hit(this);
    }

    public void TakeDamage()
    {
        if (CurrentHp <= 0) return;
        if (CurrentHp - 1 <= 0)
        {
            IsDie = true;
            Speed = 0;
            SetState(new DieState());
        }
        else
        {
            Hit();
            CurrentHp -= 1;
        }
    }

    public void StateStartCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon" && !IsRolling && !IsHit && 0 < CurrentHp)
        {
            TakeDamage();
        }
    }
}
