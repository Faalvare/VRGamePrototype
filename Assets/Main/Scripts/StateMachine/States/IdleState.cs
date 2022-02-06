using Main.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Entities.AI.StateMachine.States
{
    public class IdleState : State
    {
        public LayerMask detectionLayer;
        public State chaseState;
        public override State Tick(EntityAnimatorHandler animatorHandler, EntityStats stats, EntityAI entityAI)
        {
            #region Find a Target
            Collider[] colliders = Physics.OverlapSphere(transform.position, entityAI.chaseRadius, detectionLayer);
            foreach (var col in colliders)
            {
                EntityStats targetStats = col.GetComponent<EntityStats>();
                if (targetStats && targetStats != stats)
                {
                    Vector3 targetDirection = targetStats.transform.position - transform.position;
                    if (stats.faction.enemyOfEnemies)
                    {
                        if (targetStats.faction.enemies.Contains(stats.faction))
                        {
                            entityAI.currentTarget = targetStats;
                            break;
                        }
                    }
                    if (stats.faction.enemies.Contains(targetStats.faction))
                    {
                        entityAI.currentTarget = targetStats;
                        break;
                    }
                }
            }
            #endregion

            #region Switch to next state
            if (entityAI.currentTarget != null)
                return chaseState;
            else
                return this;
            #endregion
        }
    }
}
