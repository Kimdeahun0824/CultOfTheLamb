using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
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

    private int m_MaxHp = default;
    private int m_CurrentHp = default;
    public int CurrentHp
    {
        get
        {
            return m_CurrentHp;
        }
    }

    public float m_Speed = default;
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

    private float m_ActionDelay = default;

    private Rigidbody m_Rigidbody = default;
    public Rigidbody Rigidbody
    {
        get
        {
            return m_Rigidbody;
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

    public direction m_direction;



    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerState = new IdleState();
        m_Speed = 10.0f;
    }

    void Update()
    {

        Debug.Log($"State Pattern Debug : Current State : {m_PlayerState}");
        m_PlayerState.Action(this);
        // KeyInput();

    }

    private void CheckDirection()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //myPosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        // myPosition.x = Input.GetAxis("Horizontal");
        // myPosition.z = Input.GetAxis("Vertical");
        // myPosition.y = 0;
        m_Rigidbody.MovePosition(transform.position + m_Position * Speed * Time.deltaTime);

    }

    public void Hit()
    {
        m_PlayerState.Hit(this);
    }

    public void StateStartCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    public enum direction
    {
        NONE, LEFT_TOP, TOP, RIGHT_TOP, RIGHT, RIGHT_DOWN, DOWN, LEFT_DOWN, LEFT,
    }
}
