using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;

namespace State
{
    public class tempEnemy : MonoBehaviour
    {
        public tempEnemyType enemyType;
        public tempEnemyStateMachine enemyStateMachine;
        public SkeletonAnimationHandler skeletonAnimationHandler;

        [Space(5)]
        [Header("EnemyStat")]
        public float maxHp;
        public float currentHp;
        public float damage;

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

        protected virtual void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
        {

        }

        protected virtual void HandleAnimationStateStartEvent(TrackEntry trackEntry)
        {

        }

        protected virtual void HandleAnimationStateEndEvent(TrackEntry trackEntry)
        {

        }

        protected virtual void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
        {

        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                float damage = other.GetComponentInParent<Player>().Damage;
                TakeDamage(damage);
            }
            Debug.Log($"OnTriggerEnter : {other}");
        }
    }
}