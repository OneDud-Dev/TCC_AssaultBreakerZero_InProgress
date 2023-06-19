using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Health : MonoBehaviour
    {

        [SerializeField]
        private Ai_References aiData;
        public  Game_Events   OnAiDestroyed;



        private void TakeDamage(int dmge)
        {
            aiData.hP -= dmge;
            
            if (aiData.hP <= 0)     {OnZeroHP();}
            else                    {OnDamageReaction();   }
        }

        private void OnDamageReaction()
        {
            /* spawn hit particles
             * small shake to character torso bone, springlike
             * pause shooting
             */
        }
        
        

        private void OnZeroHP()
        {
            OnAiDestroyed.Raise(this, aiData.AiReference);
        }
    }
}
