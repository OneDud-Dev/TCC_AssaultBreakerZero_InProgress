using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerEnteredShootingRange : MonoBehaviour
    {
        public Game_Events OnEnteringShootingRange;
        public bool hasPassed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (hasPassed)
                { return; }

                OnEnteringShootingRange.Raise();
                hasPassed = true;
            }
        }
    }
}
