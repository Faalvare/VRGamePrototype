using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Entities.Actions
{
    [CreateAssetMenu(menuName ="AI/Entity Actions/Attack Action")]
    public class EntityAttackAction : EntityAction
    {
        [Tooltip("Higher weight makes it more likely to be used by the entity")]
        public int weight = 1;
        public float recoveryTime = 2;
        public float minimumAttackAngle = -35;
        public float maximumAttackAngle = 35;
        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 3;
    }
}
