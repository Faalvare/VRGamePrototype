using UnityEngine;
using Main.Entities;

namespace Main.Entities.AI.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EntityAnimatorHandler animatorHandler, EntityStats stats, EntityAI entityAI);
    }
}
