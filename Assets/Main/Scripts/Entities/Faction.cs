using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Entities
{
    [CreateAssetMenu(fileName = "Faction", menuName = "ScriptableObjects/Faction", order = 1)]
    public class Faction : ScriptableObject
    {
        public string factionName;
        public Sprite factionBanner;
        public List<Faction> allies;
        public List<Faction> enemies;
        public bool enemyOfEnemies;
        public bool friendlyFire;
    }
}
