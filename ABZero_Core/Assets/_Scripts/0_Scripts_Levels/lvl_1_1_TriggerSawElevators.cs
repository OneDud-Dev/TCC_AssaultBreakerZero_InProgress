using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerSawElevators : MonoBehaviour
    {
        public Lvl1_1_Manager checkState;
        public Game_Events SawElevatorsEvent;
        public bool hasPassed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (checkState.currentLevelState == Lvl1_1_Manager.LevelState.HasExploded)
                {
            
                    if (hasPassed)
                    { return; }

                    SawElevatorsEvent.Raise();
                    hasPassed = true;
                }
            }
        }
    }
}
