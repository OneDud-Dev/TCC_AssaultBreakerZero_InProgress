using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ABZ_Pc
{
    public class Pc_Health : MonoBehaviour
    {
        public Pc_References pcData;

        #region Variable

        [Header("text refs")]
        public RawImage indicatorGreen;
        public RawImage indicatorBlue;
        public RawImage indicatorYello;
        public RawImage indicatorRed;

        #endregion


        private void Start()
        {
            if (!indicatorGreen.gameObject.activeInHierarchy)
                { indicatorGreen.gameObject.SetActive(true);}
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (pcData.gameIsRunning)
            {
                case false: break;
                case true:
                    if (other.gameObject.CompareTag("Projectile_E"))
                    {
                        pcData.pcCtrl.hp--;

                        if (pcData.pcCtrl.hp == 11) //Blue
                        {
                            indicatorGreen.gameObject.SetActive(false);
                            indicatorBlue.gameObject.SetActive(true);
                        }
                        else if (pcData.pcCtrl.hp <= 8 && pcData.pcCtrl.hp >= 5)
                        {
                            indicatorBlue.gameObject.SetActive(false);
                            indicatorYello.gameObject.SetActive(true);
                        }
                        else if (pcData.pcCtrl.hp <= 4 && pcData.pcCtrl.hp >= 0)
                        {
                            indicatorYello.gameObject.SetActive(false);
                            indicatorRed.gameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {

            switch (pcData.gameIsRunning)
            {
                case false: break;
                case true:
                    if (collision.gameObject.CompareTag("Projectile_E"))
                        {
                            pcData.pcCtrl.hp--;

                        if (pcData.pcCtrl.hp == 11) //Blue
                        {
                            indicatorGreen.gameObject.SetActive(false);
                            indicatorBlue.gameObject.SetActive(true);
                        }
                        else if (pcData.pcCtrl.hp <= 8 && pcData.pcCtrl.hp >= 5)
                        {
                            indicatorBlue.gameObject.SetActive(false);
                            indicatorYello.gameObject.SetActive(true);
                        }
                        else if (pcData.pcCtrl.hp <= 4 && pcData.pcCtrl.hp >= 0)
                        {
                            indicatorYello.gameObject.SetActive(false);
                            indicatorRed.gameObject.SetActive(true);
                        }
                    }
                break;
            }
        }
    }
}
