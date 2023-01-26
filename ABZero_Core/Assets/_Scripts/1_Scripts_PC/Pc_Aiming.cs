using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_Aiming : MonoBehaviour
    {
        // Aim script job is to point weapon to a target,
        //
        //done: point body at a target
        //todo: get list of all enemies in the level?
        // filter for closest, mark with empty holo target
        // if current target, mark with main holo target
        // if next target too distant, mark with holo arrow pointer
        //
        //      set closer to an GameObject automaticCurrentTarget
        //
        //


        #region Variables

        public Pc_References pcData;
        public bool aimingGismos;

        //[Header("BodyParts Ref")]
        private Transform topBody;
        private Transform torsoRig;
        private Transform torsoTarget;

        private Transform leftArmTarget;
        private Transform leftArmRig;
        private Transform rightArmTarget;
        private Transform rightArmRig;

        //[Header("Target")]
        private Vector3 lookDirection;

        #endregion




        #region Unity
        private void Start()
        {
            //topBody =       pcData.upperBody; not in ref
            torsoRig = pcData.torsoRig;
            leftArmRig = pcData.leftArmRig;
            rightArmRig = pcData.rightArmRig;
            torsoTarget = pcData.torsoTarget;
            leftArmTarget = pcData.leftArmAim;
            rightArmTarget = pcData.rightArmAim;


        }


        private void FixedUpdate()
        {
            //PointBodyAtTarget(torsoRig, torsoTarget);
            SetPosition(torsoRig, torsoTarget);
        }
        #endregion




        #region Targeting Functions
        public void PointBodyAtTarget(Transform bodyPart, Transform target)
        {
            lookDirection = new Vector3(target.position.x - topBody.position.x,
                                        0,
                                        target.position.z - topBody.position.z);

            bodyPart.rotation = Quaternion.LookRotation(lookDirection);
        }


        public void SetTargetToEnemy(Transform aimTarget, Collider other)
        {
            aimTarget.position = other.transform.position;
        }



        #endregion


        public void SetPosition(Transform _torso, Transform _aim)
        {
            _torso.position = _aim.position;
        }

    }
}
