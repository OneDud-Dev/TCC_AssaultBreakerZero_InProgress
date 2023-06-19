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
        public BaseEquipment equipedWeapon;

        [Header("--------Combat----------------")]
        public bool hasAttacked;
        public int currentClip;
        public float weaponTimer;
        public bool isReloading;

        [Header("")]
        public GameObject       currentTarget;
        public List<GameObject> enemyTargets;
        public int              enemyIndex;

        #endregion


        #region Methods

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
        public void VerifyIfAiHasAnyTarget()
        {
            if (enemyTargets.Count > 0)
            {
                if (!enemyTargets[0].activeInHierarchy)
                {
                    enemyTargets.RemoveAt(0);
                    
                    if (enemyTargets[0] != null)
                    { currentTarget = enemyTargets[0]; }
                    else
                    { currentTarget = null; }
                }
            }
        }

        private void IfEnemiesChangeToAttack()
        {
            if (currentTarget == null)                  { return; }
            else  { data.aiCtrl.currentState = Ai_Controller.aiState.Attacking; }
        }
        private void IfNoEnemtChangeToTraveling()
        {
            if (currentTarget != null)                 { return; }
            else { data.aiCtrl.currentState = Ai_Controller.aiState.Traveling; }
        }
        public GameObject GetClosestTargetFromEnemyList()
        {
            #region internal variables
            GameObject closestTarget = null;
            float distanceTtoP = 1000f;
            float shortestD = 1f;
            #endregion


            if (enemyTargets.Count == 1)
                {closestTarget = enemyTargets[0];}
            
            else if (enemyTargets.Count > 0)
            {
                foreach (var target in enemyTargets)
                {
                    distanceTtoP = Vector3.Distance(data.bodyPos.position, target.transform.position);;
                    if (distanceTtoP < shortestD)
                    {
                        shortestD = distanceTtoP;
                        closestTarget = target;
                    }
                }
            }

            return closestTarget;
        }
        public void SetOneTargetToPriority(GameObject _target)
        {
            enemyTargets.Insert(0, _target);
        }

        #endregion


        #region Coroutines

        private IEnumerator UseEquipedFirearm()
        {

            if (weaponTimer > equipedWeapon.rateOfFire) //time between each bullet/projectile
            {
                hasAttacked = true;
                weaponTimer = 0f;
                currentClip += 1;

                //equipedWeapon.SpawnProjectile(data.bulletSpawner); ---------------------------------need a target
                yield return null;
            }

            else 
            {
                if (hasAttacked) //to not change every frame to the same thing
                    {hasAttacked = false;}
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
