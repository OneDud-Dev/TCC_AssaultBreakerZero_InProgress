using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABZ_Weapons;
using ABZ_GameSystems;

namespace ABZ_Pc
{
    public class Pc_Shooting : MonoBehaviour
    {
        public   Pc_References   pcData;
        public   Game_Events     onBulletShot;
        public   Game_Events     onMissileShot;
        public   Game_Events     onDefending;
        private  Pc_AutoTarget   pcTarget;
       

        #region Variables

                 BaseEquipment   equippedLeft;
                 BaseEquipment   equippedRight;
                 BaseEquipment   equippedSpecial;

        #region ENUMS

        public  enum WeaponNormalTypes   { Empty, Rifle, Gauss, Laser, Sword, Axe, Hand, Shield };
        public  enum WeaponSpecialTypes  { Empty, Missile, Mines, Cannon, Mortar }
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

        #region Left
        [Header("Left")]
        public bool leftIsActive;
        public  WeaponNormalTypes currentLeftEquip;
        public   int     leftCurrentClip;
        public   float   leftTimer;
        private  bool    leftIsEmpty;
        private  bool    isRealoadingLeftArm;
        public   bool    isHoldingLeftArmAction;
        public   bool    leftArmAnimationMove;
        private  float   activateLeftAnimationTime;

        #endregion

        #region Secondary
        [Header("Right")]
        public bool rightIsActive;
        public WeaponNormalTypes currentRightEquip;
      //public   int     rightCurrentClip;
      //public   float   rightTimer;
      //private  bool    rightIsEmpty;
      //private  bool    isRealoadingRightEquip;
        public   bool    isHoldingRightAction;
        public   bool    rightArmAnimationMove;
        public   float   activateRightAnimationTime;
        #endregion

        #region Special
        [Header("Special")]
        public bool specialIsActive;
        public  WeaponSpecialTypes currentSpecial;
        public  int     specialCurrentClip;
        public  float   specialTimer;
        public  bool    specialIsEmpty;
        public  bool    isRealoadingSpecial;
        public  bool    isHoldingSpecialAction;
        private bool    missileSpawnSide;
        public  bool    specialAnimationMove;
        public  float   activateSpecialAnimationTime;

        #endregion


        #endregion



        private void Start()
        {
            pcTarget = pcData.pcAutoTarget;
            
            
            //get weapon type references
            sEmpty          =   pcData.noSpecial;
            pEmpty          =   pcData.noWeapon;
            pAutoRifle      =   pcData.gunAR;
            mShield         =   pcData.shield;
            sMissileLaucher =   pcData.gunMissile;
            sCannon         =   pcData.gunCannon;
            
            isHoldingLeftArmAction = false;
          //isHoldingRightAction = false;
            isHoldingSpecialAction = false;
            rightArmAnimationMove = false;

            SetWeapons();
        }


        #region Unity Calculation
        private void Update()
        {

            LeftEquipCooldown();
            SpecialEquipCooldown();

            SetRightArm();
            SetLeftArm();
            SetSpecialPos();

            if (isHoldingLeftArmAction)     { ActivateLeftArmEquip(); }
            if (isHoldingSpecialAction)     { ActivateSpecialWeapon(); }
           

            
        }

        #endregion
        




        //_______________________________________METHODS_______________________________________
      
        public void SetWeapons()
        {
            switch (currentLeftEquip)
            {
                case WeaponNormalTypes.Empty:
                    equippedLeft = pEmpty;
                    break;
                case WeaponNormalTypes.Rifle:
                    equippedLeft = pAutoRifle;
                    break;
                case WeaponNormalTypes.Gauss:
                    break;
                case WeaponNormalTypes.Laser:
                    break;
                case WeaponNormalTypes.Sword:
                    break;
                case WeaponNormalTypes.Axe:
                    break;
                case WeaponNormalTypes.Hand:
                    break;
                case WeaponNormalTypes.Shield:
                    break;
            }

            switch (currentRightEquip)
            {
                case WeaponNormalTypes.Empty:
                    equippedRight = pEmpty;
                    break;
                case WeaponNormalTypes.Rifle:
                    break;
                case WeaponNormalTypes.Gauss:
                    break;
                case WeaponNormalTypes.Laser:
                    break;
                case WeaponNormalTypes.Sword:
                    break;
                case WeaponNormalTypes.Axe:
                    break;
                case WeaponNormalTypes.Hand:
                    break;
                case WeaponNormalTypes.Shield:
                    equippedRight = mShield;
                    break;
            }

            switch (currentSpecial)
            {
                case WeaponSpecialTypes.Missile:
                    equippedSpecial = sMissileLaucher;
                    break;
                case WeaponSpecialTypes.Mines:
                    break;
                case WeaponSpecialTypes.Cannon:
                    equippedSpecial = sCannon;
                    break;
                case WeaponSpecialTypes.Mortar:
                    equippedSpecial = sMortar;
                    break;
            }
        }


       
        public void LeftEquipCooldown()
        {
            if (equippedLeft == pEmpty)         { return; }

            if (leftTimer < equippedLeft.reloadTime + 1)
            {
                leftTimer += Time.deltaTime;
                
                //auto reload
                if (leftCurrentClip >= equippedLeft.magazineSize -1)
                {
                    leftIsEmpty = true; leftArmAnimationMove = false;
                    StartCoroutine(ReloadLeftEquipCoroutine()); 
                }
            }
        }
        public void SpecialEquipCooldown()
        {
            if (equippedSpecial == sEmpty)
            {
                return;
            }

                else if (specialTimer < equippedSpecial.reloadTime + 1)
                {
                    specialAnimationMove = true;
                    specialTimer += Time.deltaTime;
                    if (specialCurrentClip == equippedSpecial.magazineSize)
                    { StartCoroutine(ReloadSpecialCoroutine()); }
                }
        }


