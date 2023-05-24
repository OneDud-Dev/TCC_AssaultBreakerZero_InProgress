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
        private void IfEnemiesChangeToAttack()
        {
            if (aiCombt.enemyTargets == null || aiCombt.enemyTargets.Count <= 0) { return; }
            else if (aiCombt.enemyTargets.Count > 0) { currentState = aiState.Attacking; }

        }  //change to attacking enemy if list of enemies change
        private void IfNoEnemtChangeToTraveling()
        {
            if (aiCombt.enemyTargets == null || aiCombt.enemyTargets.Count <= 0)
            { currentState = aiState.Traveling; }
        }



        public void aiDisabled()
        {
            data.aiRoot.SetActive(false);
        }

        #endregion

    }
}