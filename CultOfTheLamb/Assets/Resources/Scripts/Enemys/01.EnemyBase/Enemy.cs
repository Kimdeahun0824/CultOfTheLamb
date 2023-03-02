using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;


namespace State
{
    public abstract class Enemy : MonoBehaviour, IObserver
    {
        [Header("EnemyOption")]
        public EnemyType enemyType;
        public StateMachine enemyStateMachine;
        public SkeletonAnimationHandler skeletonAnimationHandler;
        public GameObject attackCollider;

        [Space(5)]
        [Header("AStar")]
        public Vector3 currentWayPoint = default;
        public float distance = default;

        [Space(5)]
        [Header("EnemyStat")]
        public float maxHp;
        public float currentHp;
        public float defaultSpeed;
        public float currentSpeed;

        protected bool m_IsHit;
        public bool IsHit
        {
            get;
            private set;
        }
        protected bool m_IsDie;
        public bool IsDie
        {
            get;
            protected set;
        }

        public bool playerIsDieCheck = default;
        public Vector3 targetPos = default;

        protected void Start()
        {
            skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
            enemyStateMachine = new StateMachine();
            switch (enemyType)
            {
                case EnemyType.FORESTWORM:
                    var forestWorm = GetComponent<ForestWorm>();
                    enemyStateMachine.SetState(new ForestWormIntroState(forestWorm));
                    break;
                case EnemyType.SWORDMAN:
                case EnemyType.ARCHER:
                    enemyStateMachine.SetState(new Enemy_Spawn_State(this));
                    break;
            }

            currentHp = maxHp;
            currentSpeed = defaultSpeed;

            GameManager.Instance.PlayerRegisterObserver(this);
        }

        protected void Update()
        {
            enemyStateMachine.Update();
        }

        public virtual void TakeDamage(float damage)
        {
            if (currentHp <= 0) return;
            if (currentHp - damage <= 0)
            {
                currentHp = 0;
                if (enemyType == EnemyType.FORESTWORM)
                {
                    GetComponent<ForestWorm>().NotifyObservers();
                }
                Die();
            }
            else
            {
                switch (enemyType)
                {
                    case EnemyType.SWORDMAN:
                    case EnemyType.ARCHER:
                        enemyStateMachine.SetState(new EnemyHitState(this));
                        break;
                }
                currentHp -= damage;
                if (enemyType == EnemyType.FORESTWORM)
                {
                    GetComponent<ForestWorm>().NotifyObservers();
                }
            }
        }

        public virtual void SetState(StateBase state)
        {
            enemyStateMachine.SetState(state);
        }

        public virtual void Die()
        {
            switch (enemyType)
            {
                case EnemyType.FORESTWORM:
                    var forestWorm = GetComponent<ForestWorm>();
                    enemyStateMachine.SetState(new ForestWormDieState(forestWorm));
                    break;
                case EnemyType.SWORDMAN:
                case EnemyType.ARCHER:
                default:
                    StopAllCoroutines();
                    enemyStateMachine.SetState(new EnemyDieState(this));
                    break;
            }
        }

        public virtual void Destroy()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
        #region AStarAlgorithm
        Vector3[] path = default;
        Vector3[] previousPath = default;
        int targetIndex = default;
        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            //Debug.Log($"Instance ID : {this.GetInstanceID()}");
            if (pathSuccessful)
            {
                path = newPath;
                int index = 0;
                foreach (var n in path)
                {
                    index++;
                }
                StopCoroutine(FollowPath());
                StartCoroutine(FollowPath());
            }
        }
        IEnumerator FollowPath()
        {
            targetIndex = 0;
            if (path.Length <= 0) yield break;
            currentWayPoint = path[0];
            while (true)
            {
                if (transform.position == currentWayPoint)
                {
                    targetIndex++;
                    if (path.Length <= targetIndex)
                    {
                        yield break;
                    }
                    currentWayPoint = path[targetIndex];
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        #endregion


        #region ObserverPattern
        private Vector3 previousPos = default;
        public Vector3 PlayerPos
        {
            get
            {
                return previousPos;
            }
        }
        public void UpdateDate(GameObject data)
        {
            Vector3 currentPos = data.transform.position;
            if (previousPos != currentPos)
            {
                previousPos = currentPos;
                switch (enemyStateMachine.GetState())
                {
                    case EnemyDieState:
                        break;
                    default:
                        AStarManager.Instance.RequestPath(transform.position, currentPos, OnPathFound);
                        break;
                }
            }

            bool playerIsDie = data.GetComponent<Player>().IsDie;
            if (playerIsDieCheck != playerIsDie)
            {
                playerIsDieCheck = playerIsDie;
            }
        }
        #endregion


        #region SpineAnimation
        public void PlayAnimation(string aniName, int layerIndex, bool loop, float speed)
        {
            skeletonAnimationHandler.PlayAnimation(aniName, layerIndex, loop, speed);
        }
        public void AddPlayAnimation(string aniName, int layerIndex, bool loop, float speed, float delay)
        {
            skeletonAnimationHandler.AddPlayAnimation(aniName, layerIndex, loop, speed, delay);
        }

        public void SetFlip(bool flip)
        {
            skeletonAnimationHandler.SetFlip(flip);
        }

        public void SetFlip(float horizontal)
        {
            skeletonAnimationHandler.SetFlip(horizontal);
        }

        protected abstract void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e);
        protected abstract void HandleAnimationStateStartEvent(TrackEntry trackEntry);
        protected abstract void HandleAnimationStateEndEvent(TrackEntry trackEntry);
        protected abstract void HandleAnimationStateCompleteEvent(TrackEntry trackEntry);
        public void HandleAnimationStateEventAdd(Spine.AnimationState.TrackEntryEventDelegate func)
        {
            skeletonAnimationHandler.skeletonAnimation.AnimationState.Event += func;
        }
        public void HandleAnimationStateStartEventAdd(Spine.AnimationState.TrackEntryDelegate func)
        {
            skeletonAnimationHandler.skeletonAnimation.AnimationState.Start += func;
        }
        public void HandleAnimationStateEndEventAdd(Spine.AnimationState.TrackEntryDelegate func)
        {
            skeletonAnimationHandler.skeletonAnimation.AnimationState.End += func;
        }
        public void HandleAnimationStateCompleteEventAdd(Spine.AnimationState.TrackEntryDelegate func)
        {
            skeletonAnimationHandler.skeletonAnimation.AnimationState.Complete += func;
        }
        #endregion


        #region Collision
        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                float damage = other.GetComponentInParent<Player>().Damage;
                TakeDamage(damage);
            }
        }
        #endregion
    }
}