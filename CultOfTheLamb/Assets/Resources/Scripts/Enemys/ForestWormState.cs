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
            //forestWormBoss.skeletonAnimationHandler.PlayAnimationForState("Intro", 0, true, 1f);
            forestWormBoss.skeletonAnimationHandler.skeletonAnimation.state.SetAnimation(0, "intro2", false);
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
            forestWormBoss.skeletonAnimationHandler.PlayAnimationForState("Idle", 0, false, 1f);
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
        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ForestWormHeadSmashState : State
    {
        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ForestWormTrunkStrikeState : State
    {
        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }

}