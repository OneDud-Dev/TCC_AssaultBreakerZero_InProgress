using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_TriggerPassedGates : MonoBehaviour
    {
        public Game_Events OnPassedGates;
        public bool hasPassed;


        public Animator Gate_L_S;
        public Animator Gate_L_B;
        public Animator Gate_R_S;
        public Animator Gate_R_B;

        public float gateCountdown;
        public float gateTargetTime;
        private bool stopCounting = true;




        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (hasPassed)
                { return; }

                OnPassedGates.Raise();
                hasPassed = true;
                stopCounting = false;
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

                Gate_L_S.SetBool("Open", true);
                Gate_L_B.SetBool("Open", true);
                Gate_R_S.SetBool("Open", true);
                Gate_R_B.SetBool("Open", true);
                stopCounting = true;
            }

        }
    }
}
