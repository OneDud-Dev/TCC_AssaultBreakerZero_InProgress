using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ABZ_Ui
{
    public class Ui_HUD_Timer : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        public TMP_Text txtTimeValue;

        [Header("Time Variables")]
        //public string tempoDePartidaString;
        private float tempoDecorrido;
        private TimeSpan tempoJogando;

        [SerializeField] private bool tempoEstaPassando;

        #endregion



        #region Unity


        private void Start()
        {
            //tempoDePartidaString = "00:00:00";

            tempoEstaPassando = true;
            StartLevelTimer();
        }

        #endregion



        #region Funcoes

        public void StartLevelTimer()
        {


            tempoEstaPassando = true;
            tempoDecorrido = 0f;

            StartCoroutine(AtualizarTempo());
        }

        public void EndTimer()
        {
            tempoEstaPassando = false;
        }

        private IEnumerator AtualizarTempo()
        {
            while (tempoEstaPassando)
            {
                tempoDecorrido += Time.deltaTime;
                tempoJogando = TimeSpan.FromSeconds(tempoDecorrido);

                string tempoEmString = tempoJogando.ToString("mm' : 'ss'.'ff");
                txtTimeValue.text = (tempoEmString);

                yield return null;
            }
        }
        #endregion
    }
}
