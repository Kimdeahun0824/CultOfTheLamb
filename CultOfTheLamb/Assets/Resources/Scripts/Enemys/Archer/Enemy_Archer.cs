using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;

public class Enemy_Archer : Enemy
{
    [Space(5)]
    [Header("Archer_Arrow")]
    public GameObject arrow;
    private new void Start()
    {
        enemyType = EnemyType.ARCHER;
        base.Start();
        EventAdd();
        EventFind();
    }

    public void ShootArrow()
    {
        //transform.rotation = new Quaternion()
        //Quaternion direction = Quaternion.LookRotation(transform.position, targetPos);
        //float rotationY = ConvenienceFunc.GetAngle(transform.position, player.transform.position);
        //direction = Quaternion.Euler(new Vector3(0, rotationY, 0));
        //direction = Quaternion.Euler(transform.position - player.transform.position);
        Vector3 position = new Vector3(transform.position.x, 1f, transform.position.z);
        Vector3 target = new Vector3(targetPos.x, 1f, targetPos.z);
        Instantiate(arrow, position, Quaternion.identity).transform.LookAt(target);
    }

    public void EventFind()
    {

    }
    public void EventAdd()
    {
        HandleAnimationStateEventAdd(HandleAnimationStateEvent);
        HandleAnimationStateStartEventAdd(HandleAnimationStateStartEvent);
        HandleAnimationStateEndEventAdd(HandleAnimationStateEndEvent);
        HandleAnimationStateCompleteEventAdd(HandleAnimationStateCompleteEvent);

    }
    protected override void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
    }

    protected override void HandleAnimationStateStartEvent(TrackEntry trackEntry)
    {
    }

    protected override void HandleAnimationStateEndEvent(TrackEntry trackEntry)
    {
    }

    protected override void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        enemyStateMachine.ChangeState();
        // if (trackEntry.ToString() == "grunt-attack-charge2"
        // || trackEntry.ToString() == "grunt-attack-impact2"
        // || trackEntry.ToString() == "archer-attack-charge"
        // || trackEntry.ToString() == "archer-attack-impact"
        // || trackEntry.ToString() == "hurt-front")
        // {
        //     enemyStateMachine.ChangeState();
        // }

    }
}
