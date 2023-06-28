using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerArrivedAtGate : MonoBehaviour
    {
        public Game_Events atArrivingAtGate;
        public Lvl1_1_Manager checkLevelState;
        public bool hasPassed = false;

        
        public Animator Gate_L_S;
        public Animator Gate_L_B;
        public Animator Gate_R_S;
        public Animator Gate_R_B;

        private int open = Animator.StringToHash("Open");

        public float gateCountdown;
        public float gateTargetTime;
        private bool stopCounting = true;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (checkLevelState.currentLevelState != Lvl1_1_Manager.LevelState.HasSawElevator)
                {
                    return;
                }

                if (hasPassed)
                {
                    return;
                }

                hasPassed = true;
                stopCounting = false;
                atArrivingAtGate.Raise();
            }
        }


        private void FixedUpdate()
        {
            if (stopCounting)
            {
                return;
            }

            gateCountdown += Time.deltaTime;

            if (gateCountdown >= gateTargetTime)
            {
                stopCounting = true;

                Gate_L_S.SetBool(open, true);
                Gate_L_B.SetBool(open, true);
                Gate_R_S.SetBool(open, true);
                Gate_R_B.SetBool(open, true);
            }
        }
    }
}
