using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    [CreateAssetMenu(fileName = "SO_Mecha", menuName = "Mecha/SO_MechaData")]
    public class Pc_VAMT_SObj : ScriptableObject
    {
        [Header("Forward")]
        public int      forwardWalk;
        public int      forwardRun;
        public int      forwardBoost;
        [Header("Backwards")]
        public int      backwardWalk;
        public int      backwardRun;
        public int      backwardBoost;
        [Header("Strafe")]
        public int      strafeWalk;
        public int      strafeRun;
        public int      strafeBoost;
        [Header("Rotation")]
        public float    bodyPivotRotNormal;
        public float    bodyPivotRotBoosting;
        public float    camPivotSensitivity;
        [Header("Rotation")]
        public float    maxStamina;
    }
}
