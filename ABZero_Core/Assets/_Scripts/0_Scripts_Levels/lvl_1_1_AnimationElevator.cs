using ABZ_Pc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_AnimationElevator : MonoBehaviour
    {

        public bool hasPassed = false;

        public Pc_References activateMove;
        public GameObject elevatorDoor;
        public GameObject backElevatorCollider;
        public Animator animator;

        private void Start()
        {
            animator.SetBool("Activate", true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (hasPassed) { return; }

                activateMove.camMove = true;
                elevatorDoor.SetActive(false);
                backElevatorCollider.SetActive(true);
                hasPassed = true;
            }

            
        }
    }
}
