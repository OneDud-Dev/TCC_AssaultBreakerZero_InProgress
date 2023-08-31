using ABZ_Pc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ABZ_Ai
{
    public class Ai_References : MonoBehaviour
    {
        public int  hP;
        public bool isDead;
        public Pc_References pcData;


        //===========================================
        [Header("--------Character details-------")]
        public string Model;
        public int          pointValue;
        public string       pilotName;
        public string       squadronName;
        public Animator     aiAnimator;
        public GameObject   AiReference;
        public int          speedAttack;
        public int          speedPatrolling;
        public int          speedTraveling;

        #region Scripts
        [Header("--------scripts-------------")]
        public Ai_Controller  aiCtrl;
        public Ai_Aiming      aiAim;
        public Ai_Combat      aiCombat;
        public Ai_Movement    aiMov;
        public NavMeshAgent   aiAgent;
        public Ai_Health      aiHealth;
        public Ai_Sight       aiSight;
        #endregion



        #region GameObjects
        [Header("--------game objects----------------")]
        public GameObject   aiRoot;
        public Transform    aiPos;
        public Transform    aiPivot;
        public Transform    bodyPos;
        //public Transform    upperBody;
        //public Transform    lowerBody;
        public Transform    upperBodyPivot;
        public SphereCollider sightTrigger;

        public Transform    moveTarget;
        public Transform    lookTarget;
        public Transform    bulletSpawner;
        #endregion

        #region AiEventsListeners
        

        #endregion
    }
}
