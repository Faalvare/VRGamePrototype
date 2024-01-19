using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Entities.AI.StateMachine;
using Main.Entities;
using Main.Entities.AI;
using Main.Entities.AI.StateMachine.States;

public class CombatState : State
{
    public ChaseState chaseState;
    public AttackState attackState;
    public override State Tick(EntityAnimatorHandler animatorHandler, EntityStats stats, EntityAI entityAI)
    {
        float targetDistance = Vector3.Distance(entityAI.currentTarget.transform.position, entityAI.transform.position);
        if (entityAI.inAction)
        {
            animatorHandler.animator.SetFloat("z", 0, 0.1f, Time.deltaTime);
            animatorHandler.animator.SetFloat("x", 0, 0.1f, Time.deltaTime);
        }
        if(entityAI.currentRecoveryTime <= 0 && targetDistance <= stats.attackRange.GetValue())
        {
            return attackState;
        }
        else if(targetDistance> stats.attackRange.GetValue())
        {
            return chaseState;
        }else
        {
            return this;
        }
    }
}
