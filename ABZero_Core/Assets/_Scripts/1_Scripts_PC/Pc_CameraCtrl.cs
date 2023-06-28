using ABZ_GameSystems;
using ABZ_Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ABZ_Pc
{
    public class Pc_CameraCtrl : MonoBehaviour
    {
        #region Variables

        public  Pc_References pcData;
        public bool isCameraMoving;

        private Pc_VAMT_SObj  playerConfig;
        private InputAction   mouseAction;
        private Transform camPivot;
        private Transform camTarget;
        private Transform mainPivot;
        private Transform sphereMovPos;
        public float turnVector;
        public float CamFollowDampingSpeed; //-------move to vamtsettings later
        private Vector3 camFollowDamping = Vector3.zero;


        #endregion




        #region Unity
        private void Start()
        {
            mouseAction  = pcData.pcInput.actions["CamMovement"];
            
            playerConfig = pcData.vamtSettings;
            camPivot     = pcData.mainCamFollow;
            camTarget    = pcData.mainCamTarget;
            mainPivot    = pcData.mainPivot;
            sphereMovPos = pcData.sphereMov.transform;
            //screenSize = Screen.width;
        }

        private void Update()
        {
            switch (pcData.gameIsRunning)
            {
                case false: break;
                case true:
                    if (isCameraMoving)      {CameraPivotRotate(camPivot, turnVector); }
                    break;
            }
        }
        #endregion




        #region Methods
        //unity event
        public void GetMouseHorizontalDeltaValue() => 
            turnVector += (float)mouseAction.ReadValue<Vector2>().x * playerConfig.camPivotSensitivity;


        //transform methods
        public void CameraPivotRotate(Transform cam, float turn) =>
            cam.localRotation = Quaternion.Euler(0, turn, 0);


        public void SetObjPosDamped(Transform obj, Transform target, Vector3 dampingVelocity, float Speed)
        {
            obj.position = Vector3.SmoothDamp(obj.position, target.position,
                                ref dampingVelocity, Speed * Time.deltaTime);
        }
        public void SetObjectPosition(Transform obj, Transform target)
        {
            obj.position = target.position;
        }

        //UI Methods
        public void     LockCursor() => Cursor.lockState = CursorLockMode.Locked;

        public void     UnlockCursor() => Cursor.lockState = CursorLockMode.None;


        #endregion
        

    }
}
