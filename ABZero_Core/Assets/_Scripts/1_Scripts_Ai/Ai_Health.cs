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
        public  Game_Events     UnityOnAiDestroyed;
        public Game_Events      CustonEventRemoveTarget;


        public void TakeDamage(int dmge)
        {
            aiData.hP -= dmge;
            if (aiData.hP <= 0)     {OnZeroHP();}
        }

        

        public void OnZeroHP()
        {
            aiData.aiRoot.SetActive(false);
            UnityOnAiDestroyed.Raise();
            CustonEventRemoveTarget.Raise(this, aiData.AiReference);
        }
    }
}
