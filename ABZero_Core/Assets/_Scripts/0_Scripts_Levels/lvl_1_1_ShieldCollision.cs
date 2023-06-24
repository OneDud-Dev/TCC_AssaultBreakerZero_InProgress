using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_ShieldCollision : MonoBehaviour
    {
        public Game_Events OnDefending;
        public bool hasDefended;

        private void OnCollisionEnter(Collision collision)
        {
            if (hasDefended)
            {
                return;
            }

                if (collision.gameObject.CompareTag("Proj_P_M"))
                {
                    hasDefended = true;
                    OnDefending.Raise();
                }
            
        }
    }
}
