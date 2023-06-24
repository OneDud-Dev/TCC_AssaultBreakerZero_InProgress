using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Animations.Rigging;

using ABZ_Weapons;
using ABZ_GameSystems;

namespace ABZ_Pc
{
    public class Pc_References : MonoBehaviour
    {

        private void Start()
        {
            broadcasCamera.Raise(mainCamPos, 1);
        }

        //verificar se usar this pra injetar referencia está funcionando
        [Header("Game Status")]

        public bool gameIsRunning   = true;
        public bool leftOverride    = false;
        public bool rightOverride   = false;
        public bool specialOverride = false;

        [Header("input")]
        public PlayerInput  pcInput;
        public Pc_VAMT_SObj vamtSettings;
        public Animator     pcAnimator;

        [Header("event References")]
        public Game_Events broadcasCamera;

        [Header("Script References")]
        public Pc_Controller    pcCtrl;
        public Pc_BodyMotions   pcBodyMotion;
        public Pc_Movement      pcMov;
        public Pc_Animation     pcAnimeScript;
        public Pc_Shooting      pcShoot;
        public Pc_AutoTarget    pcAutoTarget;
        public Pc_Aiming        pcAim;
        public Pc_CameraCtrl    pcCam;
        

        [Header("Camera References")]
        public CinemachineVirtualCamera virCam;
        public Camera       mainCam;
        public Camera       targetCam;
        public Camera       MinimapCam;
        public Transform    mainCamPos;
        public Transform    mainCamFollow;
        public Transform    mainCamTarget;



        [Header("Object References")]
        public Rigidbody sphereMov;
        public Transform playerBodyRoot;
        public Transform mainPivot;
        public Transform tankControlForward; // make possible to use diferent references to 'forward'
        public Transform normalControlForward;
        //public Transform rightArmSlot;
        //public Transform leftArmSlot;

        
        [Header("Rig References")]
        public Transform                torsoPivot;
        public TwoBoneIKConstraint      leftArmCtrl;
        public TwoBoneIKConstraint      rightArmCtrl;
        public Transform specialArmPivot;
        public MultiRotationConstraint  specialArmRotationCtrl;

        public Transform spawn_Rifle_Left;
        public Transform spawn_S_M_Left;
        public Transform spawn_S_M_Right;
        public Transform spawn_S_Cannon;



        [Header("Combat data")]
        public W_N_AutoRifle gunAR;
        public W_N_Gauss    gunGaus;
        public W_N_Laser    gunLaser;
        public W_N_Empty    noWeapon;
        public W_S_MissileLcher gunMissile;
        public W_S_Cannon   gunCannon;
        public W_S_Motar    gunMortar;
        public W_S_Mines    gunMine;
        public W_S_Empty    noSpecial;
        public W_M_Shield   shield;
        public W_M_Sword    sword;
        public W_M_Axe      axe;
        [Header("")]
        public List<GameObject> enemyList;
        
        

        
        




    }
}
