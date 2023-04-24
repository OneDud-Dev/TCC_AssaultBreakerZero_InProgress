using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ABZ_Pc
{
    public class Pc_Controller : MonoBehaviour
    {

        #region Variables

        [Header("Data Reference")]
        public Pc_References pcData;
        
        [Header("Ref Set")]
        public Pc_VAMT_SObj vamtSetting;
        public bool playerIsActive;

        [Header("Gameplay Set")]
        public int  hp;
        public bool canAccel;
        public bool canAttack;
        public bool isPlaying;

        //[Header("Data Reference")]
        private Pc_Aiming       pcAim;
        private Pc_CameraCtrl   pcCam;
        private Pc_Movement     pcMov;
        private Pc_Shooting     pcShoot;
        private Pc_AutoTarget   pcAutoTgt;

        //[Header("Body Reference")]
        private Transform playerBody;
        private Rigidbody sphereMov;
        private Transform camPivot;

        //[Header("Input Reference")]
        private InputAction moveAction;
        private InputAction accelAction;
        private InputAction shootActionPrimary;
        private InputAction shootActionSecondary;
        private InputAction shootActionSpecialS;
        private InputAction pauseAction;

        //
        [SerializeField] private Vector2 playerMovinput;
        #endregion




        #region Unity Setup
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
            shootActionPrimary   = pcData.pcInput.actions["PrimaryArm"];
            shootActionSecondary = pcData.pcInput.actions["SecondaryArm"];
            shootActionSpecialS  = pcData.pcInput.actions["SpecialArm"];
            pauseAction          = pcData.pcInput.actions["PauseAction"];
            //seting inicial data
            pcMov.currentVerticalStatus = Pc_Movement.verticality.Grounded;
        }

        #endregion


        private void Update()
        {
            //remember, value from ScriptableObject are being used
            //chama os metodos dasa classes necessarias para aplicar a função
            
            if (playerIsActive)
            {
            //playerMovinput = moveAction.ReadValue<Vector2>(); 
            //trying to use unity events> using getmovinput on PlayerInput component
            //Unity Event sucessfull

            ForwardVectorMovInput();
            HorizontalVectorMovInput();
            ActivateBoost(accelAction);

            }



            #region Debug gismos
            if (pcMov.MovGismos)
            {
                Debug.DrawLine(sphereMov.position, sphereMov.velocity, Color.black);
            }


            #endregion
        }



        //TODO: send events to UI => health, points, 





        #region input Functions

        //----------------------------Actions
        public void PrimaryFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                pcShoot.HoldToFirePrimary();
            }
        }

        public void SpecialFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                pcShoot.HoldToFireSpecial();
            }
        }

        //----------------------------movement

        public void GetMovementInput() => playerMovinput = moveAction.ReadValue<Vector2>();

        public void ActivateBoost(InputAction _accelButton)
        {
            if (!_accelButton.IsPressed())
            {
                pcMov.currentMovement = Pc_Movement.MovementType.Walking;
            }
            else
            {
                pcMov.currentMovement = Pc_Movement.MovementType.Bursting;
            }
        }

        public void ForwardVectorMovInput()
        {
            switch (pcMov.currentMovement)
            {
                case Pc_Movement.MovementType.Walking:

                    if (playerMovinput.y > 0.1f)
                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.forwardWalkPower, 3); }

                    else if (playerMovinput.y < (-0.1f))
                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardWalkPower, 3); }

                    else
                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardWalkPower, 0); }

                    break;


                case Pc_Movement.MovementType.Bursting:
                    if (playerMovinput.y > 0.1f)
                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.forwardBurstPower, 3); }

                    else if (playerMovinput.y < (-0.1f))
                    { pcMov.AccelInputValue(playerMovinput.y, vamtSetting.backwardWalkPower, 3); }

                    else
                    { pcMov.AccelInputValue(0, 0, 0); }
                    break;



                default:
                    break;
            }
        }

        public void HorizontalVectorMovInput()
        {
            switch (pcMov.currentMovement)
            {
                case Pc_Movement.MovementType.Walking:
                    pcMov.StrafeInputValue( playerMovinput.x,
                                            vamtSetting.horizontalStrafePower,
                                            3);
                    break;


                case Pc_Movement.MovementType.Bursting:
                    pcMov.StrafeInputValue( playerMovinput.x,
                                            vamtSetting.horizontalBurstPower,
                                            3);
                    break;


                default:
                    break;
            }
        }


        //------------------------------UI

        public void pauseGame(InputAction.CallbackContext context)
        {
            if (context.performed)
            {

            }
        }
        #endregion
    }
}
