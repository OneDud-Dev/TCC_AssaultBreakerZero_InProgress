using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_EnteredShootingRange : MonoBehaviour
    {
        public Game_Events OnEnteringShootingRange;
        

        private void OnTriggerEnter(Collider other)
        {
            OnEnteringShootingRange.Raise();
        }
    }
}
