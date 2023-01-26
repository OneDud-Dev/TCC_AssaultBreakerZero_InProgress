using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_Animation : MonoBehaviour
    {
        public Pc_References data;
        private Animator pcAnimetor;
        private Pc_Movement pcMov;






        private void Start()
        {
            pcAnimetor = data.pcAnimator;
            pcMov = data.pcMov;
        }

        private void FixedUpdate()
        {
            pcAnimetor.SetFloat("Movement", pcMov.currentSpeed);
            pcAnimetor.SetBool("isBoosting", pcMov.isBoosting);

        }
    }
}
