using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerElevatorDestroyed : MonoBehaviour
    {
        public Game_Events sawElevatorEvent;
        public Lvl1_1_Manager seeLevelState;
        public bool hasPassed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (seeLevelState.currentLevelState == Lvl1_1_Manager.LevelState.HasExploded)
                {//just destroyed the target and is going back to elevators

                    if (hasPassed)
                    { return; }

                    sawElevatorEvent.Raise();
                    hasPassed = true;
                }
            }
        }
    }
}
