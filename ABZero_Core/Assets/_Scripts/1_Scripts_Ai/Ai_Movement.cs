using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ABZ_Ai
{
    public class Ai_Movement : MonoBehaviour
    {
        [Header("References")]
        public Ai_References data;
        private NavMeshAgent aiAgent;
        private Ai_Combat aiCombat;

        public bool travelDebug;
        public bool orbitDebug;



        [Header("--------PathFinding----------------")]
        public bool hasNextWalkpoint;
        public bool travelLock;
        public float confirmationDistance;
        public float distanceToNextPos;
        public Vector3 currentDestination;
        public int travelingIndex;
        public List<Transform> travelingPathNodes;

        [Header("--------Orbiting----------------")]
        public float orbitRadius;
        public int orbitingIndex;
        public List<Transform> orbitingPathNodes;
        public float orbitTimer;
        public float orbitCooldownValue;








        #region Unity
        private void Start()
        {
            //get compponent references
            aiAgent = data.aiAgent;
            aiCombat = data.aiCombat;

            //set movement
            travelingIndex = 0;
            aiCombat.enemyIndex = 0;
            hasNextWalkpoint = travelingPathNodes.Count > 0;
        }


        #endregion



        #region Functions

        public void OrbitIndexIncrement() => aiCombat.enemyIndex++;

        public void SimpleOrbitMovement(Vector3 _target, float _orbitRadius)
        {

            StartCoroutine(ChangeOrbitAfterTime(_target, _orbitRadius));


            aiAgent.SetDestination(currentDestination);
        }
        public void ChangeOrbitTarget(Vector3 _center, float _radius)
        {
            Vector2 circlePoint = Random.insideUnitCircle.normalized * _radius;
            currentDestination = new Vector3(circlePoint.x, 0, circlePoint.y)
                                                +
                                                _center;
        }
        private IEnumerator ChangeOrbitAfterTime(Vector3 center, float Radius)
        {
            orbitTimer += Time.deltaTime;

            if (orbitTimer > orbitCooldownValue)
            {
                ChangeOrbitTarget(center, Radius);
                orbitTimer = 0;

                yield return null;
            }
        }

        //----------------------------------------------------------------------------------

        public void TravelMovement()
        {
            if (hasNextWalkpoint)
            {
                //if (!travelLock)
                //{
                //    travelLock = true;
                //}

                currentDestination = travelingPathNodes[travelingIndex].position;
                aiAgent.SetDestination(currentDestination);
                distanceToNextPos = GetNextPointDistance();

                if (travelingIndex < travelingPathNodes.Count - 1)
                {
                    if (distanceToNextPos < confirmationDistance)
                    {
                        PathIndexIncrement();
                        travelLock = false;
                    }
                }
                else
                {
                    hasNextWalkpoint = false;
                    Debug.Log("Node list empty, NPC is trying to move to node");
                    return;
                }
            }
        }
        public void PatrollingMovement()
        {
            if (!hasNextWalkpoint)
            { Debug.Log("forgot to set patrolling nodes"); return; }

            if (!travelLock)
            {
                StartCoroutine(WaitNextDestination());
                //change destination
            }
            
            distanceToNextPos = GetNextPointDistance();

            if (travelingIndex < travelingPathNodes.Count - 1)
            {
                if (distanceToNextPos < confirmationDistance)
                {
                    PathIndexIncrement();
                    travelLock = false;
                }
            }
            else
            { travelingIndex = 0; }
        }



        public void PathIndexIncrement()
        {
            travelingIndex++;
        }

        public float GetNextPointDistance() => (data.aiPos.position - currentDestination).magnitude;

        public IEnumerator  WaitNextDestination()
        {
            yield return new WaitForSeconds(3f);
            currentDestination = travelingPathNodes[travelingIndex].position;
            aiAgent.SetDestination(currentDestination);

            yield break;
        }

        #endregion
    }
}
