using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABZ_Weapons;
using Cinemachine;

namespace ABZ_Pc
{
    public class Pc_References : MonoBehaviour
    {
        //verificar se usar this pra injetar referencia está funcionando

        [Header("input")]
        public PlayerInput  pcInput;
        public Pc_VAMT_SObj vamtSettings;
        public Animator     pcAnimator;


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
        public 
           CinemachineVirtualCamera virCam;
        public Camera               mainCam;
        public Transform            camPos;
        public Transform            camFollow;
        public Transform            camTarget;



        [Header("Object References")]
        public Rigidbody sphereMov;
        public Transform playerBodyRoot;
        public Transform upperBody;
        public Transform lowerBody;
        public Transform mainPivot;
        public Transform tankControlForward; // make possible to use diferent references to 'forward'
        public Transform normalControlForward;
        //public Transform rightArmSlot;
        //public Transform leftArmSlot;


        [Header("Rig References")]
        public Transform torsoRig;
        public Transform torsoTarget;
        
        public Transform leftArmRig;
        public Transform leftArmTarget;
        public Transform rightArmRig;
        public Transform rightArmTarget;
        
        public Transform specialArmPivotRig;
        public Transform specialArmPivotTarget;
        //procedure leg animation
        public Transform rightLegTarget;
        public Transform leftLegTarget;


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
        public W_M_Shield   Shield;
        public W_M_Sword    normalSword;
        public W_M_Axe      normalAxe;
        [Header("")]
        public List<GameObject> enemyList;
    }
}
