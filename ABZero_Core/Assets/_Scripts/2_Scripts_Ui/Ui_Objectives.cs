using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace ABZ_Ui
{
    public class Ui_Objectives : MonoBehaviour
    {
        public TMP_Text txtCurrentObjective;


        public void SetObjectiveToUiText(Component sender, object _newObjective)
        {
            txtCurrentObjective.text = (string)_newObjective;
        }
    }
}
