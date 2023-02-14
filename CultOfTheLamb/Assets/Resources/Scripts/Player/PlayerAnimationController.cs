using UnityEngine;
using Spine.Unity;

public class PlayerAnimationController : MonoBehaviour
{
    #region Inspector
    [Header("Components")]
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, idle_up,
    run, run_down, run_horizontal, run_up, run_up_diagonal,
    roll, roll_up, roll_down,
    attack_combo_1, attack_combo_2, attack_combo_3,
    knockback, die;
    #endregion
    Player player;
    IPlayerState previousPlayerState;
    public Spine.Animation nextAnimation;

    void Start()
    {
        //skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        if (skeletonAnimation == null) return;
        player = GetComponent<Player>();
        if (player == null) return;
        previousPlayerState = player.GetState();
        nextAnimation = idle;
    }

    void Update()
    {
        IPlayerState currentPlayerState = player.GetState();
        if (previousPlayerState != currentPlayerState)
        {
            currentPlayerState.SetAnimation(this);
            PlayNewStableAnimation();
        }
        previousPlayerState = currentPlayerState;
    }
    void PlayNewStableAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
    }

    void Turn()
    {

    }
}
