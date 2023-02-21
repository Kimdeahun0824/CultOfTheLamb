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
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
    }

    public class ForestWormHeadSmashState : State
    {
        private ForestWormBoss forestWormBoss;
        public override void OnEnter()
        {
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
        public override void OnEnter()
        {
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
                forestWormBoss.enemyStateMachine.SetState(new ForestWormBossDeadState(forestWormBoss));
            }
        }
    }

    public class ForestWormBossDeadState : State
    {
        private ForestWormBoss forestWormBoss;
        public ForestWormBossDeadState(ForestWormBoss forestWormBoss_)
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