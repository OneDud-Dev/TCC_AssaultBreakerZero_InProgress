using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Ui
{
    [CreateAssetMenu(fileName = "LevelBriefingData", menuName = "Level/LevelBriefing")]
    public class SO_LevelBriefing : ScriptableObject
    {
        public string levelName;
        public string levelDescription;
        public string levelObjective;
        public string levelObjective2;

        public string pilotTag;
        public string pilotCredentials;
        public string squadronName;

        public string location;

        public float endingTime;
    }
}
