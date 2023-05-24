using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Health : MonoBehaviour
    {

        [SerializeField]
        private Ai_References aiData;




        private void TakeDamage(int dmge)
        {
            aiData.hP -= dmge;
            
            if (aiData.hP <= 0)     {onBeingDestroyed();}
            else                    {DamageStagger();   }
        }

        private void DamageStagger()
        {
            /* spawn hit particles
             * small shake to character torso bone, springlike
             * pause shooting
             */
        }
        
        
        private void onBeingDestroyed()
        {
            /* spawn explosion
             * send event to add enemy destroyed count to level controller
             * desable ai
             */
        }
    }
}
