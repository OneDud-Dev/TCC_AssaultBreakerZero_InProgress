using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_Movement : MonoBehaviour
    {

        public Pc_References pcData;

        #region Variables

        public Vector3 bodyOffset;
        public Vector3 pivotOffset;
        public bool debuggerGismos;
        public bool isBoosting;
        #region References

        [Header("Object References")]
        private Transform playerBody;
        private Transform lowerBody;
        private Rigidbody sphereMov;

        private Transform pivotPos;

        #endregion

        #region Enums
        public enum MovementType { Walking, Bursting }
        public enum verticality { Grounded, Suspended }
        #endregion

        #region MovementData

        public MovementType currentMovement;
        public verticality currentVerticalStatus;
        public LayerMask groundLayer;
        [SerializeField] private float groundCheckLength;

        [Header("Movement forces")]
        [SerializeField] private float forwardForce;
        [SerializeField] private float horizontalForce;
        [SerializeField] private float downwardForce;
        [SerializeField] private float RotationForce;


        [Header("Movement Value")]
        public bool isCalculatingSpeed;
        public float currentSpeed;
        public float currentVerticalSpeed;

        public float currentAltitudeValue;
        public float currentAngleValue;
        #endregion

        #endregion



        //_______________________________________UNITY_______________________________________

        #region Unity Setup
        private void Start()
        {
            pcData.pcCtrl.isPlaying = true;
            isCalculatingSpeed = true;

            sphereMov = pcData.sphereMov;
            playerBody = pcData.playerBody;
            pivotPos = pcData.camPivot;

            StartCoroutine(ICalculateSpeed());
        }

        #endregion





        #region Unity Calculations
        private void Update()
        {
            FollowSphereCollider(playerBody, sphereMov.transform, bodyOffset);
            FollowSphereCollider(pivotPos, sphereMov.transform, pivotOffset);

            CheckIfIsGrounded(currentAltitudeValue, groundCheckLength);
            ActivateSpeedCalculation();
        }


        private void FixedUpdate()
        {
            currentAltitudeValue = GetAltitude(sphereMov.position, -pcData.camPivot.up);

            //CustomGravity(sphereMov, pcData.camPivot, 10, 30);


            //appliyng values to rigidybody
            switch (currentVerticalStatus)
            {
                case Pc_Movement.verticality.Grounded:
                    switch (currentMovement)
                    {
                        case Pc_Movement.MovementType.Walking:
                            ForwardMovement(sphereMov, playerBody, forwardForce);
                            StrafeMovement(sphereMov, playerBody, horizontalForce);
                            BodyRotation(playerBody, pcData.camPivot, RotationForce);
                            isBoosting = false;
                            break;



                        case Pc_Movement.MovementType.Bursting:
                            ForwardMovement(sphereMov, playerBody, forwardForce);
                            StrafeMovement(sphereMov, playerBody, horizontalForce);
                            BodyRotation(playerBody, pcData.camPivot, RotationForce);
                            isBoosting = true;
                            break;
                        default:
                            break;
                    }
                    break;

                //------------------------------------------------------------------------------------------------
                case Pc_Movement.verticality.Suspended:
                    switch (currentMovement)
                    {
                        case Pc_Movement.MovementType.Walking:
                            ForwardMovement(sphereMov, playerBody, forwardForce);
                            StrafeMovement(sphereMov, playerBody, horizontalForce);
                            BodyRotation(playerBody, pcData.camPivot, RotationForce);
                            isBoosting = false;
                            break;



                        case Pc_Movement.MovementType.Bursting:
                            ForwardMovement(sphereMov, playerBody, forwardForce);
                            StrafeMovement(sphereMov, playerBody, horizontalForce);
                            BodyRotation(playerBody, pcData.camPivot, RotationForce);
                            isBoosting = true;
                            break;

                        default:
                            break;
                    }

                    //------------------------------------------------------------------------------------------------
                    break;
                default:

                    break;
            }



        }


        private void LateUpdate()
        {
        }

        #endregion




        //_______________________________________METHODS_______________________________________
        //________Methods that require player input values are applied at pc Controller________
        //_____________________________________________________________________________________


        #region MovementRelatedFunctions
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
            float _angle = Vector3.Angle(_playerBody.forward, _Target.forward);
            Vector3 _cross = Vector3.Cross(_playerBody.forward, _Target.forward);

            if (_cross.y < 0)
            {
                _angle = -_angle;
            }

            RotationForce = _angle;
        }
        public void BodyRotation(Transform _Body, Transform _target, float _rotationSpeed)
        {
            _Body.rotation = Quaternion.RotateTowards(_Body.rotation, _target.rotation, _rotationSpeed);
        }



        //_______________________________________________________________________________________________________
        //||||||||||||||||||||||||||||||||||||||||||||-------||||||||||||||||||||||||||||||||||||||||||||||||||||\\
        #endregion




        #region BodyPositioningRelatedFunctions
        public void BodyDriftTurn(Rigidbody rb, GameObject bodypart)
        {
            rb.AddForce(-bodypart.transform.right * currentSpeed, ForceMode.Acceleration);
        }
        public void FollowSphereCollider(Transform targetPos, Transform spherePos, Vector3 offSetPos)
        {
            targetPos.position = spherePos.transform.position - offSetPos;
        }
        #endregion




        #region CustomGravityRelatedFunctions
        public void CustomGravity(Rigidbody _sphereRb, Transform _downwardDir, float _groundedAccel, float _suspendedAccel)
        {
            switch (currentVerticalStatus)
            {
                case verticality.Grounded:
                    _sphereRb.AddForce((-_downwardDir.up) * _groundedAccel, ForceMode.Acceleration);
                    _sphereRb.drag = 1f;
                    break;


                case verticality.Suspended:
                    _sphereRb.AddForce((-_downwardDir.up) * _suspendedAccel, ForceMode.Acceleration);
                    _sphereRb.drag = .8f;

                    break;
                default:
                    break;
            }
        }


        public float DownwardValue(AnimationCurve _curveValue, float _altitudeValue, int _multiplier)
        {
            float downwardMov = _curveValue.Evaluate(_altitudeValue) * _multiplier;

            return downwardMov;
        }



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
                if (debuggerGismos)
                {
                    Debug.DrawLine(sphereMov.position, hit.point, Color.yellow);
                }

                _autitude = hit.distance;
                return _autitude;
            }
            else
                return 20f;

        }

        #endregion




        private void ActivateSpeedCalculation()
        {
            if (isCalculatingSpeed && pcData.pcCtrl.isPlaying)
            {
                return;
            }

            else if (isCalculatingSpeed && !pcData.pcCtrl.isPlaying)
            {
                isCalculatingSpeed = false;
            }

            else if (!isCalculatingSpeed && pcData.pcCtrl.isPlaying)
            {
                isCalculatingSpeed = true;
                StartCoroutine(ICalculateSpeed());
            }
        }

        private IEnumerator ICalculateSpeed()
        {


            while (pcData.pcCtrl.isPlaying)
            {
                Vector3 prevPos = pivotPos.position;

                yield return new WaitForFixedUpdate();

                currentSpeed = (Vector3.Distance(pivotPos.position, prevPos) / Time.fixedDeltaTime);
            }
        }

    }
}
