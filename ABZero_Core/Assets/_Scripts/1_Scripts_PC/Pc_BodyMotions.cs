using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_BodyMotions : MonoBehaviour
    {
        // BodyRoot deve seguir ShphereMovement com uma pequena força de mola
        // lowerbody deve rotacionar devagar em direção ao FOWARD do corpo ou
        //      usar um angulo treshold maximo que faz começar a rotacionar em direção a camera 
        // upperbody deve rotacionar um pouco mais rápido em direção a frente da camera
        // armRigs devem rotacionar rapidamente para frente da camera até certo angulo, parar se passar


        #region Variables
        public Pc_References pcData;
        public bool playerMotionGismos;

        [Header("Offsets")]
        public Vector3 bodyOffset;
        public Vector3 pivotOffset;

        [Header("Values")]
        private Vector3 legDirection;
        private Vector3 dampingCameraVector = Vector3.zero;


        #endregion

        #region Objects References
        private Transform sphereMov;
        private Transform cameraFollow;
        private Transform mainPivot;

        private Transform bodyRootPos;
        private Transform upperBody;
        private Transform lowerBody;

        private Transform rightArmRig;
        private Transform rightArmTarget;
        private Transform leftArmRig;
        private Transform leftArmTarget;

        private Transform rightLegTarget;
        private Transform leftLegTarget;

        #endregion




        private void Start()
        {
            
            //others
            sphereMov       = pcData.sphereMov.transform;
            cameraFollow    = pcData.camFollow;
            mainPivot       = pcData.mainPivot; //read only
            //body
            bodyRootPos     = pcData.playerBodyRoot;
            upperBody       = pcData.upperBody;
            lowerBody       = pcData.lowerBody;
            //rigs
            rightArmRig     = pcData.rightArmRig;
            rightArmTarget  = pcData.rightArmTarget;
            leftArmRig      = pcData.leftArmRig;
            leftArmTarget   = pcData.leftArmTarget;
            rightLegTarget  = pcData.rightLegTarget;
            leftLegTarget   = pcData.leftLegTarget;


        }


        private void Update()
        {
            legDirection = new Vector3(pcData.sphereMov.velocity.x ,     0 ,
                                       pcData.sphereMov.velocity.z);

            SetObjectPosition(bodyRootPos, sphereMov);
            SetObjectPosition(mainPivot,   sphereMov);
            
            
            //SetObjectPosition(cameraFollow,sphereMov);

            //SetObjPosDamped(bodyRootPos, sphereMov, BodyDampingVel, 1);
            //SetObjPosDamped(mainPivot,   sphereMov, ref 1,1);=========================================================

            //RotateObjOverTime(lowerBody, legDirection); //needs smooth / damping

        }

        private void FixedUpdate()
        {
            SetObjPosDamped(cameraFollow, mainPivot, dampingCameraVector, 5f);
            
        }


        #region SetPosition 
        //---------------------------------Place obj on top of another-----------------------------------
        public void SetObjectPosition(Transform obj, Transform target)
        {
            obj.position = target.position;
        }

        public void SetObjectPosition(Transform obj, Transform target, Vector3 offset)
        {
            obj.position = target.position - offset;
        }

        public void SetObjPosDamped(Transform obj, Transform target, Vector3 dampingVelocity, float Speed)
        {
            obj.position = Vector3.SmoothDamp(  obj.position,
                                                target.position,
                                                ref dampingVelocity,
                                                Speed * Time.deltaTime);
        }

        #endregion




        #region ROTATION
        //--------------------------------------------Rotation--------------------------------------------

        public void SetRotationEqualToTarget(Transform obj, Transform target)
        {
            obj.rotation = target.rotation;
        }
        
        public void RotateObjOverTime(Transform bodyObj, Transform target, float rotationSpeed)
        {
            bodyObj.rotation = Quaternion.RotateTowards(bodyObj.rotation,
                                                        target.rotation,
                                                        rotationSpeed);
        }
        
        public void InstantPointsTowardsTarget(Transform bodyObj, Transform target)
        {
            Vector3 _lookDirection = new Vector3(target.position.x - upperBody.position.x,      0,
                                                 target.position.z - upperBody.position.z);

            bodyObj.rotation = Quaternion.LookRotation(_lookDirection);
        }

        #endregion




    }
}
