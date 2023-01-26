using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Projectiles
{
    public class P_BulletBehavior : MonoBehaviour
    {
        #region Variables

        public Rigidbody bulletRB;

        public float bullletSpeed = 60f;
        #endregion


        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\


        #region Unity

        private void Start()
        {
            bulletRB.velocity = transform.forward * bullletSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }

        private void FixedUpdate()
        {
            Destroy(this.gameObject, 1.6f);
        }


        #endregion
    }
}
