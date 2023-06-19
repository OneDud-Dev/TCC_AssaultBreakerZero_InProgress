using ABZ_GameSystems;
using ABZ_Projectiles;
using UnityEngine;

namespace ABZ_Weapons
{
    [CreateAssetMenu(fileName = "SO_W_S_MissileLcher", menuName = "Weapons/W_S_MissileLcher")]
    public class W_S_MissileLcher : BaseEquipment, IShoot
    {
        public GameObject projectile;

        public void SpawnProjectile(Transform _spawnPoint, GameObject _target)
        {
            GameObject projObj = Instantiate(projectile, _spawnPoint.position, _spawnPoint.rotation);
            projObj.GetComponent<P_MissileBehavior>().AddTargetToMissile(_target.transform);
        }
        public void SpawnProjectile(Transform _spawnPoint)
        {
            Instantiate(projectile, _spawnPoint.position, _spawnPoint.rotation);
        }
    
    
    }
}
