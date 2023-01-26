using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ui
{
    public class Ui_HUD_Targets : MonoBehaviour
    {
        Vector3 lookDirection;

        public void SetTargetPosition(Transform pcPos)
        {
            this.gameObject.transform.position = pcPos.position;
        }

        public void PoinToPlayer(Transform pcPos)
        {
            lookDirection = new Vector3(pcPos.position.x - this.gameObject.transform.position.x,
                                        0,
                                        pcPos.position.z - this.gameObject.transform.position.z);

            this.gameObject.transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
