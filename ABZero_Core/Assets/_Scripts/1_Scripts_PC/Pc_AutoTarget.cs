using ABZ_GameSystems;
using ABZ_Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ABZ_Pc
{
    public class Pc_AutoTarget : MonoBehaviour
    {

        #region Variables

        
        public  Pc_References    pcData;
        public  GameObject       currentTarget;
        private GameObject       closestTarget;
        public  List<GameObject> enemiesOnRange;

        //public  GameObject   hudTargetPos;
        //private Vector3      hudCanvasDirection;

        public  bool             hasTarget;
        private bool             lockTarget;

        #endregion



        #region Unity Methods

        private void Start()
        {
            hasTarget = false;
            lockTarget = false;
        }

       


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ETargetChar") ||
                other.gameObject.CompareTag("ETargetObjective"))
            {
                AddEnemyToList(other.gameObject);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("ETargetChar") ||
                other.gameObject.CompareTag("ETargetObjective"))
            {
                RemoveAEnemyFromList(other.gameObject);
            }
        }
        #endregion

        //----------------------------------------------------------
        //      
        //----------------------------------------------------------
        public void AddEnemyToList(GameObject _enemy)
        {
            if (enemiesOnRange.Contains(_enemy))
            {                return;            } //se já tiver, voltar

            else
            {
                if (!hasTarget)
                    {hasTarget = true;}
                
                enemiesOnRange.Add(_enemy);

                if (currentTarget == null && closestTarget == null)
                {
                    currentTarget = _enemy;
                    closestTarget = _enemy;
                    SetEnemyTargetType(_enemy, Ui_HUD_Targets.TargetType.Main);
                }
                else if (enemiesOnRange.Count > 1)
                {
                    SetEnemyTargetType(_enemy, Ui_HUD_Targets.TargetType.recognized);
                }
            }
        }



        public void RemoveAEnemyFromList(GameObject _enemy)
        {
            if (!enemiesOnRange.Contains(_enemy))
            {                return;            }//se não tiver, voltar

            else
            {
                if (currentTarget == _enemy)    {currentTarget = null;}
                if (closestTarget == _enemy)    {closestTarget = null;}
                                                _enemy.GetComponent<Ui_HUD_Targets>().DeactivateTargets();
                                                enemiesOnRange.Remove(_enemy);

                GetClosestEnemy(pcData.mainPivot);
            }
            //target is disasable by enemy_ctrl when destroyed
        }

        public void OnEventEnemyDestroyed(Component sender, object _enemy)
        {
            GameObject enemy = (GameObject)_enemy;
            
            if (!enemiesOnRange.Contains(enemy))
                                            { return; }//se não tiver, voltar

            else
            {
                if (currentTarget == enemy) { currentTarget = null; }
                if (closestTarget == enemy) { closestTarget = null; }
                enemy.GetComponent<Ui_HUD_Targets>().DeactivateTargets();
                enemiesOnRange.Remove(enemy);

                GetClosestEnemy(pcData.mainPivot);
            }
        }
        private void GetClosestEnemy(Transform playerPos)
        {
            float distanceTtoP;
            float shortestD = 1000;

            switch (enemiesOnRange.Count)
            {
                case <= 0:
                    //closestTarget = null;
                    //currentTarget = null;
                    hasTarget = false;
                    break;
                case 1:
                    closestTarget = enemiesOnRange[0];
                    currentTarget = closestTarget;
                    SetEnemyTargetType(currentTarget, Ui_HUD_Targets.TargetType.Main);
                    break;
                case >1:
                    foreach (GameObject item in enemiesOnRange)
                    {
                        distanceTtoP = Vector3.Distance(playerPos.position, item.transform.position);
                        if (distanceTtoP < shortestD)
                        {
                            shortestD = distanceTtoP;
                            closestTarget = item;
                        }
                    }

                    currentTarget = closestTarget;
                    SetEnemyTargetType(currentTarget, Ui_HUD_Targets.TargetType.Main);
                    break;
            }
        }
      
       


        private void SetEnemyTargetType(GameObject _enemyTarget, Ui_HUD_Targets.TargetType _targetType)
        {
            Ui_HUD_Targets target = _enemyTarget.GetComponent<Ui_HUD_Targets>();

            switch (enemiesOnRange.Count)
            {
                case <= 0:
                    target.ActivateTargets(target, _targetType);
                    break;

                case > 0:
                    target.ActivateTargets(target, _targetType);
                    break;
            }
        }


        



        //not used in this projec
        private void UnlockTarget()
        {
            if (lockTarget) { lockTarget = false; }
        }
        private void LockTarget()
        {
            if (!lockTarget) { lockTarget = true; }
        }

        /*Ui Methods
        private void SetHUDTargetOnCurrent()
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

                else
                {
                    hudTargetPos.SetActive(false);
                    return;
                }
            }
        }

        private void SetTargetPosition(Transform pcPos)
        {
            hudTargetPos.transform.position = pcPos.position;
        }
        private void PoinToCam(Transform camPos)
        {
            hudCanvasDirection = new Vector3(camPos.position.x - hudTargetPos.transform.position.x,
                                             camPos.position.y - hudTargetPos.transform.position.y,
                                             camPos.position.z - hudTargetPos.transform.position.z);

            hudTargetPos.transform.rotation = Quaternion.LookRotation(hudCanvasDirection);
        }
        */
    }
}
