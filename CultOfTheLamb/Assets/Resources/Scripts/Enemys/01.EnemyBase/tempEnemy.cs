using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;


namespace State
{
    public abstract class tempEnemy : MonoBehaviour
    {
        public tempEnemyType enemyType;
        public tempEnemyStateMachine enemyStateMachine;
        public SkeletonAnimationHandler skeletonAnimationHandler;

        [Space(5)]
        [Header("EnemyStat")]
        public float maxHp;
        public float currentHp;
        public float damage;
        public float speed;

        protected bool m_IsDie;
        public bool IsDie
        {
            get;
            protected set;
        }

        protected void Start()
        {
            skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
            enemyStateMachine = new tempEnemyStateMachine();

            switch (enemyType)
            {
                case tempEnemyType.FORESTWORM:
                    var forestWormBoss = GetComponent<ForestWormBoss>();
                    Debug.Log($"forestWormBoss : {forestWormBoss}");
                    enemyStateMachine.SetState(new ForestWormIntroState(forestWormBoss));
                    break;
            }
            Debug.Log($"currentState : {enemyStateMachine}");

            currentHp = maxHp;
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
                currentHp -= damage;
            }
        }

        public virtual void SetState(State state)
        {
            enemyStateMachine.SetState(state);
        }

        public virtual void Die()
        {
            switch (enemyType)
            {
                case tempEnemyType.FORESTWORM:
                    var forestWormBoss = GetComponent<ForestWormBoss>();
                    enemyStateMachine.SetState(new ForestWormDieState(forestWormBoss));
                    break;
                default:
                    enemyStateMachine.SetState(new DieState(this));
                    break;
            }

        }

        protected abstract void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e);

        protected abstract void HandleAnimationStateStartEvent(TrackEntry trackEntry);

        protected abstract void HandleAnimationStateEndEvent(TrackEntry trackEntry);


        protected abstract void HandleAnimationStateCompleteEvent(TrackEntry trackEntry);


        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                float damage = other.GetComponentInParent<Player>().Damage;
                TakeDamage(damage);
            }
            Debug.Log($"OnTriggerEnter : {other}");
        }

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
    }
}