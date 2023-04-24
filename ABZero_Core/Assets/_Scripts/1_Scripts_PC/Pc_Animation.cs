using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_Animation : MonoBehaviour
    {
        public  Pc_References   data;
        private Animator        pcAnimator;
        private Pc_Movement     pcMov;






        private void Start()
        {
            pcAnimator  = data.pcAnimator;
            pcMov       = data.pcMov;
        }

        private void FixedUpdate()
        {
            pcAnimator.SetFloat("Movement", pcMov.currentSpeed);
            pcAnimator.SetBool("isBoosting", pcMov.isBoosting);

        }
    }
}
