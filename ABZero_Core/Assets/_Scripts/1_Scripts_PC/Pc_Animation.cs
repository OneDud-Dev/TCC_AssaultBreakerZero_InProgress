using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_Animation : MonoBehaviour
    {

        #region References
        public  Pc_References   data;
        private Animator        pcAnimator;
        private Pc_Movement     pcMov;

        #endregion

        #region Animator parameters
        //Note:the HASH is an INT number aquired is an Id for the parameter of the animator, not it's values
        private int  forwardPmeter    = Animator.StringToHash("VelForward");
        private int  strafePmeter     = Animator.StringToHash("VelStrafe");
        private int  isBoostingPmeter = Animator.StringToHash("IsBoosting");

        #endregion

        //animation problem, camera rotation
        //solution separate walk animation in relation to bodymotion >
        //(RotateObjOverTime(bodyRootPos, mainPivot, pcVamtOptions.pivotRotatioValue))

        //also add animation rigging for aiming

        /*About IK Arms
         * Use Inputs to change Weight of rigging poses to activate it (shield)
         * use timer to since last shot to desable right arm weight, instant set when input activate
        */

        private void Start()
        {
            pcAnimator  = data.pcAnimator;
            pcMov       = data.pcMov;

            

           // pcAnimator
        }

        private void Update()
        {
            pcAnimator.SetFloat(strafePmeter,     pcMov.ForwardLateralVertical.x);
            pcAnimator.SetFloat(forwardPmeter,    pcMov.ForwardLateralVertical.z);
            pcAnimator.SetBool (isBoostingPmeter, pcMov.isBoosting);

        }




    }
}
