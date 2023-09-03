using ABZ_Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ABZ_Levels
{
    public class lvl_1_1_Trigger_TutorialEND : MonoBehaviour
    {
        public Ui_HUD_Timer part1Ttimer;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                part1Ttimer.SaveTempoDecorrido();
                SceneManager.LoadScene("05_lvl_1_2_TrainingDay_Surface");
            }
        }
    }
}
