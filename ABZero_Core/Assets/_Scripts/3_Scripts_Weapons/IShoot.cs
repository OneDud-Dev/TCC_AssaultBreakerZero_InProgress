using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Weapons
{
    public interface IShoot
    {
        

        void SpawnProjectile(Transform _spawnPoint, GameObject _target);
        void SpawnProjectile(Transform _spawnPoint);
    }
}
