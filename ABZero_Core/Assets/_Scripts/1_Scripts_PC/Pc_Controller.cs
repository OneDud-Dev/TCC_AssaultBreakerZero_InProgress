using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ABZ_Pc
{
    public class Pc_Controller : MonoBehaviour
    {

        #region Variables

        [Header("Data Reference")]
        public Pc_References pcData;
        public Game_Events OnPauseGame;

        public Game_Controller GameCtrl;
        public GameObject pauseCanvas;

        [Header("Ref Set")]
        public Pc_VAMT_SObj vamtSetting;
        public bool         playerIsActive;

        [Header("Gameplay Set")]
        #region Player settings
        public int  hp;
        public bool isPlaying;
        public bool canBoost;
        public bool canRun;
        public bool canMove;
        public bool canAttack;

        #endregion

        //[Header("Data Reference")]    all those are privated
        #region Scripts refs
        private Pc_Aiming       pcAim;
        private Pc_CameraCtrl   pcCam;
        private Pc_Movement     pcMov;
        private Pc_Shooting     pcShoot;
        private Pc_AutoTarget   pcAutoTgt;

        #endregion

        //[Header("Body Reference")]
        #region GameObj Refs
        private Transform playerBody;
        private Rigidbody sphereMov;
        private Transform camPivot;

        #endregion

        //[Header("Input Reference")]
        #region ÍnputActions
        private InputAction moveAction;
        private InputAction accelAction;
        private InputAction boostAction;
        private InputAction shootActionPrimary;
        private InputAction shootActionSecondary;
        private InputAction shootActionSpecialS;
        private InputAction pauseAction;

        #endregion

        
        [SerializeField] private Vector2 playerMovinput;
        #endregion


        private void Awake()
        {
            //GameCtrl = GameObject.FindGameObjectWithTag("GameCtrl").GetComponent<Game_Controller>();
        }

        #region Unity
        private void Start()
        {

            //get script references
            pcAim     = pcData.pcAim;
            pcCam     = pcData.pcCam;
            pcMov     = pcData.pcMov;
            pcShoot   = pcData.pcShoot;
            pcAutoTgt = pcData.pcAutoTarget;

            //get gameobject references
            playerBody  = pcData.playerBodyRoot;
            sphereMov   = pcData.sphereMov;
            camPivot    = pcData.mainPivot;

            //get input references
            moveAction           = pcData.pcInput.actions["CharMovement"];
            accelAction          = pcData.pcInput.actions["AccelAction"];
            boostAction          = pcData.pcInput.actions["BoostAction"];
            shootActionPrimary   = pcData.pcInput.actions["PrimaryArm"];
            shootActionSecondary = pcData.pcInput.actions["SecondaryArm"];
            shootActionSpecialS  = pcData.pcInput.actions["SpecialArm"];
            pauseAction          = pcData.pcInput.actions["PauseAction"];
            //seting inicial data
            pcMov.currentVerticalStatus = Pc_Movement.verticality.Grounded;

            
        }

        private void Update()
        {
            //remember, value from ScriptableObject are being used
            //chama os metodos dasa classes necessarias para aplicar a função

            switch (pcData.gameIsRunning)
            {
                case false:
                    break;
                case true:
                    if (playerIsActive)
                    {
                        ForwardVectorMovInput();
                        HorizontalVectorMovInput();
                    }
                    break;
            }
        }

        #endregion




        #region input Functions

        //----------------------------Actions
        #region attack Actions
        public void P_LeftArmAction(InputAction.CallbackContext _leftButton)
        {
            if (!pcData.leftOverride)
            {
                if (_leftButton.performed) { pcShoot.HoldToUseLeftArm(); }
            }
        }

        public void P_RightArmAction(InputAction.CallbackContext _rightButton)
        {
            if (!pcData.rightOverride)
            {
                if (_rightButton.performed) { pcShoot.HoldToUseRightArm(); }
            }
        }

        public void P_SpecialArmAction(InputAction.CallbackContext _middleButton)
        {
            if (!pcData.specialOverride)
            {
                if (_middleButton.performed) { pcShoot.HoldToUseSpecial(); }
            }
        }

        public void P_PauseAction()
        {
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
            GameCtrl.YesCursor();
        }

        public void P_UNPause()
        {
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
            GameCtrl.NoCursor();
        }

        public void P_BackToMenu()
        {
            SceneManager.LoadScene("01_StartScreen");
        }

        #endregion

        //----------------------------movement

        public void GetMovementInput()
        {
                playerMovinput = moveAction.ReadValue<Vector2>();
        }

        public void ActivateRunning(InputAction.CallbackContext _accelButton)
        {
            switch (canRun)
            {
                case false: break;
                case true:
                    if (_accelButton.performed) { pcMov.isRunning = true; pcMov.SetMovementType(); }
                    else if (_accelButton.canceled) { pcMov.isRunning = false; pcMov.SetMovementType(); }
                    break; 
            }
        }

        public void ActivateBoost(InputAction.CallbackContext _boostButton)
        {
            switch (canBoost)
            {
                case false: break;
                case true:
                    if      (_boostButton.performed) { pcMov.isBoosting = true; pcMov.SetMovementType(); }
                    else if (_boostButton.canceled) { pcMov.isBoosting = false; pcMov.SetMovementType(); }
                    break;
            }
        }

        public void ForwardVectorMovInput()
        {
            switch (canMove)
            {
                case false: break;
                case true:
                    switch (pcMov.currentMovement)
                    {
                        case Pc_Movement.MovementType.Walking:

                            if (playerMovinput.y > 0.1f)
                                                            { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.forwardWalk, 2); }
                            else if (playerMovinput.y < (-0.1f))
                                                            { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardWalk, 2); }
                            else
                                                            { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardWalk, 0); }

                            break;

                        case Pc_Movement.MovementType.Running:
                    if (playerMovinput.y > 0.1f)
                                                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.forwardRun, 2); }
                    else if (playerMovinput.y < (-0.1f))
                                                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardRun, 2); }
                    else
                                                    { pcMov.AccelInputValue(0, 0, 0); }
                    break;

                        case Pc_Movement.MovementType.Boosting:
                    
                    if (playerMovinput.y > 0.1f)
                                                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.forwardBoost, 2); }
                    else if (playerMovinput.y < (-0.1f))
                                                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardBoost, 2); }
                    else
                                                    { pcMov.AccelInputValue(0, 0, 0); }
                    break;
                    }
                break;
            }
        }

        public void HorizontalVectorMovInput()
        {
            switch (canMove)
            {
                case false: break;
                case true:
                    switch (pcMov.currentMovement)
                    {
                        case Pc_Movement.MovementType.Walking:
                            if (playerMovinput.x > 0.1f)
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeWalk, 2); }
                            else if (playerMovinput.x < (-0.1f))
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeWalk, 2); }
                            else
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeWalk, 0); }
                            break;

                        case Pc_Movement.MovementType.Running:
                            if (playerMovinput.x > 0.1f)
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeRun, 2); }
                            else if (playerMovinput.x < (-0.1f))
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeRun, 2); }
                            else
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeRun, 0); }
                            break;

                        case Pc_Movement.MovementType.Boosting:
                            if (playerMovinput.x > 0.1f)
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeBoost, 2); }
                            else if (playerMovinput.x < (-0.1f))
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeBoost, 2); }
                            else
                            { pcMov.StrafeInputValue(playerMovinput.x, vamtSetting.strafeBoost, 0); }
                            break;
                    }
                    break;
            }
        }


        //------------------------------UI

        public void pauseGame(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                pcData.gameIsRunning = false;
                OnPauseGame.Raise();
            }
        }


        #endregion
    }
}
