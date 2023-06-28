using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ABZ_Ui
{
    public class Ui_ButtonHover : MonoBehaviour
    {

        public GameObject selected;

        public void Unselect()
        {
            selected.SetActive(false);
        }

        public void select()
        {
            selected.SetActive(true);
        }

    }
}
