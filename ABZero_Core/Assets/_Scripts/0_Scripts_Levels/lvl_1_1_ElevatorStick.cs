using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Levels
{
    public class lvl_1_1_ElevatorStick : MonoBehaviour
    {
        public Transform elevatorTransform;

        private void OnTriggerEnter(Collider other)
        {
            other.transform.SetParent(elevatorTransform);
        }

        private void OnTriggerExit(Collider other)
        {
            other.transform.SetParent(null);
        }

    }
}
