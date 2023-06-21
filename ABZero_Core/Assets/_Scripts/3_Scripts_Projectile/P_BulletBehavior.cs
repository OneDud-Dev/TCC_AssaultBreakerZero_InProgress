using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Projectiles
{
    public class P_BulletBehavior : MonoBehaviour
    {
        #region Variables

        public Rigidbody bulletRB;
        public GameObject hitBulletParticle;

        public float bullletSpeed = 60f;
        #endregion


        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\


        #region Unity

        private void Start()
        {
            bulletRB.velocity = transform.forward * bullletSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject bulletHit = Instantiate(hitBulletParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
            //send bullet damage data?
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("bullethit");
            GameObject bulletHit = Instantiate(hitBulletParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        private void FixedUpdate()
        {
            Destroy(this.gameObject, 1.6f);
        }


        #endregion
    }
}
