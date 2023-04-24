using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABZ_Weapons;

namespace ABZ_Pc
{
    public class Pc_Shooting : MonoBehaviour
    {
        public   Pc_References   pcData;
        private  Pc_AutoTarget   pcTarget;
        public   bool            ShootdebuggerGismos;

        #region Variables

                 W_FireArms      equippedPrimary;
                 W_FireArms      equippedSecondary;
                 W_FireArms      equippedSpecial;

        #region ENUMS

        public  enum WeaponNormalTypes   { Empty, Rifle, Gauss, Laser };
        public  enum WeaponSpecialTypes  { Empty, Missile, Mines, Cannon, Mortar }
        public  enum WeaponMeleeTypes    { Sword, Axe, Hand, Shield }
        #endregion

        #region Weapons type reference
                 W_N_Empty       pEmpty;
                 W_N_AutoRifle   pAutoRifle;
                 W_N_Gauss       pGaus;
                 W_N_Laser       pLaser;

                 W_S_MissileLcher sMissileLaucher;
                 W_S_Cannon      sCannon;
                 W_S_Motar       sMortar;
                 W_S_Mines       sMines;
                 W_S_Empty       sEmpty;

                 W_M_Shield      mShield;
                 W_M_Sword       mSword;
                 W_M_Axe         mAxe;
        #endregion

        #region Primary
        [Header("")]
        public  WeaponNormalTypes currentPrimary;
        public  int     primaryCurrentClip;
        public  float   primaryTimer;
        public  bool    primaryEmpty;
        public  bool    isRealoadingPrimary;
        public  bool    isHoldingPrimaryFire;
        #endregion

        #region Secondary
        [Header("")]
        public  WeaponNormalTypes currentSecondary;
        public  float   secondaryTimer;
        public  bool    usedSecondary;
        public  int     secondaryCurrentClip;
        public  bool    isRealoadingSecondary;
        #endregion

        #region Special
        [Header("")]
        public  WeaponSpecialTypes currentSpecial;
        public  int     specialCurrentClip;
        public  float   specialTimer;
        public  bool    specialEmpty;
        public  bool    isRealoadingSpecial;
        public  bool    isHoldingSpecialFire;
        public  bool    missileSpawnPos;
        #endregion


        #endregion



        private void Start()
        {
            pcTarget = pcData.pcAutoTarget;

            //get weapon type references
            pAutoRifle  = pcData.gunAR;
            pGaus       = pcData.gunGaus;
            pLaser      = pcData.gunLaser;
            pEmpty      = pcData.noWeapon;
            sMissileLaucher = pcData.gunMissile;
            sCannon     = pcData.gunCannon;
            sMortar     = pcData.gunMortar;
            sMines      = pcData.gunMine;
            sEmpty      = pcData.noSpecial;
            
            isHoldingPrimaryFire = false;
            isHoldingSpecialFire = false;

            //set equipped weapon to script
            SetWeapons();
        }


        #region Unity Calculation
        private void Update()
        {
            //lembrando que o tiro é chamado por evento e aqui só é calculado o tempo pra que próximo evento possa funcionar
            
            PrimaryWeaponCooldown();
            //SecondaryWeaponCooldown();
            //SpecialWeaponCooldown();


            if (isHoldingPrimaryFire) { UsePrimaryWeapon(); }
            //if (isHoldingSsecondaryFire) { UseSecondaryWeapon(); }

        }

        #endregion





        //_______________________________________METHODS_______________________________________
        // setting script

        public void SetWeapons()
        {
            switch (currentPrimary)
            {
                case WeaponNormalTypes.Empty:
                    break;
                case WeaponNormalTypes.Rifle:
                    equippedPrimary = pAutoRifle;
                    break;
                case WeaponNormalTypes.Gauss:
                    equippedPrimary = pGaus;
                    break;
                case WeaponNormalTypes.Laser:
                    equippedPrimary = pLaser;
                    break;

                default:
                    break;
            }

            switch (currentSecondary)
            {
                //    case WeaponNormalTypes.Rifle:
                //        equippedSecondary = pAutoRifle;
                //        break;
                //    case WeaponNormalTypes.Gauss:
                //        equippedSecondary = pGaus;
                //        break;
                //    case WeaponNormalTypes.Laser:
                //        equippedSecondary = pLaser;
                //        break;

                default:
                    break;
            }

            switch (currentSpecial)
            {
                case WeaponSpecialTypes.Missile:
                    equippedSpecial = sMissileLaucher;
                    break;
                case WeaponSpecialTypes.Mines:
                    //equippedSpecial = gunMine;
                    break;
                case WeaponSpecialTypes.Cannon:
                    equippedSpecial = sCannon;
                    break;
                case WeaponSpecialTypes.Mortar:
                    equippedSpecial = sMortar;
                    break;


                default: break;
            }
        }


