using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class _lvl_1_2_Everything : MonoBehaviour
    {
        public Game_Events EverythingEvent
             ;
        public bool hasPassed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (hasPassed)
                { return; }

                EverythingEvent.Raise();
                hasPassed = true;
            }
        }
    }
}
