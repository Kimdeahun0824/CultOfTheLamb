using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
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
    Enemy enemy;
    EnemyState previousPlayerState;
    EnemyDirection previousDirection;
    public Spine.Animation nextAnimation;


}
