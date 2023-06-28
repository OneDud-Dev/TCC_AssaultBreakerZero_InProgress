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


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Proj_P_B"))
            {
                aiHP.TakeDamage(1);
            }

            else if (other.gameObject.CompareTag("Proj_P_M"))
            {
                aiHP.TakeDamage(1);
            }
            else
                { return;}
        }
    }
}