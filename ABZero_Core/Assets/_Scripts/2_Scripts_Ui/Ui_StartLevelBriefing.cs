using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using ABZ_GameSystems;
using System;

namespace ABZ_Ui
{
    public class Ui_StartLevelBriefing : MonoBehaviour
    {
        public Game_AudioController _audio;
        public SO_LevelBriefing levelBriefinginfo;
        public enum briefingType { Starting, Ending}
        public briefingType thisBriefingScreen;

        public TMP_Text levelName;
        public TMP_Text levelDescription;
        public TMP_Text levelObjectives;
        public TMP_Text LevelTasksOrEnemies;
        public TMP_Text LevelTimer;



        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            InitiateLevelBriefing(levelBriefinginfo);
        }



        //for reusing the scene but with diferent information, determined by GameController and state machine;
        public void InitiateLevelBriefing(SO_LevelBriefing briefingLevelData)
        {

            if (thisBriefingScreen == briefingType.Ending)
            {
                var tempo = TimeSpan.FromSeconds(levelBriefinginfo.endingTime);
                LevelTimer.text = tempo.ToString("mm' : 'ss'.'ff");
            }
        }

        public void BackToCampaingMenu ()
        {
            _audio.Play("Back");
            SceneManager.LoadScene("01_StartScreen");


        }

        public void Beginlvl1()
        {
            _audio.Play("Clicked");
            levelBriefinginfo.endingTime = 0;
            SceneManager.LoadScene("04_lvl_1_1_TrainingDay_Underground");
        }
    }
}
