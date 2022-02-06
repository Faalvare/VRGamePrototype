using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Main.Others;
using Main.Entities.Actions;
using Main.Entities.AI.StateMachine;
namespace Main.Entities.AI{
    [RequireComponent(typeof(EntityStats))]
    public class EntityAI : MonoBehaviour
    {
        [Header("State Machine")]
        public State currentState;
        public EntityStats currentTarget;
        public NavMeshAgent agent;
        public LookAt lookAt;
        public EntityAnimatorHandler animatorHandler;
        public EntityStats stats;
        public bool inAction;
        public float currentRecoveryTime = 0;

        [Header("Settings")]
        public float combatRadius = 3;
        public float chaseRadius = 8;
        public float rotationSpeed = 20;
        private void Awake()
        {
            if (!animatorHandler)
                animatorHandler = GetComponent<EntityAnimatorHandler>();
            stats = GetComponent<EntityStats>();
        }

        private void Start()
        {
            agent.enabled = false;
            animatorHandler.entityRigidBody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            if (currentTarget)
            {
                lookAt.lookAtTargetPosition = currentTarget.transform.position;
            }
        }

        private void FixedUpdate()
        {
            HandleStateMachine();
            animatorHandler.animator.SetFloat("MovementSpeed", stats.movementSpeed.GetValue());
            animatorHandler.animator.SetFloat("AttackSpeed", stats.attackSpeed.GetValue());
        }
        private void HandleStateMachine()
        {
            if(currentState != null)
            {
                State nextState = currentState.Tick(animatorHandler,stats,this);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (inAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    inAction = false;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, combatRadius);
        }
    }
}
