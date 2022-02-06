using Main.Entities.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Entities
{
    public class EntityAnimatorHandler : AnimatorManager
    {
        public Rigidbody entityRigidBody;
        public EntityStats stats;
        private Vector3 velocity;
        public override void Start()
        {
            base.Start();
            if (!entityRigidBody)
                entityRigidBody = GetComponent<Rigidbody>();
            if (!stats)
                stats = GetComponent<EntityStats>();

        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            entityRigidBody.drag = 0;
            float velY = entityRigidBody.velocity.y;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            velocity = deltaPosition / delta;
            velocity.y = velY;
            entityRigidBody.velocity = velocity * stats.movementSpeed.GetValue();
        }
    }
}