        // on player input    
        public void PrimaryWeaponCooldown()
        {
            if (equippedPrimary == pEmpty)
            {
                return;
            }

            if (primaryTimer < equippedPrimary.reloadTime + 1)
            {
                primaryTimer += Time.deltaTime;
                
                //auto reload
                if (primaryCurrentClip == equippedPrimary.magazineSize) //-----TODO: count max ammo
                { StartCoroutine(ReloadPrimaryCoroutine()); }
            }
        }
        public void SecondaryWeaponCooldown()
        {
            if (equippedSecondary == pEmpty)
            {
                return;
            }
            if (secondaryTimer < equippedSecondary.reloadTime + 1)
            {
                secondaryTimer += Time.deltaTime;
                if (secondaryCurrentClip == equippedSecondary.magazineSize)
                {
                    StartCoroutine(ReloadSecondaryCoroutine());
                }
            }
        }
        public void SpecialWeaponCooldown()
        {
            if (equippedSpecial == sEmpty)
            {
                return;
            }

            if (specialTimer < equippedSpecial.reloadTime + 1)
            {
                specialTimer += Time.deltaTime;
                //auto reload
                if (specialCurrentClip == equippedSpecial.magazineSize)
                { StartCoroutine(ReloadSpecialCoroutine()); }
            }
        }

        // Using weapons

        #region Primary attack

        //primaryTimer é usado como cooldown entre tiros do mesmo clip
        //              é usado como tempo sem atirar enquanto recarega

        public void HoldToFirePrimary()
        {
            if (!isHoldingPrimaryFire)
            { isHoldingPrimaryFire = true; }
            else
            { isHoldingPrimaryFire = false; }
        }

        public void UsePrimaryWeapon()
        {
            if (!primaryEmpty)
            {
                isRealoadingPrimary = false;
                StartCoroutine(PrimaryWeaponCoroutine());
            }
        }

        private IEnumerator PrimaryWeaponCoroutine()
        {
            if (primaryTimer > equippedPrimary.rateOfFire)
            {
                primaryTimer = 0f;
                primaryCurrentClip += 1;
                equippedPrimary.SpawnProjectile(pcData.rightArmTarget);


                //TRY - start reload on the same frame?
                if (primaryCurrentClip == equippedPrimary.magazineSize) { primaryEmpty = true; }

                yield return null;
            }
        }

        private IEnumerator ReloadPrimaryCoroutine()
        {
            primaryTimer += Time.deltaTime;

            if (primaryTimer > equippedPrimary.reloadTime)
            {
                primaryTimer = 0f;
                primaryCurrentClip = 0;

                primaryEmpty = false;

                yield return null;
            }

        }



        #endregion


        #region Secondary Attack

        public void CallUseSecondaryWeapon()
        {
            if (secondaryCurrentClip > equippedSecondary.magazineSize)
            {
                usedSecondary = true;
                StartCoroutine(ReloadSecondaryCoroutine());
            }

            else
            {
                usedSecondary = false;
                StartCoroutine(SecondaryWeaponCoroutine());
            }
        }

        private IEnumerator SecondaryWeaponCoroutine()
        {
            if (secondaryTimer > equippedSecondary.rateOfFire)
            {
                usedSecondary = true;
                secondaryTimer = 0f;
                secondaryCurrentClip += 1;

                equippedSecondary.SpawnProjectile(pcData.torsoTarget);
                yield return null;
            }
            else
            {
                usedSecondary = false;
                secondaryTimer += Time.deltaTime;
            }
        }

        private IEnumerator ReloadSecondaryCoroutine()
        {
            secondaryTimer += Time.deltaTime;

            if (secondaryTimer > equippedSecondary.reloadTime)
            {
                secondaryTimer = 0;
                secondaryCurrentClip = 0;

                usedSecondary = false;
                isRealoadingSecondary = false;
                yield return null;
            }

        }
        #endregion


        #region Special Attack

        public void HoldToFireSpecial()
        {
            if (!isHoldingSpecialFire)
            { isHoldingSpecialFire = true; }
            else
            { isHoldingSpecialFire = false; }
        }

        public void UseSpecialWeapon()
        {
            if (!specialEmpty)
            {
                isRealoadingSpecial = false;

                if (pcData.pcAutoTarget.currentTarget != null)
                {
                    StartCoroutine(SpecialWeaponCoroutine());
                }
            }
        }

        private IEnumerator SpecialWeaponCoroutine()
        {
            if (specialTimer > equippedSpecial.rateOfFire)
            {
                specialTimer = 0f;
                specialCurrentClip += 1;
        
                //if (missileSpawnPos)
                //{
                //    equippedSpecial.SpawnProjectile(pcData.specialArmAim1, pcData.pcTarget.currentTarget.transform);
                //    missileSpawnPos = false;
                //}
                //else
                //{
                // equippedSpecial.SpawnProjectile(pcData.specialArmAim2, pcData.pcTarget.currentTarget.transform);
                //  missileSpawnPos = true;
                //}
        
                if (specialCurrentClip == equippedSpecial.magazineSize) { specialEmpty = true; }
        
                yield return null;
            }
        }

        private IEnumerator ReloadSpecialCoroutine()
        {
            specialTimer += Time.deltaTime;

            if (specialTimer > equippedSpecial.reloadTime)
            {
                specialTimer = 0;
                specialCurrentClip = 0;

                specialEmpty = false;
                isRealoadingSpecial = false;
                yield return null;
            }
        }
        #endregion
    }
}
