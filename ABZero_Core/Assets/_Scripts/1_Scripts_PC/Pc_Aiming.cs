using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_Aiming : MonoBehaviour
    {
        // Aim script job is to point weapon to a target,
        

        #region Variables

        public  Pc_References   pcData;
        public  bool            aimingGismos;

      
        private Transform       topBody;
        private Vector3         lookDirection;

        #endregion



        #region Targeting Functions
       

        public void SetTargetToEnemyPos(Transform aimTarget, Collider other)
        {
            aimTarget.position = other.transform.position;
        }



        #endregion
    }
}