        #region Left Arm Equipment Usage ---------------------------------------------------------------------

        public void SetLeftArm()
        {
            if (!leftArmAnimationMove && !leftIsEmpty)
            {
                if (activateLeftAnimationTime > 0)
                {
                    activateLeftAnimationTime -= Time.deltaTime;
                    if (activateLeftAnimationTime < 0)
                    { activateLeftAnimationTime = 0f; }

                    pcData.pcAnimeScript.DeactivateLeftArm(activateLeftAnimationTime);
                }
            }


            else if (!leftArmAnimationMove && leftIsEmpty)
            {
                if (activateLeftAnimationTime > 0.22)
                {activateLeftAnimationTime = 0.22f;}

                if (activateLeftAnimationTime > 0)
                {
                    activateLeftAnimationTime -= Time.deltaTime;
                    if (activateLeftAnimationTime < 0)
                    { activateLeftAnimationTime = 0f; }

                    pcData.pcAnimeScript.ReloadLeftArmOff(activateLeftAnimationTime);
                }
            }

            if (leftArmAnimationMove && !leftIsEmpty)
            {
                if (activateLeftAnimationTime <= 0.22)
                {
                    activateLeftAnimationTime += Time.deltaTime;
                    if (activateLeftAnimationTime > 0.22f)
                    { activateLeftAnimationTime = 0.22f; }

                    pcData.pcAnimeScript.ReloadLeftArmOn(activateLeftAnimationTime);
                }
            }

            else if (leftArmAnimationMove && leftIsEmpty)
            {
                if (activateLeftAnimationTime <= 0.22)
                {
                    activateLeftAnimationTime += Time.deltaTime;
                    if (activateLeftAnimationTime > 0.22f)
                    { activateLeftAnimationTime = 0.22f; }

                    pcData.pcAnimeScript.ActivateLeftArm(activateLeftAnimationTime);
                }
            }
        }
        public void HoldToUseLeftArm()
        {
            if (!isHoldingLeftArmAction)
            {
                isHoldingLeftArmAction  = true;
            }
            else
            { 
                isHoldingLeftArmAction  = false; }
            }
        public void ActivateLeftArmEquip()
        {
            if (leftIsActive)
            {
                if (!leftIsEmpty)
                {
                    StartCoroutine(LeftEquipCoroutine());
                }
            }
        }
        public IEnumerator LeftEquipCoroutine()
        {
            
            if (leftTimer > equippedLeft.rateOfFire)
            {
                leftTimer = 0f;
                leftCurrentClip += 1;
                ((IShoot)equippedLeft).SpawnProjectile(pcData.spawn_Rifle_Left);

                if (leftCurrentClip >= equippedLeft.magazineSize - 1)
                { 
                    leftIsEmpty = true; 
                }
            }

            yield return null;
        }
        public IEnumerator ReloadLeftEquipCoroutine()
        {

            if (leftTimer > equippedLeft.reloadTime)
            {
                leftTimer = 0f;
                leftCurrentClip = 0;

                leftIsEmpty = false;
                leftArmAnimationMove = true;

                yield return null;
            }

        }


        public void EventTurnOnLeftArm() => leftIsActive = true;
        public void EventTurnOffLeftArm() => leftIsActive = false;
        #endregion


        #region Secondary Attack---------------------------------------------------------------------------


