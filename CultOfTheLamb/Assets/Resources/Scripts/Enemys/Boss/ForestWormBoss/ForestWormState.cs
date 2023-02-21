namespace State
{
    using UnityEngine;
    public class ForestWormIntroState : State
    {
        private ForestWormBoss forestWormBoss;

        public ForestWormIntroState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Intro", 0, false, 1f);
            Debug.Log($"{this} state Enter");
        }

        public override void OnExit()
        {
            Debug.Log($"{this} state Exit");
        }

        public override void UpdateState()
        {
            if (forestWormBoss.IsEndIntro)
            {
                forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
            }
            Debug.Log($"{this} state Update");

        }
    }

    public class ForestWormIdleState : State
    {
        private ForestWormBoss forestWormBoss;

        public ForestWormIdleState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Idle", 0, true, 1f);
            Debug.Log($"{this} state Enter");
        }

        public override void OnExit()
        {
            Debug.Log($"{this} state Exit");
        }

        public override void UpdateState()
        {

            Debug.Log($"{this} state Update");
        }

    }

    public class ForestWormMoveState : State
    {
        private ForestWormBoss forestWormBoss;
        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Move_Out", 0, false, 1f);
        }

        public override void OnExit()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Move_In", 0, false, 1f);
        }

        /// <summary>
        /// MoveState UpdateState 움직임을 구현 할 예정
        /// </summary>
        public override void UpdateState()
        {

        }
    }

    public class ForestWormHeadSmashState : State
    {
        private ForestWormBoss forestWormBoss;

        public ForestWormHeadSmashState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Head_Smash", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
    }

    public class ForestWormTrunkStrikeState : State
    {
        private ForestWormBoss forestWormBoss;

        public ForestWormTrunkStrikeState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Trunk_Strike", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
    }

    public class ForestWormDieState : State
    {
        private ForestWormBoss forestWormBoss;
        public ForestWormDieState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
            if (forestWormBoss.IsDie)
            {
                forestWormBoss.enemyStateMachine.SetState(new ForestWormDeadState(forestWormBoss));
            }
        }
    }

    public class ForestWormDeadState : State
    {
        private ForestWormBoss forestWormBoss;
        public ForestWormDeadState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Dead", 0, true, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
    }

}