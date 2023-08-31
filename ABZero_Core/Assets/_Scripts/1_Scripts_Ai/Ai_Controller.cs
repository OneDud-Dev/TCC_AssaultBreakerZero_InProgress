using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        private NavMeshAgent    aiAgent;

        public Transform upperPartsPivot;
        public Transform lowerPartsPivot;

        [Header("States")]

        public aiSide       thisAiType;
        public aiNature     thisAiNature;
        public aiState      ThisAiState;

        
        public enum aiSide      { Enemy, Companion, Allied }
        public enum aiNature    { Chaser, Focused, Patroler }
        public enum aiState     { Moving, Attacking, Chasing}

        #endregion




        #region Unity
        private void Start()
        {
            aiMov   =   data.aiMov;
            aiCombt =   data.aiCombat;
            aiAim   =   data.aiAim;
            aiAgent =   data.aiAgent;
            
            if (thisAiNature == aiNature.Patroler)
                aiAgent.speed = 5;
            
        }

        private void Update()
        {
            SetObjectPosition(data.bodyPos.transform, transform);
            SetObjectPosition(data.aiPivot, transform);
            
            if (!data.pcData.gameIsRunning)
                return;

            switch (ThisAiState)
            {
                case aiState.Moving:
                    switch (thisAiNature)
                    {
                        case aiNature.Chaser:
                            aiMov.TravelMovement();
                            IfEnemiesChangeToAttack();
                            break;


                        case aiNature.Focused:
                            aiMov.TravelMovement();

                        break;

                        case aiNature.Patroler:
                            aiMov.PatrollingMovement();
                            IfEnemiesChangeToAttack();
                            break;
                    }

                    break;




                case aiState.Attacking:
                    switch (thisAiNature)
                    {
                        case aiNature.Chaser:
                            IfNoEnemtChangeToTraveling();
                            ChaserAttackPattern();
                            break;

                        case aiNature.Patroler:
                            IfNoEnemtChangeToPatrolling();
                            PatrollerAttackPattern();
                            break;


                        case aiNature.Focused:
                            break;
                    }

                    break;


                case aiState.Chasing:
                    break;
            }
            
        }

        #endregion

        


        #region Ctrl Methods


        private void IfEnemiesChangeToAttack()
        {
            if (aiCombt.enemyTargets == null || aiCombt.enemyTargets.Count <= 0)
                { return; }
            else if (aiCombt.enemyTargets.Count > 0)
            {
                aiAgent.speed = data.speedAttack;
                ThisAiState = aiState.Attacking;}

        }  //change to attacking enemy if list of enemies change


        private void IfNoEnemtChangeToTraveling()
        {
            if (aiCombt.enemyTargets == null || aiCombt.enemyTargets.Count <= 0)
            
            {   aiAgent.speed = data.speedTraveling;
                ThisAiState = aiState.Moving; }
        }
        private void IfNoEnemtChangeToPatrolling() //only speed change
        {
            if (aiCombt.enemyTargets == null || aiCombt.enemyTargets.Count <= 0)

            {
                aiAgent.speed = data.speedPatrolling;
                ThisAiState = aiState.Moving;
            }
        }

        public void ChaserAttackPattern()
        {
            IfNoEnemtChangeToTraveling();

            if (aiCombt.currentTarget == null)
            {  
                Debug.Log("Chaser has no enemy to orbit");
                if (aiCombt.enemyTargets == null)
                { ThisAiState = aiState.Moving; }
                else
                { aiCombt.currentTarget = aiCombt.enemyTargets[0]; }
                return;
            }
            else
            {
                aiMov.SimpleOrbitMovement(aiCombt.currentTarget.gameObject.transform.position,
                                            aiMov.orbitRadius);
                aiCombt.AiAttack();
            }
        }
        private void PatrollerAttackPattern()
        {
            IfNoEnemtChangeToPatrolling();
            if (aiCombt.currentTarget == null)
            {
                Debug.Log("Patroller has no enemy to orbit");
                if (aiCombt.enemyTargets == null)
                { ThisAiState = aiState.Moving; }
                else
                { aiCombt.currentTarget = aiCombt.enemyTargets[0]; }
                return;
            }
            else
            {
                aiMov.SimpleOrbitMovement(aiCombt.currentTarget.gameObject.transform.position,
                                          aiMov.orbitRadius);
                aiCombt.AiAttack();
            }
        }



        




        public void SetObjectPosition(Transform obj, Transform target)
        {
            obj.position = target.position;
        }




        public void aiDestroyed()
        {
            data.aiRoot.SetActive(false);
        }

        #endregion

    }
}