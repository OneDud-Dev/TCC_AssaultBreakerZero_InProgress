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
            NoCursor();
        }


        public void NoCursor()
        {
            Debug.Log("no cursor");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void YesCursor()
        {
            Debug.Log("yesCursor");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
