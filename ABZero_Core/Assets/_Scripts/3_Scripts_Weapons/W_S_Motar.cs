using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Weapons
{
    [CreateAssetMenu(fileName = "SO_W_S_Mortar", menuName = "Weapons/W_S_Mortar")]
    public class W_S_Motar : BaseEquipment, IShoot
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
