using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using State;

public class Player : MonoBehaviour, ISubject
{
    public SkeletonAnimationHandler skeletonAnimationHandler;
    private GameObject m_AttackCollider = default;
    public GameObject AttackColloder
    {
        get { return m_AttackCollider; }
        private set { m_AttackCollider = value; }
    }
    private StateMachine stateMachine = default;
    public void SetState(StateBase state)
    {
        stateMachine.SetState(state);
    }

    public StateMachine GetState()
    {
        return stateMachine;
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
    public float CurrentHp = default;
    public float Default_Speed = 500f;
    public float Speed = default;
    public float Damage = default;

    private float m_ActionDelay = default;

    private Rigidbody m_Rigidbody = default;

    public bool IsAttack;

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
        set;
    }

    private Vector3 previousPos = default;
    public bool IsEventComplete = false;

    public bool IsFlip = false;

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
        skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        SpineAnimationEventAdd();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_AttackCollider = transform.GetChild(1).gameObject;
        m_AttackCollider.SetActive(false);
        Speed = Default_Speed;
        CurrentHp = MaxHp;
        Damage = 1f;

        transform.position = GameManager.Instance.startPos;

        stateMachine = new StateMachine();
        SetState(new Player_Idle_State(this));
        previousPos = transform.position;
        NotifyObservers();
    }

    void Update()
    {
        stateMachine.Update();
        skeletonAnimationHandler.SetFlip(IsFlip);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_Rigidbody.velocity = m_Position * Speed * Time.deltaTime;
        if (previousPos != transform.position)
        {
            previousPos = transform.position;
            NotifyObservers();
        }
    }

    public void Hit()
    {
        stateMachine.SetState(new Player_Hit_State(this));
    }

    public void TakeDamage()
    {
        if (CurrentHp <= 0) return;
        if (CurrentHp - 1 <= 0)
        {
            IsDie = true;
            Speed = 0;
            CurrentHp = 0;
            SetState(new Player_Die_State(this));
        }
        else
        {
            Hit();
            CurrentHp -= 1;
        }
        NotifyObservers();
    }

    #region Spine Func
    public void SetAnimation(string aniName, int layerIndex, bool loop, float speed)
    {
        skeletonAnimationHandler.PlayAnimation(aniName, layerIndex, loop, speed);
    }

    public void SpineAnimationEventAdd()
    {
        skeletonAnimationHandler.skeletonAnimation.state.Event += HandleAnimationStateEvent;
        skeletonAnimationHandler.skeletonAnimation.state.Start += HandleAnimationStateStartEvent;
        skeletonAnimationHandler.skeletonAnimation.state.End += HandleAnimationStateEndEvent;
        skeletonAnimationHandler.skeletonAnimation.state.Complete += HandleAnimationStateCompleteEvent;
    }

    public void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {

    }
    public void HandleAnimationStateStartEvent(TrackEntry trackEntry)
    {

    }
    public void HandleAnimationStateEndEvent(TrackEntry trackEntry)
    {

    }
    public void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        if (trackEntry.ToString() == "idle"
        || trackEntry.ToString() == "idle-up"
        || trackEntry.ToString() == "run-up"
        || trackEntry.ToString() == "run-up-diagonal"
        || trackEntry.ToString() == "run-down"
        || trackEntry.ToString() == "run"
        || trackEntry.ToString() == "run-horizontal"
        ) return;
        if (!IsEventComplete)
        {
            stateMachine.ChangeState();
        }
    }
    #endregion

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon" && !IsRolling && !IsHit && 0 < CurrentHp)
        {
            TakeDamage();
        }
        else if (other.tag == "TriggerZone")
        {
            int x = other.GetComponentInParent<Room>().x;
            int y = other.GetComponentInParent<Room>().y;
            other.GetComponentInParent<Room>().RoomClear();
            RoomMove(other.name, x, y);
        }
    }

    public void RoomMove(string roomDirection, int x, int y)
    {
        switch (roomDirection)
        {
            case "TriggerZone_Left":
                transform.position = GameManager.Instance.RoomChangeLeft(x, y);
                SetDirection(Direction.LEFT);
                break;
            case "TriggerZone_Top":
                transform.position = GameManager.Instance.RoomChangeTop(x, y);
                SetDirection(Direction.UP);
                break;
            case "TriggerZone_Right":
                transform.position = GameManager.Instance.RoomChangeRight(x, y);
                SetDirection(Direction.RIGHT);
                break;
            case "TriggerZone_Bottom":
                transform.position = GameManager.Instance.RoomChangeBottom(x, y);
                SetDirection(Direction.DOWN);
                break;
        }
        SetState(new Player_DungeonMove_State(this));
    }


    #region ObserverPattern
    private List<IObserver> List_Observers = new List<IObserver>();
    public void RegisterObserver(IObserver observer)
    {
        List_Observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        List_Observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in List_Observers)
        {
            observer.UpdateDate(gameObject);
        }
    }
    #endregion
}
