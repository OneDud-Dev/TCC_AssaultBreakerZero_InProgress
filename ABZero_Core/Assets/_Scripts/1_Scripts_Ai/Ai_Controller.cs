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

            SetObjectPosition(data.bodyPos.transform, transform);
            SetObjectPosition(data.aiPivot, transform);
            RotateObjOverTime(data.upperBodyPivot.transform, transform, 2);
            RotateObjOverTime(data.bodyPos.transform, transform, 2);


            switch (currentState)
            {
                case aiState.Traveling:
                    switch (thisAttitudeType)
                    {
                        case aiTravelType.Chaser:
                            aiMov.TravelMovement();
                            IfEnemiesChangeToAttack();
                            break;


                        case aiTravelType.Focused:
                            aiMov.TravelMovement();
                            IfEnemiesChangeToAttack();
                            break;

                        #region unused
                        case aiTravelType.Patroler:
                            break;
                        default:
                            break;

                            #endregion
                    }

                    break;




                case aiState.Attacking:
                    IfNoEnemtChangeToTraveling();
                    if (aiCombt.currentTarget == null)
                    {
                        aiMov.SimpleOrbitMovement(aiCombt.enemyTargets[aiCombt.enemyIndex].gameObject.transform.position,
                                                    aiMov.orbitRadius);
                        data.aiAim.PointUpperBodyToTarget(aiCombt.enemyTargets[aiCombt.enemyIndex].gameObject.transform.position, data.upperBodyPivot);
                        aiCombt.AiAttack();
                    }
                    else
                    {
                        aiMov.SimpleOrbitMovement(aiCombt.currentTarget.gameObject.transform.position,
                                                    aiMov.orbitRadius);
                        data.aiAim.PointUpperBodyToTarget(aiCombt.currentTarget.gameObject.transform.position, data.upperBodyPivot);
                        aiCombt.AiAttack();
                    }


                    break;




                case aiState.Chasing:
                    break;




                default:
                    break;
            }


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


        public void SetObjectPosition(Transform obj, Transform target)
        {
            obj.position = target.position;
        }

        public void RotateObjOverTime(Transform bodyObj, Transform target, float rotationSpeed)
        {
            bodyObj.rotation = Quaternion.RotateTowards(bodyObj.rotation,
                                                        target.rotation,
                                                        rotationSpeed);
        }


        public void aiDestroyed()
        {
            data.aiRoot.SetActive(false);
        }

        #endregion

    }
}