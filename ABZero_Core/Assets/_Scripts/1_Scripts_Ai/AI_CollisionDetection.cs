using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ABZ_Ai
{
    public class AI_CollisionDetection : MonoBehaviour
    {


        //this script only sends information about projectile to listeners       

        public Ai_Health aiHP;
        public int damageFromMissile = 2, damageFromBullet = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Proj_P_B"))
            {
                aiHP.TakeDamage(damageFromBullet);
            }

            else if (other.gameObject.CompareTag("Proj_P_M"))
            {
                aiHP.TakeDamage(damageFromMissile);
            }
            else
                { return;}
        }
    }
}