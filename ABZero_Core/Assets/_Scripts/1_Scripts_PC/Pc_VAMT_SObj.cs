using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Pc
{
    [CreateAssetMenu(fileName = "SO_Mecha", menuName = "Mecha/SO_MechaData")]
    public class Pc_VAMT_SObj : ScriptableObject
    {
        public int      backwardWalkPower;
        public int      forwardWalkPower;
        public int      forwardBurstPower;

        public int      horizontalStrafePower;
        public int      horizontalBurstPower;

        public float    pivotRotatioValue;
        public float    CamTurnSensitivity;
    }
}
