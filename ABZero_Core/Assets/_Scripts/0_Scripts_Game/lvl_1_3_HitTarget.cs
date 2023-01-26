using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZCore
{
    public class lvl_1_3_HitTarget : MonoBehaviour
    {
        public lvl_1_0_LvController lvlCtrl;
        public int hitPoints;
        public GameObject hitParticle;
        public GameObject destroyedParticle;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            if (hitPoints < 1)
            {
                lvlCtrl.targetsD++;
                this.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                TakeDamage(collision);
            }
        }


        private void TakeDamage(Collision hit)
        {
            hitPoints -= 1;
            Instantiate(hitParticle, hit.transform.position, Quaternion.identity);
        }
    }
}
