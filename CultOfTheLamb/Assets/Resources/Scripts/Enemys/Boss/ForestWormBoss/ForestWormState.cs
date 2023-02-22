namespace State
{
    using UnityEngine;
    using System.Collections;

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
            forestWormBoss.skeletonAnimationHandler.AddPlayAnimation("Idle", 0, true, 1f, 0f);
            if (forestWormBoss.IsMoveIn)
            {
                forestWormBoss.StartCoroutine(randomStateSelect());
            }
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


        IEnumerator randomStateSelect()
        {
            yield return new WaitForSeconds(1f);
            int randomNum = Random.Range(0, 3);
            // Move State Debug
            randomNum = 0;
            switch (randomNum)
            {
                case 0:
                    forestWormBoss.SetState(new ForestWormMoveState(forestWormBoss));
                    break;
                case 1:
                    forestWormBoss.SetState(new ForestWormHeadSmashState(forestWormBoss));
                    break;
                case 2:
                    forestWormBoss.SetState(new ForestWormTrunkStrikeState(forestWormBoss));
                    break;
                default:
                    break;

            }
        }
    }

    public class ForestWormMoveState : State
    {
        private ForestWormBoss forestWormBoss;
        public ForestWormMoveState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }
        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Move_Out", 0, false, 1f);
            Vector3 currentPos = forestWormBoss.transform.position;
            float randomX = Random.Range(-10, 10);
            float randomZ = Random.Range(-10, 10);
            forestWormBoss.randomPos = new Vector3(currentPos.x + randomX, currentPos.y, currentPos.z + randomZ);

            Debug.Log($"{this} state Enter");
        }

        public override void OnExit()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Move_In", 0, false, 1f);
            forestWormBoss.IsCreateSpike = false;
            forestWormBoss.StopCoroutine(moveSpikeCreate());
            Debug.Log($"{this} state Exit");
        }

        /// <summary>
        /// MoveState UpdateState 움직임을 구현 할 예정
        /// </summary>
        public override void UpdateState()
        {
            if (forestWormBoss.IsMoveOut)
            {
                if (!forestWormBoss.IsCreateSpike)
                {
                    forestWormBoss.StartCoroutine(moveSpikeCreate());
                }
                forestWormBoss.transform.position = Vector3.MoveTowards(forestWormBoss.transform.position, forestWormBoss.randomPos, forestWormBoss.speed * Time.deltaTime);
                if (forestWormBoss.transform.position == forestWormBoss.randomPos)
                {
                    forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
                }
            }
            Debug.Log($"{this} state Update + currentPos : {forestWormBoss.transform.position} / RandomPos : {forestWormBoss.randomPos}");
        }

        IEnumerator moveSpikeCreate()
        {
            forestWormBoss.IsCreateSpike = true;
            while (forestWormBoss.IsCreateSpike)
            {
                yield return new WaitForSeconds(0.1f);
                forestWormBoss.MoveSpikeCreate();
            }
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