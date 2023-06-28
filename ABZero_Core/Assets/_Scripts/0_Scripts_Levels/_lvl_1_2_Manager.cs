using ABZ_Pc;
using ABZ_Ai;
using System.Collections;
using System.Collections.Generic;
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
        public int enemiesDefeated;
        public GameObject lvl1_2_OST;

        public Game_Events EventEnding;

        private void Awake()
        {
            
        }

        private void Start()
        {
            pcData.pcShoot.leftArmAnimationMove = true;
            pcData.pcShoot.SetLeftArm();
            Instantiate(lvl1_2_OST, transform.position, transform.rotation);
        }






        public void WhenEnemyIsDefeated()
        {
            enemiesDefeated += 1;

            if (enemiesDefeated >= 11)
            {
                EndLevel();
            }
        }

        public void EndLevel()
        {
            if (levelHasEnded)
            {
                EventEnding.Raise();
                float t = 0;
                t += Time.deltaTime;

                if (t >= 10)
                {
                    SceneManager.LoadScene("01_StartScreen 1");
                }
            }
        }


        public IEnumerator ChangeToStart()
        {

            yield return new WaitForSeconds(10);
        }

    }
}
