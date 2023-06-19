using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Controller : MonoBehaviour
    {


        #region Variables
        [Header("References")]
        public  Ai_References   data;
        private Ai_Movement     aiMov;
        private Ai_Combat       aiCombt;
        private Ai_Aiming       aiAim;

        [Header("States")]
        public aiType       thisAiType;
        public aiTravelType thisAttitudeType;
        public aiState      currentState;

        public enum aiType      { Enemy, Companion, Allied }
        public enum aiTravelType{ Chaser, Focused, Patroler }
        public enum aiState     { Traveling, Attacking, Chasing }

        #endregion




        #region Unity
        private void Start()
        {
            aiMov =     data.aiMov;
            aiCombt =   data.aiCombat;
            aiAim =     data.aiAim;

        }

        private void Update()
        {
            

        }

        private void FixedUpdate()
        {
            

        }

        

        #endregion




        #region Ctrl Methods
        

        public void aiDestroyed()
        {
            data.aiRoot.SetActive(false);
        }

        #endregion

    }
}