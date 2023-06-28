using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class Lvl1_1_MLncher : MonoBehaviour
    {

        
        public bool isShooting = true;
        public float rateOfFire;
        public float timer;
        public GameObject projectil;
        public Transform spawn;
        public AudioSource lchsound;
     
        private void Update()
        {
            timer += Time.deltaTime;
            
            if (timer > rateOfFire)
            {
                Instantiate(projectil, spawn.position, spawn.rotation);
                lchsound.Play();

                timer= 0;
            }
        }
        public void deactivateShooting()
        {
            isShooting = false;
        }

    }
}
