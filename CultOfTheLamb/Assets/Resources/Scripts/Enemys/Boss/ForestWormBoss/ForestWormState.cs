namespace State
{
    using UnityEngine;
    using System.Collections;

    public class ForestWormIntroState : StateBase
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
            //Debug.Log($"{this} state Update");
        }

        public override void ChangeState()
        {
            forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
        }

        public override void Action()
        {
        }
    }

    public class ForestWormIdleState : StateBase
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
        public override void ChangeState()
        {
            forestWormBoss.StartCoroutine(randomStateSelect());
        }
        public override void Action()
        {
        }
        IEnumerator randomStateSelect()
        {
            yield return new WaitForSeconds(1f);
            //ChangeState();
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
    public class ForestWormMoveState : StateBase
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
            float randomX = Random.Range(GameManager.Instance.minX + 5, GameManager.Instance.maxX - 5);
            float randomZ = Random.Range(GameManager.Instance.minY + 5, GameManager.Instance.maxY - 5);
            forestWormBoss.randomPos = new Vector3(randomX, currentPos.y, randomZ);

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
                forestWormBoss.transform.position = Vector3.MoveTowards(forestWormBoss.transform.position, forestWormBoss.randomPos, forestWormBoss.currentSpeed * Time.deltaTime);
                if (forestWormBoss.transform.position == forestWormBoss.randomPos)
                {
                    forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
                }
            }
            Debug.Log($"{this} state Update + currentPos : {forestWormBoss.transform.position} / RandomPos : {forestWormBoss.randomPos}");
        }
        public override void ChangeState()
        {
            //forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
        }
        public override void Action()
        {
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

    public class ForestWormBossMoveOutState : StateBase
    {
        ForestWormBoss forestWormBoss;
        public ForestWormBossMoveOutState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }
        public override void OnEnter()
        {
        }

        public override void UpdateState()
        {
        }
        public override void OnExit()
        {
        }

        public override void Action()
        {
        }


        public override void ChangeState()
        {
        }

    }

    public class ForestWormBossMoveInState : StateBase
    {
        ForestWormBoss forestWormBoss;
        public ForestWormBossMoveInState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }
        public override void OnEnter()
        {
        }

        public override void UpdateState()
        {
        }
        public override void OnExit()
        {
        }

        public override void Action()
        {
        }


        public override void ChangeState()
        {
        }
    }
    public class ForestWormHeadSmashState : StateBase
    {
        private ForestWormBoss forestWormBoss;
        int index = default;
        public ForestWormHeadSmashState(ForestWormBoss forestWormBoss_)
        {
            this.forestWormBoss = forestWormBoss_;
        }

        public override void OnEnter()
        {
            forestWormBoss.skeletonAnimationHandler.PlayAnimation("Head_Smash", 0, false, 1f);
            index = 0;
        }

        public override void OnExit()
        {
            foreach (var entry in forestWormBoss.attackColliders)
            {
                entry.SetActive(false);
            }
            index = 0;
        }

        public override void UpdateState()
        {
        }
        public override void ChangeState()
        {
            forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
        }
        public override void Action()
        {
            if (0 < index)
            {
                forestWormBoss.attackColliders[index - 1].SetActive(false);
            }
            forestWormBoss.attackColliders[index].SetActive(true);
            index++;
        }
    }

    public class ForestWormTrunkStrikeState : StateBase
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
        public override void ChangeState()
        {
            forestWormBoss.enemyStateMachine.SetState(new ForestWormIdleState(forestWormBoss));
        }
        public override void Action()
        {
            forestWormBoss.StartCoroutine(TrunkStrike());
            Debug.Log($"Action TrunkStrike");
        }

        IEnumerator TrunkStrike()
        {
            Vector3 targetPos_1 = new Vector3(forestWormBoss.transform.position.x - 10, 0f, forestWormBoss.transform.position.z + 10);
            Vector3 targetPos_2 = new Vector3(forestWormBoss.transform.position.x + 10, 0f, forestWormBoss.transform.position.z + 10);
            Vector3 targetPos_3 = new Vector3(forestWormBoss.transform.position.x - 10, 0f, forestWormBoss.transform.position.z - 10);
            Vector3 targetPos_4 = new Vector3(forestWormBoss.transform.position.x + 10, 0f, forestWormBoss.transform.position.z - 10);
            Vector3 currentPos_1 = forestWormBoss.transform.position;
            Vector3 currentPos_2 = forestWormBoss.transform.position;
            Vector3 currentPos_3 = forestWormBoss.transform.position;
            Vector3 currentPos_4 = forestWormBoss.transform.position;
            bool endTrunkStrike = false;
            while (!endTrunkStrike)
            {
                yield return new WaitForSeconds(0.1f);
                if (currentPos_1 != targetPos_1)
                {
                    currentPos_1 = Vector3.MoveTowards(currentPos_1, targetPos_1, 100 * Time.deltaTime);
                    forestWormBoss.TrunkStrikeSpikeCreate(currentPos_1);
                }
                if (currentPos_2 != targetPos_2)
                {
                    currentPos_2 = Vector3.MoveTowards(currentPos_2, targetPos_2, 100 * Time.deltaTime);
                    forestWormBoss.TrunkStrikeSpikeCreate(currentPos_2);
                }
                if (currentPos_3 != targetPos_3)
                {
                    currentPos_3 = Vector3.MoveTowards(currentPos_3, targetPos_3, 100 * Time.deltaTime);
                    forestWormBoss.TrunkStrikeSpikeCreate(currentPos_3);


                }
                if (currentPos_4 != targetPos_4)
                {
                    currentPos_4 = Vector3.MoveTowards(currentPos_4, targetPos_4, 100 * Time.deltaTime);
                    forestWormBoss.TrunkStrikeSpikeCreate(currentPos_4);

                }
                else
                {
                    endTrunkStrike = true;
                }
            }
        }
    }

    public class ForestWormDieState : StateBase
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
        public override void ChangeState()
        {

        }
        public override void Action()
        {
        }
    }

    public class ForestWormDeadState : StateBase
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
        public override void ChangeState()
        {
            throw new System.NotImplementedException();
        }
        public override void Action()
        {
        }
    }

}