using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABZ_Weapons;

namespace ABZ_Pc
{
    public class Pc_References : MonoBehaviour
    {
        //verificar se usar this pra injetar referencia está funcionando

        [Header("input")]
        public PlayerInput pcInput;
        public Animator pcAnimator;


        [Header("Script References")]
        public Pc_Controller pcCtrl;
        public Pc_Movement pcMov;
        public Pc_Shooting pcShoot;
        public Pc_Aiming pcAim;
        public Pc_CameraCtrl pcCam;
        public Pc_Animation pcAnimeScript;
        public Pc_AutoTarget pcTarget;


        [Header("Camera Ref erences")]
        public Camera mainCam;
        public Transform camPos;
        public Transform camPivot;
        public Transform camTarget;



        [Header("Object References")]
        public Transform playerBody;
        public Transform rightArmSlot;
        public Transform leftArmSlot;
        //public Transform upperBody;
        //public Transform lowerBody;
        public Rigidbody sphereMov;
        public Transform tankControlForward; // make possible to use diferent references to 'forward'
        public Transform normalControlForward;


        [Header("Rig References")]
        public Transform torsoTarget;
        public Transform torsoRig;
        public Transform leftArmAim;
        public Transform leftArmRig;
        public Transform rightArmAim;
        public Transform rightArmRig;
        public Transform specialArmRig;
        public Transform specialArmAim1;
        public Transform specialArmAim2;


        [Header("Combat data")]
        public W_N_AutoRifle gunAR;
        public W_N_Gauss gunGaus;
        public W_N_Laser gunLaser;
        public W_N_Empty noWeapon;
        [Header("")]
        public W_S_MissileLcher gunMissile;
        public W_S_Cannon gunCannon;
        public W_S_Motar gunMortar;
        public W_S_Mines gunMine;
        public W_S_Empty noSpecial;
        [Header("")]
        public W_M_Shield Shield;
        public W_M_Sword normalSword;
        public W_M_Axe normalAxe;
        [Header("")]
        public List<GameObject> enemyList;
    }
}
