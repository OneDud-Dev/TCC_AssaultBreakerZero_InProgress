using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABZ_Weapons;

namespace ABZ_Ai
{
    public class Ai_Combat : MonoBehaviour
    {
        #region Variables
        [Header("References")]
        public Ai_References data;
        public W_FireArms equipedWeapon;

        [Header("--------Combat----------------")]
        public bool hasAttacked;
        public int currentClip;
        public float weaponTimer;
        public bool isReloading;

        [Header("")]
        public List<GameObject> enemyTargets;
        public int enemyIndex;

        #endregion







        #region Functions

        public void AiAttack()
        {

            if (currentClip > equipedWeapon.magazineSize)
            {
                hasAttacked = true;
                StartCoroutine(WeaponReload());
            }

            else
            {
                hasAttacked = false;
                StartCoroutine(UseEquipedFirearm());
            }
        }
        #endregion

        public void VerifyIfHasEnemyTarget()
        {
            if (enemyTargets.Count > 0)
            {
                if (!enemyTargets[0].activeInHierarchy)
                {
                    enemyTargets.RemoveAt(0);
                }
            }
        }


        #region Coroutines

        private IEnumerator UseEquipedFirearm()
        {

            if (weaponTimer > equipedWeapon.rateOfFire)
            {
                hasAttacked = true;
                weaponTimer = 0f;
                currentClip += 1;

                equipedWeapon.SpawnProjectile(data.bulletSpawner);
                yield return null;
            }

            else
            {
                hasAttacked = false;
                weaponTimer += Time.deltaTime;
            }
        }

        private IEnumerator WeaponReload()
        {
            weaponTimer += Time.deltaTime;

            if (weaponTimer > equipedWeapon.reloadTime)
            {
                weaponTimer = 0;
                currentClip = 0;

                hasAttacked = false;
                isReloading = false;

                yield return null;
            }
        }


        #endregion
    }
}
