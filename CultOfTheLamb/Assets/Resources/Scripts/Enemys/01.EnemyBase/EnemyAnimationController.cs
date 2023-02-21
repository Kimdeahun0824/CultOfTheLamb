using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class EnemyAnimationController : MonoBehaviour
{
    #region Inspector
    [Header("Components")]
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset run, run_up,
    attack_Charge, attack_impact,
    archer_Attack_Charge, archer_Attack_Impact,
    knockback, knockback_up;
    #endregion
    //Enemy enemy;
    EnemyState previousPlayerState;
    EnemyDirection previousDirection;
    public Spine.Animation nextAnimation;

    [SpineEvent]
    public string eventName;
    EventData eventData;


    private void Start()
    {
        //skeletonAnimation.
        skeletonAnimation.AnimationState.SetAnimation(0, run, true);
        eventData = skeletonAnimation.skeleton.Data.FindEvent(eventName);

        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
        skeletonAnimation.AnimationState.End += delegate (TrackEntry trackEntry)
        {
            GetComponent<Enemy>().attackCollider.SetActive(false);
        };

    }

    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        bool eventMatch = (eventData == e.Data);
        if (eventMatch)
        {
            GetComponent<Enemy>().attackCollider.SetActive(true);
            //enemy.attackCollider.SetActive(true);
            Debug.Log($"event 호출");
        }
    }


}
