using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ABZ_Ai
{
    public class AI_CollisionDetection : MonoBehaviour
    {


        //this script only sends information about projectile to listeners       

        public Game_Events OnAiDamageEvent;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile_A") ||
                other.gameObject.CompareTag("Projectile_P"))
            {
                OnAiDamageEvent.Raise(this, other);
            }

            else
                { return;}

        }
    }
}