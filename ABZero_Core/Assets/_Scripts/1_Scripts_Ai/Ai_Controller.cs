using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Controller : MonoBehaviour
    {
        [Header("References")]
        public Ai_References data;
        private Ai_Movement aiMov;
        private Ai_Combat aiCombt;
        private Ai_Aiming aiAim;

        [Header("States")]
        public aiType thisAiType;
        public aiTravelType thisAttitudeType;
        public aiState currentState;

        public enum aiType { Enemy, Companion, Allied }
        public enum aiTravelType { Chaser, Focused, Patroler }
        public enum aiState { Traveling, Attacking, Chasing }







        #region Unity

        private void Start()
        {
            aiMov = data.aiMov;
            aiCombt = data.aiCombat;
            aiAim = data.aiAim;
            #region  Ai movement debug

            if (aiMov.orbitDebug)
            {
                aiMov.ChangeOrbitTarget(aiCombt.enemyTargets[aiCombt.enemyIndex].gameObject.transform.position,
                                             aiMov.orbitRadius);
            }
            else if (aiMov.travelDebug)
            {

            }
            #endregion

        }

        private void Update()
        {
            #region Ai movement debug, disable later
            //this code will only work if debug is turned on
            if (aiMov.orbitDebug)
            {
                //movement
                aiMov.SimpleOrbitMovement(aiCombt.enemyTargets[aiCombt.enemyIndex].gameObject.transform.position,
                                               aiMov.orbitRadius);
                //aiming
                aiAim.PointUpperBodyToTarget(aiCombt.enemyTargets[aiCombt.enemyIndex].gameObject.transform.position);
                data.moveTarget.position = aiMov.currentDestination;


                return;
            }
            else if (aiMov.travelDebug)
            {
                data.aiMov.TravelMovement();
                return;
            }

            #endregion


        }

        #endregion




        private void IfEnemiesChangeToAttack()
        {
            if (aiCombt.enemyTargets == null || aiCombt.enemyTargets.Count <= 0) { return; }
            else if (aiCombt.enemyTargets.Count > 0) { currentState = aiState.Attacking; }

        }  //change to attacking enemy if list of enemies change

    }
}