        public void HoldToUseRightArm()
        {
            if (!isHoldingRightAction)
            { isHoldingRightAction = true;
                rightArmAnimationMove = true;
            }
            else
            { isHoldingRightAction = false;
                rightArmAnimationMove = false;
            }
        }

        public void SetRightArm()
        {
            if (!rightArmAnimationMove)
            {
                if (activateRightAnimationTime > 0)
                {
                    activateRightAnimationTime -= Time.deltaTime;
                    if (activateRightAnimationTime < 0)
                    { activateRightAnimationTime = 0f; }

                    pcData.pcAnimeScript.DeactivateRightArm(activateRightAnimationTime);

                    StartCoroutine(leftarmONWhenDefending());
                }
            }

            
            if (rightArmAnimationMove)
            {
                if (activateRightAnimationTime <= 0.22)
                {
                    activateRightAnimationTime += Time.deltaTime;
                    if (activateRightAnimationTime > 0.22f)
                    { activateRightAnimationTime = 0.22f; }

                    pcData.pcAnimeScript.ActivateRightArm(activateRightAnimationTime);
                    leftIsActive = false;
                }
            }

        }

        public IEnumerator leftarmONWhenDefending()
        {
            yield return new WaitForSeconds(0.225f);
            leftIsActive = true;
        }

        #endregion


        #region Special Attack----------------------------------------------------------------

        public void HoldToUseSpecial()
        {
            if (!isHoldingSpecialAction)
            { isHoldingSpecialAction = true; }
            else
            { isHoldingSpecialAction = false; }
        }
        public void ActivateSpecialWeapon()
        {
          
            if (!specialIsEmpty)
            {
                isRealoadingSpecial = false;

                StartCoroutine(SpecialWeaponCoroutine());
            }
            
        }
        private IEnumerator SpecialWeaponCoroutine()
        {
            if (specialTimer > equippedSpecial.rateOfFire)
            {
                specialTimer = 0f;
                specialCurrentClip += 1;

                ShootEquipedSpecial();

                if (specialCurrentClip == equippedSpecial.magazineSize)
                    { specialIsEmpty = true; specialAnimationMove = false; }
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

                specialIsEmpty = false;
                isRealoadingSpecial = false;
                
                yield return null;
            }
        }



        private void ShootEquipedSpecial()
        {
            switch (currentSpecial)
            {
                case WeaponSpecialTypes.Empty:
                    break;
                case WeaponSpecialTypes.Missile:
                    switch (pcTarget.hasTarget)  
                    {
                        case true:
                            if (missileSpawnSide)
                            {
                                ((IShoot)equippedSpecial).SpawnProjectile(pcData.spawn_S_M_Left, pcTarget.currentTarget);
                                missileSpawnSide = false;
                            }
                            else
                            {
                                ((IShoot)equippedSpecial).SpawnProjectile(pcData.spawn_S_M_Right, pcTarget.currentTarget);
                                missileSpawnSide = true;
                            }
                            break;
                        case false:
                            if (missileSpawnSide)
                            {
                                ((IShoot)equippedSpecial).SpawnProjectile(pcData.spawn_S_M_Left);
                                missileSpawnSide = false;
                            }
                            else
                            {
                                ((IShoot)equippedSpecial).SpawnProjectile(pcData.spawn_S_M_Right);
                                missileSpawnSide = true;
                            }

                            break;
                    }

                    
                    break;
                case WeaponSpecialTypes.Mines:
                    break;
                case WeaponSpecialTypes.Cannon:
                    ((IShoot)equippedSpecial).SpawnProjectile(pcData.spawn_S_Cannon);
                    break;
                case WeaponSpecialTypes.Mortar:
                    break;
            }
        }
        #endregion
        
        private void SetSpecialPos()
        {
            if (!specialAnimationMove)
            {
                if (activateSpecialAnimationTime > 0)
                {
                    activateSpecialAnimationTime -= Time.deltaTime;
                    if (activateSpecialAnimationTime < 0)
                    { activateSpecialAnimationTime = 0f; }

                    pcData.pcAnimeScript.DeactivateSpecialArm(activateSpecialAnimationTime);

                    specialAnimationMove = true;
                }
            }

            if (specialAnimationMove)
            {
                if (activateSpecialAnimationTime <= 0.22)
                {
                    activateSpecialAnimationTime += Time.deltaTime;
                    if (activateSpecialAnimationTime > 0.22f)
                    { activateSpecialAnimationTime = 0.22f; }
                    pcData.pcAnimeScript.DeactivateSpecialArm(activateSpecialAnimationTime);

                    specialAnimationMove = false;
                }
            }
        }
    }
}
