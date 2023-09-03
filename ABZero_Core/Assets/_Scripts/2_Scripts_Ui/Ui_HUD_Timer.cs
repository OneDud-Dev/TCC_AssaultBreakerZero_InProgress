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
        public SO_LevelBriefing saveTimerToSO;

        [Header("Time Variables")]
        //public string tempoDePartidaString;
        private float tempoDecorrido = 0;
        private TimeSpan tempoJogando;

        [SerializeField] private bool tempoEstaPassando;

        #endregion



        #region Unity


        private void Start()
        {
            

            tempoEstaPassando = true;
            StartLevelTimer();
        }

        #endregion



        #region Funcoes

        public void StartLevelTimer()
        {


            tempoEstaPassando = true;
            tempoDecorrido += saveTimerToSO.endingTime;

            StartCoroutine(AtualizarTempo());
        }

        public void EndTimer()
        {
            tempoEstaPassando = false;
        }

        public void SaveTempoDecorrido() => saveTimerToSO.endingTime += tempoDecorrido;

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
