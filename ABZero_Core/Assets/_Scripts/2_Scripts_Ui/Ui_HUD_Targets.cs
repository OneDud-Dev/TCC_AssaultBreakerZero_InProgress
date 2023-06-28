using ABZ_GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ABZ_Ui
{
    public class Ui_HUD_Targets : MonoBehaviour
    {
        #region Variables
        public bool isNotEnemy;
        public bool targetIsActive;
        public TargetType thisTargetType;
        public enum TargetType { Main, next, recognized }

        [Header("AiReferences")]
        public Transform mainCam;
        public GameObject currentTarget;

        public GameObject hudMainTarget;
        public GameObject hudNextTarget;
        public GameObject hudRecognizedTarget;
        #endregion

       

        private void FixedUpdate()
        {
            if (!targetIsActive) { return; }
            else                 { PoinToCam(mainCam, currentTarget); }
        }

        private void OnDisable()
        {
            
        }

        public void ActivateTargets(Ui_HUD_Targets _thisComponent, TargetType _thisTarget)
        {
            if (_thisComponent != this) { return; }
            else
            {
                targetIsActive = true;
                switch (_thisTarget)
                {
                    case TargetType.Main:
                        currentTarget = hudMainTarget;                    break;
                    case TargetType.next:
                        currentTarget = hudNextTarget;                    break;
                    case TargetType.recognized:
                        currentTarget = hudRecognizedTarget;              break;
                }
                currentTarget.SetActive(true);
            }
        }

        public void DeactivateTargets()
        {
            targetIsActive = false;
            currentTarget.SetActive(false);
            currentTarget = null;
        }

        public void ChangeTargetType(TargetType _newType)
        {
            switch (_newType)
            {
                case TargetType.Main:
                    thisTargetType = TargetType.Main;                    break;
                case TargetType.next:
                    thisTargetType = TargetType.next;                    break;
                case TargetType.recognized:
                    thisTargetType = TargetType.recognized;              break;
            }
        }

        private void PoinToCam(Transform _camPos, GameObject _hudTarget)
        {
            Vector3 hudCanvasDirection = new Vector3(_camPos.position.x - _hudTarget.transform.position.x,
                                                     _camPos.position.y - _hudTarget.transform.position.y,
                                                     _camPos.position.z - _hudTarget.transform.position.z);

            _hudTarget.transform.rotation = Quaternion.LookRotation(hudCanvasDirection);
        }

        public void GetCamTransformFromEvent(Component _sender, object _camera)
        {
            mainCam =  (Transform)_sender;
        }
    }
}
