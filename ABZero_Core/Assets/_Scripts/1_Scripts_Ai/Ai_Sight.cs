using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Sight : MonoBehaviour
    {
        #region Variables
        public Ai_References data;
        private SphereCollider sightTrigger;
        private Ai_Controller aiCtrl;
        private Ai_Combat aiCombat;

        public float sightRange;

        #endregion


        #region Unity

        private void Start()
        {
            aiCtrl = data.aiCtrl;
            aiCombat = data.aiCombat;

            sightTrigger = data.sightTrigger;
            sightTrigger.radius = sightRange;

        }


        private void OnTriggerEnter(Collider other)
        {
            switch (aiCtrl.thisAttitudeType)
            {
                case Ai_Controller.aiTravelType.Chaser:
                    ChaserLookForEnemy(other);
                    break;


                case Ai_Controller.aiTravelType.Focused:
                    FocusedLookForTarget(other);
                    break;


                #region Unused
                case Ai_Controller.aiTravelType.Patroler:
                    break;
                default:
                    break;

                #endregion
            }
        }

        #endregion

        private void ChaserLookForEnemy(Collider _char)
        {
            if (aiCombat.enemyTargets.Count == 0)
            {
                aiCombat.enemyTargets.Add(_char.gameObject);
            }

            else if (aiCombat.enemyTargets.Count > 0)
            {
                if (aiCombat.enemyTargets.Contains(_char.gameObject))
                { return; }
                else
                {
                    aiCombat.enemyTargets.Add(_char.gameObject);

                    /*mudar o alvo da IA para jogador a primeira vista, mal implementado
                    if (_char.CompareTag("Player"))
                    {
                        aiCombat.enemyTargets.RemoveAt(0);
                    }
                    */
                }
            }
        }


        private void FocusedLookForTarget(Collider _char)
        {
            if (aiCombat.enemyTargets.Count == 0 && !_char.CompareTag("Player"))
            {
                aiCombat.enemyTargets.Add(_char.gameObject);
            }

            else if (aiCombat.enemyTargets.Count > 0 && !_char.CompareTag("Player"))
            {
                if (!aiCombat.enemyTargets.Contains(_char.gameObject))
                {
                    aiCombat.enemyTargets.Add(_char.gameObject);
                }
            }
        }


        private void PatrollerLookForTarget(Collider _char)
        {
            //chase then return
        }

    }
}
