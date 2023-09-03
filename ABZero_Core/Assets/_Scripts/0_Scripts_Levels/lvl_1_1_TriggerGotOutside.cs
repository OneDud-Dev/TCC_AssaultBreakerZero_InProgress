using ABZ_GameSystems;
using ABZ_Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerGotOutside : MonoBehaviour
    {
        public Game_Events gotOutsideEvent;
        public Game_Events activateFadeOutEvent;
        public Ui_HUD_Timer hudTimer;
        public bool hasPassed = false;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                hasPassed = true;
                hudTimer.SaveTempoDecorrido();

                gotOutsideEvent.Raise();
                activateFadeOutEvent.Raise();
            }
        }
    }
}
