using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    public class Pc_AutoTarget : MonoBehaviour
    {

        #region Variables

        public Pc_References pcData;
        public List<GameObject> enemiesOnRange;

        public GameObject hudTargetPos;
        private Vector3 hudCanvasDirection;

        public GameObject currentTarget;
        public GameObject closestTarget;

        private bool lockTarget;

        #endregion

        #region Unity Methods

        private void Start()
        {
            lockTarget = false;
        }

        private void FixedUpdate()
        {
            GetClosestEnemy(pcData.normalControlForward);
            SetHUDTargetOnCurrent();
            ChangeCurrentTarget();
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                AddEnemyOnRange(other);
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                RemoveEnemyOnRange(other);
            }
        }
        #endregion




        public void AddEnemyOnRange(Collider enemyCo)
        {
            if (!enemiesOnRange.Contains(enemyCo.gameObject))
            {
                enemiesOnRange.Add(enemyCo.gameObject);

                if (currentTarget == null && closestTarget == null)
                {
                    currentTarget = enemyCo.gameObject;
                    closestTarget = enemyCo.gameObject;
                }
                else
                {
                    return;
                }
            }
        }
        public void RemoveEnemyOnRange(Collider enemyCo)
        {
            if (enemiesOnRange.Contains(enemyCo.gameObject))
            {
                if (currentTarget == enemyCo)
                {
                    currentTarget = null;
                }
                if (closestTarget == enemyCo)
                {
                    closestTarget = null;
                }

                enemiesOnRange.Remove(enemyCo.gameObject);
            }
        }

        public void UnlockTarget()
        {
            if (lockTarget) { lockTarget = false; }
        }
        public void LockTarget()
        {
            if (!lockTarget) { lockTarget = true; }
        }




        public void GetClosestEnemy(Transform playerPos)
        {
            float distanceTtoP;
            float shortestD = 1000;
            if (enemiesOnRange.Count <= 0)
            {
                closestTarget = null;
                currentTarget = null;
            }

            else if (enemiesOnRange.Count == 1)
            {
                closestTarget = enemiesOnRange[0];
            }

            else if (enemiesOnRange.Count > 1)
            {
                foreach (GameObject item in enemiesOnRange)
                {
                    distanceTtoP = Vector3.Distance(playerPos.position, item.transform.position);
                    if (distanceTtoP < shortestD)
                    {
                        shortestD = distanceTtoP;
                        closestTarget = item;
                    }
                }
            }
        }


        public void ChangeCurrentTarget()
        {
            if (currentTarget == null && closestTarget == null)
            {
                return;
            }

            if (!enemiesOnRange.Contains(currentTarget))
            {
                currentTarget = null;

                if (closestTarget != null)
                {
                    currentTarget = closestTarget;
                }

            }
        }


        public void SetHUDTargetOnCurrent()
        {
            if (enemiesOnRange.Count <= 0)
            {
                if (hudTargetPos.activeInHierarchy)
                {
                    hudTargetPos.SetActive(false);
                }
                return;
            }

            else
            {
                if (currentTarget != null)
                {
                    if (!hudTargetPos.activeInHierarchy)
                    {
                        hudTargetPos.SetActive(true);
                    }

                    SetTargetPosition(currentTarget.transform);
                    PoinToCam(pcData.camPos);
                }

                else if (currentTarget == null)
                {

                }

                else
                {
                    hudTargetPos.SetActive(false);
                    return;
                }
            }
        }

        public void SetTargetPosition(Transform pcPos)
        {
            hudTargetPos.transform.position = pcPos.position;
        }
        public void PoinToCam(Transform camPos)
        {
            hudCanvasDirection = new Vector3(camPos.position.x - hudTargetPos.transform.position.x,
                                             camPos.position.y - hudTargetPos.transform.position.y,
                                             camPos.position.z - hudTargetPos.transform.position.z);

            hudTargetPos.transform.rotation = Quaternion.LookRotation(hudCanvasDirection);
        }
    }
}
