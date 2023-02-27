using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;


namespace State
{
    public abstract class Enemy : MonoBehaviour, ISubject
    {
        [Header("EnemyOption")]
        public EnemyType enemyType;
        public EnemyStateMachine enemyStateMachine;
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

        [Space(5)]
        [Header("Target")]
        public GameObject player;
        public Vector3 targetPos = default;

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

        protected void Start()
        {
            skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
            enemyStateMachine = new EnemyStateMachine();
            player = GameObject.Find("Player");
            switch (enemyType)
            {
                case EnemyType.FORESTWORM:
                    var forestWormBoss = GetComponent<ForestWormBoss>();
                    Debug.Log($"forestWormBoss : {forestWormBoss}");
                    enemyStateMachine.SetState(new ForestWormIntroState(forestWormBoss));
                    break;
                case EnemyType.SWORDMAN:
                case EnemyType.ARCHER:
                    enemyStateMachine.SetState(new IdleState(this));
                    break;
            }
            Debug.Log($"currentState : {enemyStateMachine}");

            currentHp = maxHp;
            currentSpeed = defaultSpeed;
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
                Die();
            }
            else
            {
                switch (enemyType)
                {
                    case EnemyType.SWORDMAN:
                    // var swordMan = GetComponent<Enemy_SwordMan>();
                    // if (swordMan != null)
                    // {
                    //     enemyStateMachine.SetState(new HitState(swordMan));
                    // }
                    //break;
                    case EnemyType.ARCHER:
                        // var archer = GetComponent<Enemy_Archer>();
                        // if (archer != null)
                        // {
                        //     enemyStateMachine.SetState(new HitState(archer));
                        // }
                        enemyStateMachine.SetState(new HitState(this));
                        break;
                }
                currentHp -= damage;
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
                    var forestWormBoss = GetComponent<ForestWormBoss>();
                    enemyStateMachine.SetState(new ForestWormDieState(forestWormBoss));
                    break;
                case EnemyType.SWORDMAN:
                case EnemyType.ARCHER:
                default:
                    enemyStateMachine.SetState(new DieState(this));
                    break;
            }
        }

        #region AStarAlgorithm
        Vector3[] path = default;
        int targetIndex = default;
        Vector3 currentPlayerPos = default;
        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                path = newPath;
                StopCoroutine(FollowPath());
                StartCoroutine(FollowPath());
            }
        }
        IEnumerator FollowPath()
        {
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
        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                float damage = other.GetComponentInParent<Player>().Damage;
                Debug.Log($"damage : {damage}");
                TakeDamage(damage);
            }
            Debug.Log($"OnTriggerEnter : {other}");
        }


        #region ObserverPattern
        private List<IObserver> List_Observers = new List<IObserver>();
        public void RegisterObserver(IObserver observer)
        {
            List_Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            List_Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in List_Observers)
            {
                observer.UpdateDate(gameObject);
            }
        }
        #endregion
    }
}