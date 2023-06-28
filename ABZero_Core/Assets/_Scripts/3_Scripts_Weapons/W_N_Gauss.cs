using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Weapons
{
    [CreateAssetMenu(fileName = "SO_W_N_Gauss", menuName = "Weapons/W_N_Gauss")]
    public class W_N_Gauss : BaseEquipment, IShoot
    {
        public GameObject projectile { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void SpawnProjectile(Transform _spawnPoint, GameObject _target)
        {
            throw new System.NotImplementedException();
        }

        public void SpawnProjectile(Transform _spawnPoint)
        {
            throw new System.NotImplementedException();
        }
    }
}
