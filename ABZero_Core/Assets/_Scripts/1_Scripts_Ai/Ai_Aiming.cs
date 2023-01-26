using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Aiming : MonoBehaviour
    {
        [Header("References")]

        public Ai_References data;




        public void PointUpperBodyForward()
        {
            data.upperBody.rotation = Quaternion.LookRotation(data.lookTarget.position);
        }



        public void PointUpperBodyToTarget(Vector3 _target)
        {
            Vector3 lookdir = new Vector3(_target.x - data.upperBody.position.x,
                                       0,
                                       _target.z - data.upperBody.position.z);

            data.upperBody.rotation = Quaternion.LookRotation(lookdir);
        }



        public void MoveTargetToPredictedPosition()
        {

        }
    }
}
