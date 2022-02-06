using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    /// <summary>
    /// A singleton that handles the "RPG" interactions, things like Damage, Avoidance, Drops, Etc...
    /// </summary>
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster instance;

        private void Start()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(instance);
                instance = this;
            }
        }
    }
}
