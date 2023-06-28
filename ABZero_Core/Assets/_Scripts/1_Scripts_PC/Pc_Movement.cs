using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ABZ_Pc
{
    public class Pc_Movement : MonoBehaviour
    {

        public Pc_References pcData;

        #region Variables

        public bool isRunning;
        public bool isBoosting;

        #region References

        [Header("Object References")]
        private Transform forwardReference;
        private Transform lowerBody;
        private Rigidbody sphereMov;
      //public  Rigidbody rbBodyVelocity;
        private Transform pivotPos;

        #endregion

        #region Enums
        public enum MovementType { Walking, Running, Boosting }
        public enum verticality { Grounded, Suspended }
        #endregion

        #region MovementData

        [Header("Movement Status")]
        public MovementType currentMovement;
        public verticality  currentVerticalStatus;
        public LayerMask    groundLayer;
        public float        groundCheckLength;

        [Header("Velocity on each axis")]
        public Vector3 ForwardLateralVertical;
        
        [Header("Movement forces")]
        //input value received by pcCtrl
        [SerializeField] private float forwardForce;
        [SerializeField] private float horizontalForce;
        [SerializeField] private float downwardForce;
        [SerializeField] private float RotationForce;


        [Header("Movement Value")]
        public bool  isCalculatingSpeed;
        public float currentSpeed;
      //public float currentVerticalSpeed; Deprecieted
        public float currentAltitudeValue;
        public float currentAngleValue;
        #endregion

        #endregion



        //_______________________________________UNITY_______________________________________

        #region Unity Setup
        private void Start()
        {
            pcData.pcCtrl.isPlaying = true;
            isCalculatingSpeed      = true;

            sphereMov           = pcData.sphereMov;
            forwardReference    = pcData.normalControlForward;
            pivotPos            = pcData.mainPivot;

            StartCoroutine(ICalculateSpeed());
        }

        #endregion





        #region Unity Calculations
        private void Update()
        {
            switch (pcData.gameIsRunning)
            {
                case false:
                    break;
                case true:
                    CheckIfIsGrounded(currentAltitudeValue, groundCheckLength);
                    ActivateSpeedCalculation();// calculate speed data for amny references
                    //GetDownwardForce(pcData.vamtSettings.gravityCurve, currentAltitudeValue, 1);
                    break;
            }
        }



        private void FixedUpdate()
        {
            switch (pcData.gameIsRunning)
            {
                case false:
                    break;
                case true:

                    currentAltitudeValue = GetAltitude(sphereMov.position, -pivotPos.up);
                    CustomGravity(sphereMov, pivotPos, 5, 100);

                    //movement calculations
                    switch (currentVerticalStatus)
                    {
                        case Pc_Movement.verticality.Grounded:
                            switch (currentMovement)
                            {
                                case Pc_Movement.MovementType.Walking:
                                    ForwardMovement (sphereMov, forwardReference, forwardForce);
                                    StrafeMovement  (sphereMov, forwardReference, horizontalForce);
                                    
                                    break;

                                case Pc_Movement.MovementType.Running:
                                    ForwardMovement (sphereMov, forwardReference, forwardForce);
                                    StrafeMovement  (sphereMov, forwardReference, horizontalForce);
                                  
                                    break;
                                case Pc_Movement.MovementType.Boosting:
                                    ForwardMovement(sphereMov, forwardReference, forwardForce);
                                    StrafeMovement(sphereMov, forwardReference, horizontalForce);
                                  
                                    break;
                            }
                            break;

                       //------------------------------------------------------------------------------------------------
                        case Pc_Movement.verticality.Suspended:
                            switch (currentMovement)
                            {
                                case Pc_Movement.MovementType.Walking:
                                    ForwardMovement (sphereMov, forwardReference, forwardForce);
                                    StrafeMovement  (sphereMov, forwardReference, horizontalForce);
                                  
                                    break;

                                case Pc_Movement.MovementType.Running:
                                    ForwardMovement (sphereMov, forwardReference, forwardForce);
                                    StrafeMovement  (sphereMov, forwardReference, horizontalForce);
                                  
                                    isBoosting = true;
                                    break;

                                case Pc_Movement.MovementType.Boosting:
                                    ForwardMovement(sphereMov, forwardReference, forwardForce);
                                    StrafeMovement(sphereMov, forwardReference, horizontalForce);
                                  
                                    break;
                            }

                            //------------------------------------------------------------------------------------------------
                            break;
                    }
                break;
            }
        }

        #endregion




        //_______________________________________METHODS_______________________________________
        //________Methods that require player input values are applied at pc Controller________
        //_____________________________________________________________________________________


        #region MovementRelatedFunctions

        public void SetMovementType()
        {
            if (isRunning && !isBoosting)
            {
                currentMovement = MovementType.Running;
            }
            else if (isBoosting)
            {
                currentMovement = MovementType.Boosting;
            }
            else { currentMovement = MovementType.Walking;}
        }
        //||||||||||||||||||||||||||||||||||||||||||||FORWARD||||||||||||||||||||||||||||||||||||||||||||||||||||\\

        /// <summary>
        /// Receives player input then calculates the Force value to be applied to move the physical object
        /// </summary>
        /// <param name="_inputValue">Value received from InputManager</param>
        /// <param name="_forwardPower">Power Value of specific mecha legs equipped</param>
        /// <param name="_multiplier">Constant multiplier</param>
        public void AccelInputValue(float _YInputVector, float _forwardPower, int _multiplier)
        {
            forwardForce = _YInputVector * _forwardPower * _multiplier;
        }



        /// <summary>
        /// Method used to apply force to the character RigidyBody on it's forward vector
        /// </summary>
        /// <param name="_sphereRb">Character RigidyBody</param>
        /// <param name="_forwardDir">Character Forward vector</param>
        /// <param name="_accel">Force to be applied into the character</param>
        public void ForwardMovement(Rigidbody _sphereRb, Transform _forwardDir, float _accel)
        {
            _sphereRb.AddForce(_forwardDir.forward * _accel, ForceMode.Acceleration);
        }



        //_______________________________________________________________________________________________________
        //||||||||||||||||||||||||||||||||||||||||||||BOOST||||||||||||||||||||||||||||||||||||||||||||||||||||\\


        public float BoostInputValue(AnimationCurve _boostCurve, float _timeValue, int _multiplier)
        {
            float _accel = _boostCurve.Evaluate(_timeValue) * _multiplier;
            return _accel;
        }



        //_______________________________________________________________________________________________________
        //||||||||||||||||||||||||||||||||||||||||||||STRAFE||||||||||||||||||||||||||||||||||||||||||||||||||||\\

        public void StrafeInputValue(float _XInputVector, float _horizontalPower, int _multiplier)
        {
            horizontalForce = _XInputVector * _horizontalPower * _multiplier;
        }
        public void StrafeMovement(Rigidbody _sphereRb, Transform _horizontalDir, float _accel)
        {
            _sphereRb.AddForce(_horizontalDir.right * _accel, ForceMode.Acceleration);
        }



        //_______________________________________________________________________________________________________
        //||||||||||||||||||||||||||||||||||||||||||||CURVED||||||||||||||||||||||||||||||||||||||||||||||||||||\\

        public void GetFrontVectorAngle(Transform _playerBody, Transform _Target)
        {
            float   _angle  = Vector3.Angle(_playerBody.forward, _Target.forward);
            Vector3 _cross  = Vector3.Cross(_playerBody.forward, _Target.forward);

            if (_cross.y < 0)
            {
                _angle = -_angle;
            }

            RotationForce = _angle;
        }
        



        //_______________________________________________________________________________________________________
        //||||||||||||||||||||||||||||||||||||||||||||-------||||||||||||||||||||||||||||||||||||||||||||||||||||\\
        #endregion




        #region BodyPositioningRelatedFunctions
        //public void BodyDriftTurn(Rigidbody rb, GameObject bodypart)
        //{
        //    rb.AddForce(-bodypart.transform.right * currentSpeed, ForceMode.Acceleration);
        //}
        //public void FollowSphereCollider(Transform targetPos, Transform spherePos, Vector3 offSetPos)
        //{
        //    targetPos.position = spherePos.transform.position - offSetPos;
        //}
        #endregion




        #region CustomGravityRelatedFunctions
        public void CheckIfIsGrounded(float _distance, float hight)
        {

            if (_distance <= hight)
            {
                currentVerticalStatus = verticality.Grounded;

            }
            else
            {
                currentVerticalStatus = verticality.Suspended;
            }
        }
        public float GetAltitude(Vector3 origin, Vector3 upDirection)
        {
            RaycastHit hit;
            Ray _ray = new Ray(origin, upDirection);

            float _autitude;

            if (Physics.Raycast(_ray, out hit, groundLayer))
            {
                _autitude = hit.distance;
                return _autitude;
            }
            else
                return 20f;
        }
      
        public void CustomGravity(Rigidbody _sphereRb, Transform _downwardDir, float _groundedAccel, float _suspendedAccel)
        {
            switch (currentVerticalStatus)
            {
                case verticality.Grounded:
                    _sphereRb.AddForce((-_downwardDir.up) * _groundedAccel);
                    //_sphereRb.drag = 2f;
                    break;


                case verticality.Suspended:
                    _sphereRb.AddForce((-_downwardDir.up) * _suspendedAccel);
                    //_sphereRb.drag = .3f;

                    break;
            }
        }
        public float GetDownwardForce(AnimationCurve _curveValue, float _altitudeValue, int _multiplier)
        {
            float downwardMov = _curveValue.Evaluate(_altitudeValue) * _multiplier;

            return downwardMov;
        }




        #endregion




        private void ActivateSpeedCalculation()
        {
            if (isCalculatingSpeed && pcData.gameIsRunning)
            {   return;   }

            else if (isCalculatingSpeed && !pcData.gameIsRunning)
            {   isCalculatingSpeed = false;   }

            else if (!isCalculatingSpeed && pcData.gameIsRunning)
            {   isCalculatingSpeed = true;
                StartCoroutine(ICalculateSpeed());  }
        }

        private IEnumerator ICalculateSpeed()
        {
            while (pcData.pcCtrl.isPlaying)
            {
                ForwardLateralVertical = new Vector3(
                   (float)Math.Round(pivotPos.InverseTransformDirection(sphereMov.velocity).x, 3),
                   (float)Math.Round(pivotPos.InverseTransformDirection(sphereMov.velocity).y, 3),
                   (float)Math.Round(pivotPos.InverseTransformDirection(sphereMov.velocity).z, 3) );

                yield return null;
            }
        }

    }
}
