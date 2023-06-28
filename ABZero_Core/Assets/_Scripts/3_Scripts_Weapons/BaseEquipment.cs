using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_Weapons
{
    public abstract class BaseEquipment : ScriptableObject
    {

        public int   baseDamage;
        public int   maxAmmo;
        public int   magazineSize; // can be used diferent movements on meele
        public float rateOfFire; // Meele: time for new movement
        public float reloadTime; // Meele: time after combo completed?

    }
}
