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
            switch (aiCtrl.thisAiNature)
            {
                case Ai_Controller.aiNature.Chaser:
                    if (other.gameObject.CompareTag("Player") )
                    
                        {ChaserLookForEnemy(other);}
                        break;


                case Ai_Controller.aiNature.Focused:
                    if (other.gameObject.CompareTag("Player"))
                    
                        {FocusedLookForTarget(other);}
                        break;

                case Ai_Controller.aiNature.Patroler:
                    if (other.gameObject.CompareTag("Player"))

                        { PatrollerLookForTarget(other); }
                        break;
   
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
                    aiCombat.currentTarget = _char.gameObject;
                    aiCombat.enemyTargets.Add(_char.gameObject);
                }
            }
        }


        private void FocusedLookForTarget(Collider _char) //change to List<GameObject> of target objectives
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
            if (aiCombat.enemyTargets.Count == 0)
            {
                aiCombat.enemyTargets.Add(_char.gameObject);
                aiCombat.currentTarget = _char.gameObject;
            }

            else if (aiCombat.enemyTargets.Count > 0)
            {
                if (aiCombat.enemyTargets.Contains(_char.gameObject))
                { return; }
                aiCombat.enemyTargets.Add(_char.gameObject);
            }
        }
    }
}
