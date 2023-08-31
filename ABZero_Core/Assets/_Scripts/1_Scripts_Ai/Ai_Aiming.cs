using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ABZ_Ai
{
    public class Ai_Aiming : MonoBehaviour
    {
        [Header("References")]

        public Ai_References data;
        bool    noPlayerReference;



        private void Update()
        {
            //game is runing guard
            //if (!data.pcData.gameIsRunning) return;
            //else if (data.pcData == null && !noPlayerReference)//delete later
            //{
            //    noPlayerReference = true;
            //    Debug.Log($"{data.Model} has no player reference");
            //}

            //only aiming controls
            switch (data.aiCtrl.thisAiNature)
            {
                case Ai_Controller.aiNature.Chaser:
                    switch (data.aiCtrl.ThisAiState)
                    {
                        case Ai_Controller.aiState.Moving:
                            InstantRotateToTarget(data.upperBodyPivot.position, data.aiAgent.transform);
                            InstantRotateToTarget(data.bodyPos.transform.position, data.aiAgent.transform);
                            break;
                        case Ai_Controller.aiState.Attacking:
                            RotationOverTime(data.upperBodyPivot, data.aiCombat.currentTarget.transform, 35f);
                            CopyRotationOverTime(data.bodyPos.transform, data.aiAgent.transform, 35f);
                            break;
                        case Ai_Controller.aiState.Chasing:
                            break;
                    }
                    break;


                case Ai_Controller.aiNature.Focused:
                    switch (data.aiCtrl.ThisAiState)
                    {
                        case Ai_Controller.aiState.Moving:
                            InstantRotateToTarget(data.upperBodyPivot.position,   data.aiAgent.transform);
                            InstantRotateToTarget(data.bodyPos.transform.position, data.aiAgent.transform);
                            break;
                        case Ai_Controller.aiState.Attacking:
                            RotationOverTime(data.upperBodyPivot,   data.aiCombat.currentTarget.transform, 50f);
                            CopyRotationOverTime(data.bodyPos.transform, data.aiAgent.transform, 35f);
                            break;
                        case Ai_Controller.aiState.Chasing:
                            break;
                    }
                    break;


                case Ai_Controller.aiNature.Patroler:
                    switch (data.aiCtrl.ThisAiState)
                    {
                        case Ai_Controller.aiState.Moving:
                            CopyRotationOverTime(data.upperBodyPivot  , data.aiAgent.transform, 35f);
                            CopyRotationOverTime(data.bodyPos.transform, data.aiAgent.transform, 35f);
                            break;
                        case Ai_Controller.aiState.Attacking:
                            RotationOverTime(data.upperBodyPivot  , data.aiCombat.currentTarget.transform, 50f);
                            CopyRotationOverTime(data.bodyPos.transform, data.aiAgent.transform,                35f);
                            break;
                        case Ai_Controller.aiState.Chasing:
                            break;
                    }

                    break;

            }
        }







        public void PointForward(Transform _bodyPart)
        {
            _bodyPart.rotation = Quaternion.LookRotation(data.lookTarget.position);
        }

        public void InstantRotateToTarget(Vector3 _target, Transform _bodyPart)
        {
            Vector3 lookdir = new Vector3(_target.x - _bodyPart.position.x,
                                       0,
                                       _target.z - _bodyPart.position.z);

            _bodyPart.rotation = Quaternion.LookRotation(lookdir);
        }

        public void CopyRotationOverTime(Transform bodyObj, Transform target, float rotationSpeed)
        {
            bodyObj.rotation = Quaternion.RotateTowards(bodyObj.rotation,
                                                        target.rotation,
                                                        rotationSpeed);
        }

        public void RotationOverTime(Transform part, Transform target, float rotationSpeed)
        {
            var partDirection = Quaternion.LookRotation(new Vector3(target.position.x - part.position.x, 0,
                                                                    target.position.z - part.position.z) );

            part.rotation = Quaternion.RotateTowards(part.rotation, partDirection, rotationSpeed * Time.deltaTime);
        }

        public void MoveTargetToPredictedPosition()
        {

        }
    }
}
