using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZCore
{
    public class lvl_1_3_TriggerTargetLines : MonoBehaviour
    {
        public lvl_1_0_LvController lvl1Ctrl;

        private void OnTriggerEnter(Collider other)
        {
            if (lvl1Ctrl.currentTrigger == lvl_1_0_LvController.lvlTriggers.EnteredArea
                                        &&
                                        other.gameObject.CompareTag("Player"))
            {
                lvl1Ctrl.TargetDialogue();
            }
        }
    }
}
