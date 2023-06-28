using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Weapons
{
    [CreateAssetMenu(fileName = "SO_W_N_Rifle", menuName = "Weapons/W_N_Rifle")]
    public class W_N_AutoRifle : BaseEquipment, IShoot
    {
        public GameObject projectile;

        public void SpawnProjectile(Transform _spawnPoint, GameObject _target)
        {
            GameObject bullet = Instantiate(projectile, _spawnPoint.position, _spawnPoint.rotation);
        }

        public void SpawnProjectile(Transform _spawnPoint)
        {
            GameObject bullet = Instantiate(projectile, _spawnPoint.position, _spawnPoint.rotation);
        }

        public void Fuck(Transform arg)
        {
            GameObject bullet = Instantiate(projectile, arg.position, arg.rotation);
        }
    }
}
