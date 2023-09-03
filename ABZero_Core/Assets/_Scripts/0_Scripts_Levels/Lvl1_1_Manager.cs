using ABZ_GameSystems;
using ABZ_Pc;
using ABZ_Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class Lvl1_1_Manager : MonoBehaviour
    {
        public Pc_References pcData;
        public SO_LevelBriefing levelBriefingTime;
        public int  HitTargets;
        public bool allTargetFliped;
        public bool targetExploded;
        public bool playerDefended;

        public string[] Objectives;

        public Game_Events[] ObjectiveEvents;

        public Game_Events[] DialogueEvents;

        public enum LevelState { Started, HasDefended, HasFliped, HasExploded, HasSawElevator, HasArrivedGate, GateOpened, HasGotOutside }
        public LevelState currentLevelState;

        
        public Lvl1_1_MLncher lclLaucher;
        public GameObject Elevetors;

        public GameObject[] sounds;


        private void Start()
        {
            ObjectiveEvents[0].Raise(this, Objectives[0]);
            pcData.leftOverride     = true;
            pcData.rightOverride    = true;
            pcData.specialOverride  = true;

            sounds[0] = Instantiate(sounds[0], transform.position, transform.rotation);
            
        }



        // INCREMENT EVENTS ARE CUSTOM EVENTS
        // OBJECTIVE EVENTS ARE CUSTOM EVENTS
        // LEVELSTATE EVENTS ARE UNITY EVENTS

        public void IncrementTargets()
        {
            HitTargets++;
            if (HitTargets >= 4)
            {
                allTargetFliped = true;
                ObjectiveEvents[3].Raise(this, Objectives[3]) ;
                PlayerHasFlipedTargets();
            }
        }


       
        public void PlayerHasEnteredShootingRange()
        {
            // UNITY EVENT RAISED BY CORRIDOR TRIGGER
            pcData.rightOverride = false;
            ObjectiveEvents[1].Raise(this, Objectives[1]) ; 
            
        }

        public void PlayerUsedShield()
        {
            //EVENT RAISED BY SHIELD COLLIDER
            lclLaucher.enabled = false;
            playerDefended = true;
            pcData.leftOverride = false;
            pcData.pcShoot.leftArmAnimationMove = true;
            pcData.pcShoot.ui_LaserPointer.SetActive(true);
            pcData.pcShoot.SetLeftArm();
            currentLevelState = LevelState.HasDefended;
            ObjectiveEvents[2].Raise(this, Objectives[2]) ; 
        }

        public void PlayerHasFlipedTargets()
        {
            DialogueEvents[3].Raise();
            //EVENTS INCREMENTED BY EACH TARGET
            allTargetFliped = true;
            pcData.specialOverride = false;
            currentLevelState = LevelState.HasFliped;
            ObjectiveEvents[3].Raise(this, Objectives[3]) ;
        }
        
        public void PlayerExplodeTarget()
        {
            DialogueEvents[4].Raise();
            //EVENT RAISED BY BIG TARGET EXPLOSION
            targetExploded = true;
            currentLevelState = LevelState.HasExploded;
            ObjectiveEvents[4].Raise(this, Objectives[4]) ;

            //SPAWN BOMBARDMENT SOUNDS
            Elevetors.SetActive(true);
        }

        public void PlayerAfterMissiles()
        {
            sounds[0].GetComponent<AudioSource>().Stop();
            sounds[1] = Instantiate(sounds[1], transform.position, transform.rotation);
            sounds[2] = Instantiate(sounds[2], transform.position, transform.rotation);


        }

        public void PlayerSawElevator()
        {
            ObjectiveEvents[5].Raise(this, Objectives[5]);
            currentLevelState = LevelState.HasSawElevator;
        }

        public void PlayerArrivedAtGate()
        {
            //EVENT RAISED BY GATE ROOM TRIGGER
            //activate gate animation open
            currentLevelState = LevelState.HasArrivedGate;
            sounds[2].GetComponent<AudioSource>().Stop();

        }

        public void PlayerPastGate()
        {
            //EVENT RAISED BY SECOND TRIGGER PAST THE GATE
            //OPEN SECOND GATE
            currentLevelState = LevelState.GateOpened;
        }
        public void PlayerOutSide()
        {
            //EVENT RAISED BY THIRD TRIGGER
            //DIALOGUE
            //DEACTIVATE PLAYER
            //CHANGE LVEL TO 1-1
        }
    }
}
