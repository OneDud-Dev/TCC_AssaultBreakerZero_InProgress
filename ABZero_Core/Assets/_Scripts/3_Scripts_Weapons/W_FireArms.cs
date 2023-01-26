using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Weapons
{
    public class W_FireArms : BaseWeapon
    {
        public int baseDamage;
        public int maxAmmo;
        public int magazineSize;
        public float rateOfFire;
        public float reloadTime;


        public GameObject projectile;

        public void SpawnProjectile(Transform _spawnPoint)
        {
            Instantiate(projectile, _spawnPoint.position, _spawnPoint.rotation);
        }


        //missile method
        //public void SpawnProjectile(Transform _spawnPoint,
        //                            Transform _target)
        //{
        //    GameObject projObj = Instantiate(projectile, _spawnPoint.position,
        //                                                 _spawnPoint.rotation);
        //
        //    P_MissileBehavior miBehave = projObj.GetComponent<P_MissileBehavior>();
        //    miBehave.SetTarget(_target);
        //}
    }
}
