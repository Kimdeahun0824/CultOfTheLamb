using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    private GameObject m_AttackCollider = default;
    public GameObject AttackColloder
    {
        get
        {
            return m_AttackCollider;
        }
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

    private int m_MaxHp = default;
    private int m_CurrentHp = default;
    public int CurrentHp
    {
        get
        {
            return m_CurrentHp;
        }
    }
    public float m_Default_Speed = 500f;
    private float m_Speed = default;
    public float Speed
    {
        get
        {
            return m_Speed;
        }
        set
        {
            m_Speed = value;
        }
    }

    private float m_Damage = default;
    public float Damage
    {
        get
        {
            return m_Damage;
        }
        private set
        {
            m_Damage = value;
        }
    }

    private float m_ActionDelay = default;

    private Rigidbody m_Rigidbody = default;

    private bool m_IsAttack;
    public bool IsAttack
    {
        get
        {
            return m_IsAttack;
        }
        set
        {
            m_IsAttack = value;
        }
    }

    private bool m_IsRolling;
    public bool IsRolling
    {
        get
        {
            return m_IsRolling;
        }
        set
        {
            m_IsRolling = value;
        }
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
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerState = new IdleState();
        m_AttackCollider = transform.GetChild(1).gameObject;
        m_AttackCollider.SetActive(false);
        m_Speed = 500.0f;
    }

    void Update()
    {
        Debug.Log($"State Pattern Debug : Current State : {m_PlayerState}");
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

    public void StateStartCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }
}
