using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class _lvl1_2_TriggerDestruction : MonoBehaviour
    {
        public Game_Events OnSawDestructionFirst;
        public bool hasPassed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (hasPassed)
                { return; }

                OnSawDestructionFirst.Raise();
                hasPassed = true;
            }
        }
    }
}
