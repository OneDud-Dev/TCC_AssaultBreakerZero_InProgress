using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABZ_GameSystems;
using ABZ_Projectiles;
using UnityEngine.UI;

namespace ABZ_Levels
{
    public class Lvl_1_1_Targets : MonoBehaviour
    {
        public GameObject minimapLocator;
        public enum TargetType { bulletTarget, MissileTarger }
        public TargetType thisTarget;
        public bool hasBeenHit;

        public GameObject explosionParticle;
        public Animator bulletTargetAnimator;
        public Game_Events onBulletTargetHit;
        public Game_Events onMissileTargetHit;
        public Game_Events OnDestroyedByMissile;
        


        private void OnTriggerEnter(Collider other)
        {
            switch (thisTarget)
            {
                case TargetType.bulletTarget:
                    if (hasBeenHit)
                    {                        break;                    }

                    if (other.gameObject.CompareTag("Proj_P_B"))
                    {
                            bulletTargetAnimator.SetBool("HasBeenHit", true);
                            onBulletTargetHit.Raise();
                            minimapLocator.SetActive(false);
                     
                    }

                    break;


                case TargetType.MissileTarger:
                    if (hasBeenHit)
                    { break; }

                    if (other.gameObject.CompareTag("Proj_P_M"))
                    {
                        onMissileTargetHit.Raise();
                        OnDestroyedByMissile.Raise();
                        minimapLocator.SetActive(false);
                    }
                    break;
            }
        }
    }
}
