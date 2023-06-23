using ABZ_GameSystems;
using ABZ_Pc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class Lvl1_1_Manager : MonoBehaviour
    {
        public Pc_References pcData;
        public int  HitTargets;
        public bool allTargetFliped;
        public bool targetExploded;
        public bool playerDefended;

        public string[] Objectives;

        public Game_Events[] ObjectiveEvents;

        public Game_Events[] DialogueEvents;

        public enum LevelState { Started, HasDefended, HasFliped, HasExploded, HasSawElevator, HasArrivedGate, GateOpened, HasGotOutside }
        public LevelState currentLevelState;




        private void Start()
        {
            ObjectiveEvents[0].Raise(this, Objectives[0]);
            pcData.pcShoot.leftIsActive = false;
            pcData.pcShoot.rightIsActive = false;
            pcData.pcShoot.specialIsActive = false;
        }

        



        // event methods
        public void IncrementTargets()
        {
            HitTargets++;
            if (HitTargets >= 4)
            {
                allTargetFliped = true;
                ObjectiveEvents[3].Raise(this, Objectives[3]) ;
            }
        }

        public void PlayerHasEnteredShootingRange()
        {
            pcData.pcShoot.rightIsActive = true;
            ObjectiveEvents[1].Raise(this, Objectives[1]) ; 
        }

        public void PlayerUsedShield()
        {
            ObjectiveEvents[2].Raise(this, Objectives[2]) ; 
            playerDefended = true;
            currentLevelState = LevelState.HasDefended;
            pcData.pcShoot.leftIsActive = true;
        }

        public void PlayerHasFlipedTargets()
        {
            ObjectiveEvents[3].Raise(this, Objectives[3]) ;
            allTargetFliped = true;
            currentLevelState = LevelState.HasFliped;
            pcData.pcShoot.specialIsActive = true;
        }
        //am settubg methods to change objectives and states and dialogues of 1-1 Level
        public void ExplodeTarget()
        {
            ObjectiveEvents[4].Raise(this, Objectives[4]) ;
            targetExploded = true;
            currentLevelState = LevelState.HasExploded;
        }


    }
}
