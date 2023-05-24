using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ABZ_Ai
{
    public class Ai_References : MonoBehaviour
    {
        public int hP;
        public bool isDead;


        //===========================================



        #region Scripts
        [Header("--------scripts----------------")]
        public Ai_Controller  aiCtrl;
        public Ai_Aiming      aiAim;
        public Ai_Combat      aiCombat;
        public Ai_Movement    aiMov;
        public NavMeshAgent   aiAgent;
        #endregion



        #region GameObjects
        [Header("--------game objects----------------")]
        public GameObject   aiRoot;
        public Transform    aiPos;

        public Transform    bodyPos;
        public Transform    upperBody;
        public Transform    lowerBody;
        public Transform    bodyPivot;
        public SphereCollider sightTrigger;

        public Transform    moveTarget;
        public Transform    lookTarget;
        public Transform    bulletSpawner;
        #endregion
    }
}
