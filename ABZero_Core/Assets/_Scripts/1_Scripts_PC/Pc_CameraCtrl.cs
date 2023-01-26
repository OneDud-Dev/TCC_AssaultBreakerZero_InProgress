using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ABZ_Pc
{
    public class Pc_CameraCtrl : MonoBehaviour
    {
        #region Variables

        public Pc_References pcData;

        //[Header("Bodyparts Ref")]

        private Transform camPivot;
        private Transform camTarget;

        //[Header("input Ref")]
        private PlayerInput pcInputs;
        private InputAction mouseAction;

        //[Header("Screen")]
        //private float screenSize;

        [Header("Cam Movement Data")]
        private Vector2 turnVector;
        public float turnSensitivity;


        #endregion




        #region Unity
        private void Start()
        {
            camPivot = pcData.camPivot;
            camTarget = pcData.camTarget;
            pcInputs = pcData.pcInput;
            //screenSize = Screen.width;


            mouseAction = pcInputs.actions["CamMovement"];
        }

        private void Update()
        {
            turnVector.x += (GetMouseMovement(mouseAction).x * 0.05f) * turnSensitivity;
            turnVector.y += (GetMouseMovement(mouseAction).y * 0.05f) * turnSensitivity;

            CameraPivotRotate(camPivot, turnVector);
        }
        #endregion




        //Gameplay Methods
        public void PointObjAtTarget(Transform pivot, Transform target, Transform origin)
        {
            Vector3 lookDirection = new Vector3(target.position.x - origin.position.x,
                                        0,
                                        target.position.z - origin.position.z);

            pivot.rotation = Quaternion.LookRotation(lookDirection);
        }
        public Vector2 GetMouseMovement(InputAction input)
        {
            Vector2 value = input.ReadValue<Vector2>();

            return value;
        }
        public void CameraPivotRotate(Transform cam, Vector2 turn)
        {
            cam.localRotation = Quaternion.Euler(0, turn.x, 0);
        }


        //UI Methods
        public void LockCursor() => Cursor.lockState = CursorLockMode.Locked;

        public void UnlockCursor() => Cursor.lockState = CursorLockMode.None;

    }
}
