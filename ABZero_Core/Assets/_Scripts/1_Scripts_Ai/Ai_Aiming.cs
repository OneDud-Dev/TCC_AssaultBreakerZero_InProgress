using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABZ_Ai
{
    public class Ai_Aiming : MonoBehaviour
    {
        [Header("References")]

        public Ai_References data;




        public void PointUpperBodyForward(Transform _bodyPart)
        {
            _bodyPart.rotation = Quaternion.LookRotation(data.lookTarget.position);
        }



        public void PointUpperBodyToTarget(Vector3 _target, Transform _bodyPart)
        {
            Vector3 lookdir = new Vector3(_target.x - _bodyPart.position.x,
                                       0,
                                       _target.z - _bodyPart.position.z);

            _bodyPart.rotation = Quaternion.LookRotation(lookdir);
        }



        public void MoveTargetToPredictedPosition()
        {

        }
    }
}
