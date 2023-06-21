using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_GameSystems
{
    public class Game_Controller : MonoBehaviour
    {


        //make singleton


        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
