using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Entities.AI.StateMachine.States
{
    public class ChaseState : State
    {
        public CombatState combatState;
        public override State Tick(EntityAnimatorHandler animatorHandler, EntityStats stats, EntityAI entityAI)
        {
            #region Chase the target
            if (entityAI.inAction)
            {
                animatorHandler.animator.SetFloat("z", 0, 0.1f, Time.deltaTime);
                animatorHandler.animator.SetFloat("x", 0, 0.1f, Time.deltaTime);
                return this;

            }
            Vector3 targetDirection = entityAI.currentTarget.transform.position - entityAI.transform.position;
            float viewAngle = Vector3.Angle(targetDirection, entityAI.transform.forward);
            float targetDistance = Vector3.Distance(entityAI.currentTarget.transform.position, entityAI.transform.position);

            if (targetDistance > stats.attackRange.GetValue())
            {
                if (targetDistance > entityAI.combatRadius && targetDistance < entityAI.chaseRadius)
                {
                    animatorHandler.animator.SetFloat("z", 2, 1, Time.deltaTime);
                }
                else if (targetDistance < entityAI.combatRadius)
                {
                    animatorHandler.animator.SetFloat("z", 1, 1, Time.deltaTime);
                }
            }
            else if (targetDistance <= stats.attackRange.GetValue())
            {
                animatorHandler.animator.SetFloat("z", 0, 0.1f, Time.deltaTime);
            }
            HandleRotation(entityAI,animatorHandler);

            entityAI.agent.transform.localPosition = Vector3.zero;
            entityAI.agent.transform.localRotation = Quaternion.identity;
            #endregion

            #region Handle switch state
            if (targetDistance <= stats.attackRange.GetValue())
                return combatState;
            else
                return this;
            #endregion
        }


        private void HandleRotation(EntityAI entityAI,EntityAnimatorHandler animatorHandler)
        {
            if (entityAI.inAction)
            {
                Vector3 direction = entityAI.currentTarget.transform.position - entityAI.transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                entityAI.transform.rotation = Quaternion.Slerp(entityAI.transform.rotation, targetRotation, entityAI.rotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 targetVelocity = animatorHandler.entityRigidBody.velocity;

                entityAI.agent.enabled = true;
                entityAI.agent.SetDestination(entityAI.currentTarget.transform.position);
                animatorHandler.entityRigidBody.velocity = targetVelocity;
                entityAI.transform.rotation = Quaternion.Slerp(entityAI.transform.rotation, entityAI.agent.transform.rotation, entityAI.rotationSpeed / Time.deltaTime);
            }

            entityAI.agent.transform.localPosition = Vector3.zero;
            entityAI.agent.transform.localRotation = Quaternion.identity;
        }
    }
}
