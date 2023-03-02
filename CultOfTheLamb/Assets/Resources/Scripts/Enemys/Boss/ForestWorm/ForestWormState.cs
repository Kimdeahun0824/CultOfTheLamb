namespace State
{
    using UnityEngine;
    using System.Collections;

    public class ForestWormIntroState : StateBase
    {
        private ForestWorm forestWorm;

        public ForestWormIntroState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }

        public override void OnEnter()
        {
            forestWorm.PlayAnimation("Intro", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }

        public override void ChangeState()
        {
            forestWorm.enemyStateMachine.SetState(new ForestWormIdleState(forestWorm));
        }

        public override void Action()
        {
        }
    }

    public class ForestWormIdleState : StateBase
    {
        private ForestWorm forestWorm;

        public ForestWormIdleState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }

        public override void OnEnter()
        {
            forestWorm.AddPlayAnimation("Idle", 0, true, 1f, 0f);
            if (forestWorm.IsMoveIn)
            {
                forestWorm.StartCoroutine(randomStateSelect());
            }
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
        public override void ChangeState()
        {
            forestWorm.StartCoroutine(randomStateSelect());
        }
        public override void Action()
        {
        }
        IEnumerator randomStateSelect()
        {
            yield return new WaitForSeconds(1f);
            int randomNum = Random.Range(0, 3);
            switch (randomNum)
            {
                case 0:
                    forestWorm.SetState(new ForestWormMoveState(forestWorm));
                    break;
                case 1:
                    forestWorm.SetState(new ForestWormHeadSmashState(forestWorm));
                    break;
                case 2:
                    forestWorm.SetState(new ForestWormTrunkStrikeState(forestWorm));
                    break;
                default:
                    break;
            }
        }
    }
    public class ForestWormMoveState : StateBase
    {
        private ForestWorm forestWorm;
        public ForestWormMoveState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }
        public override void OnEnter()
        {
            forestWorm.PlayAnimation("Move_Out", 0, false, 1f);
            Vector3 currentPos = forestWorm.transform.position;
            float randomX = Random.Range(GameManager.Instance.minX + 5, GameManager.Instance.maxX - 5);
            float randomZ = Random.Range(GameManager.Instance.minY + 5, GameManager.Instance.maxY - 5);
            forestWorm.randomPos = new Vector3(randomX, currentPos.y, randomZ);

        }

        public override void OnExit()
        {
            forestWorm.PlayAnimation("Move_In", 0, false, 1f);
            forestWorm.IsCreateSpike = false;
            forestWorm.StopCoroutine(moveSpikeCreate());
        }

        /// <summary>
        /// MoveState UpdateState 움직임을 구현 할 예정
        /// </summary>
        public override void UpdateState()
        {
            if (forestWorm.IsMoveOut)
            {
                if (!forestWorm.IsCreateSpike)
                {
                    forestWorm.StartCoroutine(moveSpikeCreate());
                }
                forestWorm.transform.position = Vector3.MoveTowards(forestWorm.transform.position, forestWorm.randomPos, forestWorm.currentSpeed * Time.deltaTime);
                if (forestWorm.transform.position == forestWorm.randomPos)
                {
                    forestWorm.enemyStateMachine.SetState(new ForestWormIdleState(forestWorm));
                }
            }
        }
        public override void ChangeState()
        {
        }
        public override void Action()
        {
        }
        IEnumerator moveSpikeCreate()
        {
            forestWorm.IsCreateSpike = true;
            while (forestWorm.IsCreateSpike)
            {
                yield return new WaitForSeconds(0.1f);
                forestWorm.MoveSpikeCreate();
            }
        }
    }

    public class ForestWormBossMoveOutState : StateBase
    {
        ForestWorm forestWorm;
        public ForestWormBossMoveOutState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
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
        ForestWorm forestWorm;
        public ForestWormBossMoveInState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
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
        private ForestWorm forestWorm;
        int index = default;
        public ForestWormHeadSmashState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }

        public override void OnEnter()
        {
            forestWorm.PlayAnimation("Head_Smash", 0, false, 1f);
            index = 0;
        }

        public override void OnExit()
        {
            foreach (var entry in forestWorm.attackColliders)
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
            forestWorm.enemyStateMachine.SetState(new ForestWormIdleState(forestWorm));
        }
        public override void Action()
        {
            if (0 < index)
            {
                forestWorm.attackColliders[index - 1].SetActive(false);
            }
            forestWorm.attackColliders[index].SetActive(true);
            index++;
        }
    }

    public class ForestWormTrunkStrikeState : StateBase
    {
        private ForestWorm forestWorm;

        public ForestWormTrunkStrikeState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }

        public override void OnEnter()
        {
            forestWorm.PlayAnimation("Trunk_Strike", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
        public override void ChangeState()
        {
            forestWorm.SetState(new ForestWormIdleState(forestWorm));
        }
        public override void Action()
        {
            forestWorm.StartCoroutine(TrunkStrike());
        }

        IEnumerator TrunkStrike()
        {
            Vector3 targetPos_1 = new Vector3(forestWorm.transform.position.x - 10, 0f, forestWorm.transform.position.z + 10);
            Vector3 targetPos_2 = new Vector3(forestWorm.transform.position.x + 10, 0f, forestWorm.transform.position.z + 10);
            Vector3 targetPos_3 = new Vector3(forestWorm.transform.position.x - 10, 0f, forestWorm.transform.position.z - 10);
            Vector3 targetPos_4 = new Vector3(forestWorm.transform.position.x + 10, 0f, forestWorm.transform.position.z - 10);
            Vector3 currentPos_1 = forestWorm.transform.position;
            Vector3 currentPos_2 = forestWorm.transform.position;
            Vector3 currentPos_3 = forestWorm.transform.position;
            Vector3 currentPos_4 = forestWorm.transform.position;
            bool endTrunkStrike = false;
            while (!endTrunkStrike)
            {
                yield return new WaitForSeconds(0.1f);
                if (currentPos_1 != targetPos_1)
                {
                    currentPos_1 = Vector3.MoveTowards(currentPos_1, targetPos_1, 100 * Time.deltaTime);
                    forestWorm.TrunkStrikeSpikeCreate(currentPos_1);
                }
                if (currentPos_2 != targetPos_2)
                {
                    currentPos_2 = Vector3.MoveTowards(currentPos_2, targetPos_2, 100 * Time.deltaTime);
                    forestWorm.TrunkStrikeSpikeCreate(currentPos_2);
                }
                if (currentPos_3 != targetPos_3)
                {
                    currentPos_3 = Vector3.MoveTowards(currentPos_3, targetPos_3, 100 * Time.deltaTime);
                    forestWorm.TrunkStrikeSpikeCreate(currentPos_3);


                }
                if (currentPos_4 != targetPos_4)
                {
                    currentPos_4 = Vector3.MoveTowards(currentPos_4, targetPos_4, 100 * Time.deltaTime);
                    forestWorm.TrunkStrikeSpikeCreate(currentPos_4);

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
        private ForestWorm forestWorm;
        public ForestWormDieState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }

        public override void OnEnter()
        {
            forestWorm.PlayAnimation("Die", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
            if (forestWorm.IsDie)
            {
                forestWorm.enemyStateMachine.SetState(new ForestWormDeadState(forestWorm));
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
        private ForestWorm forestWorm;
        public ForestWormDeadState(ForestWorm forestWorm_)
        {
            this.forestWorm = forestWorm_;
        }

        public override void OnEnter()
        {
            forestWorm.PlayAnimation("Dead", 0, true, 1f);
            forestWorm.StartCoroutine(GameClear());
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
        public override void ChangeState()
        {
        }
        public override void Action()
        {
        }

        IEnumerator GameClear()
        {
            yield return new WaitForSeconds(2f);
            GameManager.Instance.GameClear();
        }
    }

}