using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Main.Entities
{
    /// <summary>
    /// The entity stadistics, it handles the base stats and also applies modifiers to the base stats based on the strength, agility and intelligence stats.
    /// </summary>
    public class EntityStats : MonoBehaviour
    {
        [Header("Base stats")]
        public Stat maxHealth = new Stat(100);
        public Stat defense = new Stat(1);
        public Stat attackPower = new Stat(10);
        public Stat maxMana = new Stat(100);
        [Tooltip("Abilities are affected in different ways from this stat")]
        public Stat abilityPower = new Stat(0);
        [Tooltip("Attack speed affects certain animations, it works as a multiplier (1 = 100%)")]
        public Stat attackSpeed = new Stat(1);
        public Stat attackRange = new Stat(1.5f);
        public Stat movementSpeed = new Stat(1);
        [Tooltip("Health regenerated per second")]
        public Stat healthRegen = new Stat(0.1f);
        [Tooltip("Mana regenerated per second")]
        public Stat manaRegen = new Stat(1);

        [Header("Modifier stats")]
        [Tooltip("attack dmg,max life, armor and life regen")]
        public Stat strength = new Stat(1);
        [Tooltip("Attack Speed, movement speed, avoidance, crit chance, crit power")]
        public Stat agility = new Stat(1);
        [Tooltip("Ability Power, Mana, Mana Regen")]
        public Stat intelligence = new Stat(1);

        [Header("Entity Faction")]
        public Faction faction;

        
        private float currentHealth = 100;
        private float currentMana = 100;
        private void Start()
        {
            maxHealth.AddModifier("str", strength.GetValue() * 10);
            currentHealth = maxHealth.GetValue();
            currentMana = maxMana.GetValue();
            
        }

        private void Update()
        {
            currentHealth += healthRegen.GetValue() * Time.deltaTime;
            currentMana += manaRegen.GetValue() * Time.deltaTime;
            if (currentHealth > maxHealth.GetValue())
                currentHealth = maxHealth.GetValue();
            if (currentMana > maxMana.GetValue())
                currentMana = maxMana.GetValue();
        }

        private void UpdateBaseStats()
        {
            maxHealth.ChangeModifier("str", strength.GetValue() * 10);
            healthRegen.ChangeModifier("str", strength.GetValue() * 0.1f);
        }
    }
}
