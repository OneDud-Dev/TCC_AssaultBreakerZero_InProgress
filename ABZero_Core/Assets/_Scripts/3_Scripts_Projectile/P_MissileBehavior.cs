using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Projectiles
{
    public class P_MissileBehavior : MonoBehaviour
    {

        
        public Rigidbody  missile_RB;
        public Transform  missile_Trnsfm;
        public Transform  missile_Target;
        public GameObject explosionParticle;

        public float maxSpeed;
        public float rotForce;
        public float destroyTime;
        public float destroyTimeIfNoTarget;

        Vector3     heading;
        Vector3     currentSpeed;
        bool        hasTarget = false;

        public Vector3 debugAngularVelocity; //---------delete later

        

        private void FixedUpdate()
        {
            switch (hasTarget)
            {
                case false:
                    Homing();
                    
                    Destroy(this.gameObject, destroyTimeIfNoTarget );
                break;


                case true:
                    //TargetLostByDestroyed(missile_Target.gameObject);
                    
                    Homing();
                    
                    Destroy(this.gameObject, destroyTime);
                break;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Instantiate(explosionParticle, other.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(explosionParticle, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }


        public void AddTargetToMissile(Transform _target)
        { 
            hasTarget = true;
            missile_Target = _target;
        }

        public void TargetLostByDestroyed(GameObject _target)
        {
            if (_target.activeInHierarchy)  { return;            }
            else                            { hasTarget = false; }
        }


        void Homing()
        {
            currentSpeed = transform.forward * Mathf.Lerp(1, maxSpeed, 3f);

            switch (hasTarget)
            {
                case false:
                    missile_RB.velocity = currentSpeed;
                    break;


                case true:
                    heading = (missile_Target.position - missile_Trnsfm.position).normalized;
                    Vector3 rotationAmount = Vector3.Cross(missile_Trnsfm.forward, heading);

                    missile_RB.angularVelocity = rotationAmount * rotForce;
                    missile_RB.velocity = currentSpeed;


                    debugAngularVelocity = missile_RB.angularVelocity; //-----------------------------delete later
                    break;
            }
            
        }
        











    }
}
