using ABZ_Pc;
using ABZ_Ui;

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ABZ_GameSystems;

namespace ABZ_Levels
{
    public class _lvl_1_2_Manager : MonoBehaviour
    {
        public bool levelHasEnded;
        public Pc_References pcData;
        public GameObject lv1Dialogues;
        public Ui_HUD_Timer playTime;
        public int enemiesDefeated;
        public GameObject lvl1_2_OST;

        public Game_Events EventEnding;
        public float t = 0;
        

        private void Start()
        {
            pcData.pcShoot.leftArmAnimationMove = true;
            pcData.pcShoot.SetLeftArm();
            Instantiate(lvl1_2_OST, transform.position, transform.rotation);
        }

        private void Update()
        {

            if (levelHasEnded) { EndLevel(); ; }
            

        }




        public void WhenEnemyIsDefeated()
        {
            enemiesDefeated += 1;

            if (enemiesDefeated >= 9)
            {
                levelHasEnded = true;
            }
        }

        public void EndLevel()
        {
            
            EventEnding.Raise();
            playTime.EndTimer();
            t += Time.deltaTime;

            if (t >= 10)
            {
                SceneManager.LoadScene("01_StartScreen 1");
            }
        }


        public IEnumerator ChangeToStart()
        {

            yield return new WaitForSeconds(10);
        }

    }
}
