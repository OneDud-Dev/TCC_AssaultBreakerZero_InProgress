using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ABZ_Pc
{
    public class Pc_Animation : MonoBehaviour
    {

        #region References
        public  Pc_References   data;
        private Animator        pcAnimator;
        private Pc_Movement     pcMov;

        public AnimationCurve activationCurve;
        public AnimationCurve deactivationCurve;
        public AnimationCurve reloadCurveOff;
        public AnimationCurve reloadCurveOn;

        #endregion

        #region Animator parameters
        //Note:the HASH is an INT number aquired is an Id for the parameter of the animator, not it's values
        private int  forwardPmeter    = Animator.StringToHash("VelForward");
        private int  strafePmeter     = Animator.StringToHash("VelStrafe");
        private int  isRunningPmeter  = Animator.StringToHash("isRunning");
        private int  isBoostingPmeter = Animator.StringToHash("IsBoosting");

        #endregion

        #region Rigging References
        private TwoBoneIKConstraint leftArmCtl;
        private TwoBoneIKConstraint rightArmCtl;
        private MultiRotationConstraint specialArmCtl;
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

            leftArmCtl  = data.leftArmCtrl;
            rightArmCtl = data.rightArmCtrl;
            specialArmCtl = data.specialArmRotationCtrl;
        }

        private void Update()
        {
            pcAnimator.SetFloat(strafePmeter,     pcMov.ForwardLateralVertical.x);
            pcAnimator.SetFloat(forwardPmeter,    pcMov.ForwardLateralVertical.z);
            pcAnimator.SetBool (isRunningPmeter,  pcMov.isRunning);
            pcAnimator.SetBool (isBoostingPmeter, pcMov.isBoosting);
        }

        #region activation deactivation
        public void ActivateLeftArm(float _time)       => leftArmCtl.weight    = activationCurve.Evaluate(_time);
        public void  ReloadLeftArmOff(float _time)     => leftArmCtl.weight    = reloadCurveOff.Evaluate(_time);
        public void ReloadLeftArmOn(float _time)        => leftArmCtl.weight   = reloadCurveOff.Evaluate(_time);
        public void ActivateRightArm(float _time)      => rightArmCtl.weight   = activationCurve.Evaluate(_time);
        public void ActivateSpecialArm(float _time)    => specialArmCtl.weight = activationCurve.Evaluate(_time);

        public void DeactivateLeftArm(float _time)     => leftArmCtl.weight    = deactivationCurve.Evaluate(_time);
        public void DeactivateRightArm(float _time)    => rightArmCtl.weight   = deactivationCurve.Evaluate(_time);
        public void DeactivateSpecialArm(float _time)  => specialArmCtl.weight = deactivationCurve.Evaluate(_time);

        #endregion
    
    }
}
