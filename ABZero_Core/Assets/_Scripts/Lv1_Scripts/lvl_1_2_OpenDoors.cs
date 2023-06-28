using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZCore
{
    public class lvl_1_2_OpenDoors : MonoBehaviour
    {
        public  lvl_1_0_LvController lvlCtrl;
        public  Animator    door1;
        public  Animator    door2;
        public  bool        hasBeenCrossed;


        private void Start()
        {
            hasBeenCrossed = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!hasBeenCrossed)
                {
                    lvlCtrl.currentTrigger = lvl_1_0_LvController.lvlTriggers.EnteredArea;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && hasBeenCrossed)
            {
                CloseDoors();
            }
        }


        public void openDoors()
        {
            Debug.Log("opening");
            door1.SetBool("isOpen", true);
            door2.SetBool("isOpen", true);
        }

        public void CloseDoors()
        {
            Debug.Log("closing");
            door1.SetBool("isOpen", false);
            door2.SetBool("isOpen", false);
        }
    }
}
