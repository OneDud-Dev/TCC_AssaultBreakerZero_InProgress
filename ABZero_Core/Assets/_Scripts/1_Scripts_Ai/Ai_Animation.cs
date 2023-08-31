using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ABZ_Ai
{
    public class Ai_Animation : MonoBehaviour
    {
        [Header("References")]
        public Ai_References aiData;
        private Animator     aiAnimator;
        private NavMeshAgent aiAgent;

        [Header("Data")]
        private int    forwardPmeter = Animator.StringToHash("aiVelForward");
        private int    strafePmeter  = Animator.StringToHash("aiVelStrafe");
        private bool isCalculatingSpeed;
        public Vector3 aiVelocity;




        private void Start()
        {
            aiAnimator  = aiData.aiAnimator;
            aiAgent     = aiData.aiAgent;
        }

        private void Update()
        {
            if (aiData.pcData.gameIsRunning)
            {
                ActivateSpeedCalculation();
            }

            aiAnimator.SetFloat(strafePmeter,  aiVelocity.x);
            aiAnimator.SetFloat(forwardPmeter, aiVelocity.z);
        }


        private void ActivateSpeedCalculation()
        {
            if (isCalculatingSpeed && aiData.pcData.gameIsRunning)
            { return; }

            else if (isCalculatingSpeed && !aiData.pcData.gameIsRunning)
            { isCalculatingSpeed = false; }

            else if (!isCalculatingSpeed && aiData.pcData.gameIsRunning)
            {
                isCalculatingSpeed = true;
                StartCoroutine(ICalculateSpeed());
            }
        }

        private IEnumerator ICalculateSpeed()
        {
            while (aiData.pcData.gameIsRunning) // could use event
            {
                aiVelocity = new Vector3(
                   (float)Math.Round(aiData.bodyPos.InverseTransformDirection(aiAgent.velocity).x, 3),
                   (float)Math.Round(aiData.bodyPos.InverseTransformDirection(aiAgent.velocity).y, 3),
                   (float)Math.Round(aiData.bodyPos.InverseTransformDirection(aiAgent.velocity).z, 3));
       
                yield return null;
            }
        }
    }
}
