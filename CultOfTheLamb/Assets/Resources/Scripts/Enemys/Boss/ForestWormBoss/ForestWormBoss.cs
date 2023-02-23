using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Collections;
using Spine;
using State;

public class ForestWormBoss : Enemy
{
    public List<GameObject> attackColliders = default;
    public GameObject attackSpike;
    public Vector3 randomPos = default;
    private bool m_IsEndIntro = default;
    public bool IsEndIntro
    {
        get;
        private set;
    }

    private bool m_IsMoveOut = default;
    public bool IsMoveOut
    {
        get;
        private set;
    }

    private bool m_IsMoveIn = default;
    public bool IsMoveIn
    {
        get;
        private set;
    }
    private bool m_IsCreateSpike = default;
    public bool IsCreateSpike
    {
        get;
        set;
    }

    private EventData spikeAttackEvent = default;
    private EventData pushThroughGround = default;


    protected new void Start()
    {
        base.Start();
        EventAdd();
        EventFind();
        Debug.Log($"headSmashEvent : {pushThroughGround}");
    }
    protected void EventFind()
    {
        spikeAttackEvent = skeletonAnimationHandler.skeletonAnimation.skeleton.Data.FindEvent("spikeAttack");
        pushThroughGround = skeletonAnimationHandler.skeletonAnimation.skeleton.Data.FindEvent("pushThroughGround");
    }
    protected void EventAdd()
    {
        HandleAnimationStateEventAdd(HandleAnimationStateEvent);
        HandleAnimationStateStartEventAdd(HandleAnimationStateStartEvent);
        HandleAnimationStateEndEventAdd(HandleAnimationStateEndEvent);
        HandleAnimationStateCompleteEventAdd(HandleAnimationStateCompleteEvent);
    }
    public void MoveSpikeCreate()
    {
        Vector3 spikePos = new Vector3(transform.position.x, 0f, transform.position.z);
        Instantiate(attackSpike, spikePos, Quaternion.identity);
    }

    public void TrunkStrikeSpikeCreate(Vector3 spikePos)
    {
        Instantiate(attackSpike, spikePos, Quaternion.identity);
    }

    protected override void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data == spikeAttackEvent)
        {
            enemyStateMachine.Action();
        }
        else if (e.Data == pushThroughGround)
        {
            enemyStateMachine.Action();
        }
        Debug.Log($"Event Trigger Test");
    }

    protected override void HandleAnimationStateStartEvent(TrackEntry trackEntry)
    {
        Debug.Log($"Event Start Test");
    }

    protected override void HandleAnimationStateEndEvent(TrackEntry trackEntry)
    {
        Debug.Log($"Event End Test");
    }
    protected override void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        if (trackEntry.ToString() == "intro2")
        {
            IsEndIntro = true;
            IsMoveIn = true;
        }
        if (trackEntry.ToString() == "die")
        {
            IsDie = true;
        }
        if (trackEntry.ToString() == "move-in")
        {
            IsMoveOut = false;
            IsMoveIn = true;
            enemyStateMachine.OnEnter();
        }
        if (trackEntry.ToString() == "move-out")
        {
            IsMoveOut = true;
            IsMoveIn = false;
        }
        // if (trackEntry.ToString() == "" || trackEntry.ToString() == "")
        // {

        // }
        enemyStateMachine.ChangeState();
        Debug.Log($"Event Complete Test : {trackEntry.ToString()}");
    }

}
