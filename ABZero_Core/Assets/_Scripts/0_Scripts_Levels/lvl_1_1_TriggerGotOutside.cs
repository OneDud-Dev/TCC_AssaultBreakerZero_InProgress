using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerGotOutside : MonoBehaviour
    {
        public Game_Events gotOutsideEvent;
        public Game_Events activateFadeOutEvent;

        public bool hasPassed = false;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                hasPassed = true;
                gotOutsideEvent.Raise();
                activateFadeOutEvent.Raise();
            }
        }
    }
}
