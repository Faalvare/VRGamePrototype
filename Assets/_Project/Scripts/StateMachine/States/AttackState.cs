using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Entities.Actions;
namespace Main.Entities.AI.StateMachine.States
{
    public class AttackState : State
    {
        public EntityAttackAction[] entityAttacks;
        public EntityAttackAction currentAttack;
        public CombatState combatState;
        public override State Tick(EntityAnimatorHandler animatorHandler, EntityStats stats, EntityAI entityAI)
        {
            Vector3 targetDirection = entityAI.currentTarget.transform.position - entityAI.transform.position;
            float viewAngle = Vector3.Angle(targetDirection, entityAI.transform.forward);
            float targetDistance = Vector3.Distance(entityAI.currentTarget.transform.position, entityAI.transform.position);
            if (entityAI.inAction)
            {
                return combatState;
            }

            if (currentAttack != null)
            {
                if (targetDistance < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                else if (targetDistance< currentAttack.maximumDistanceNeededToAttack)
                {
                    if (viewAngle <= currentAttack.maximumAttackAngle &&
                        viewAngle >= currentAttack.minimumAttackAngle)
                    {
                        if(entityAI.currentRecoveryTime<=0 && entityAI.inAction == false)
                        {
                            animatorHandler.animator.SetFloat("z", 0, 0.1f, Time.deltaTime);
                            animatorHandler.animator.SetFloat("x", 0, 0.1f, Time.deltaTime);
                            animatorHandler.PlayAnimation(currentAttack.actionAnimation,true);
                            entityAI.currentRecoveryTime = currentAttack.recoveryTime;
                            entityAI.inAction = true;
                            currentAttack = null;
                            return combatState;
                        }
                    }
                }
            }
            else
            {
                GetNewAttack(entityAI);
            }
            return combatState;
        }

        private void GetNewAttack(EntityAI entityAI)
        {
            float targetDistance = Vector3.Distance(entityAI.currentTarget.transform.position, entityAI.transform.position);
            Vector3 targetDirection = entityAI.currentTarget.transform.position - entityAI.transform.position;
            float viewAngle = Vector3.Angle(targetDirection, entityAI.transform.forward);
            int maxWeight = 0;
            foreach (EntityAttackAction action in entityAttacks)
            {
                //If attack is in range
                if (targetDistance <= action.maximumDistanceNeededToAttack && targetDistance >= action.minimumDistanceNeededToAttack)
                {
                    //If attack is in angle
                    if (viewAngle <= action.maximumAttackAngle && viewAngle >= action.minimumAttackAngle)
                    {
                        maxWeight += action.weight;
                    }
                }
            }

            int random = Random.Range(0, maxWeight);
            int tempWeight = 0;

            foreach (EntityAttackAction action in entityAttacks)
            {
                Debug.Log("Checking distance");
                //If attack is in range
                if (targetDistance <= action.maximumDistanceNeededToAttack && targetDistance >= action.minimumDistanceNeededToAttack)
                {
                    Debug.Log("attack in range");
                    //If attack is in angle
                    if (viewAngle <= action.maximumAttackAngle && viewAngle >= action.minimumAttackAngle)
                    {
                        Debug.Log("attack in angle");
                        if (currentAttack != null)
                            return;
                        tempWeight += action.weight;

                        if (tempWeight > random)
                        {
                            currentAttack = action;
                        }
                    }
                }
            }
        }

    }
}
