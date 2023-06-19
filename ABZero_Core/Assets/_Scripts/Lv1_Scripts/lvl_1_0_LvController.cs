using ABZ_Ui;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ABZCore
{
    public class lvl_1_0_LvController : MonoBehaviour
    {
        [Header("Ref")]
        public lvl_1_2_OpenDoors firstDoor;
        public Animator door3;
        public Animator door4;
        public bool doorlock;

        #region txt refs
        [Header("TxtRef")]
        public Ui_HUD_Timer timerCtl;
        public TextMeshProUGUI txtObjetive;
        public TextMeshProUGUI txtDAllied;
        public TextMeshProUGUI txtDEnemy;
        public TextMeshProUGUI txtDNeutral;

        #endregion

        #region triggers
        [Header("LevelTriggers")]
        public lvlTriggers currentTrigger;
        public enum lvlTriggers { Start, EnteredArea, ShotTargets, wave1D, wave2D, wave3D }
        private string objetivo1 = "Testar seu VA-MT e suas armas";
        private string objetivo2 = "Destrua a força invasora";

        #endregion

        [Header("Targets")]
        public int targetsD;

        #region Dialogue things
        [Header("LvlDialogue")]
        public bool[] lockdialogue;
        public string[] startLines;
        public string[] doorLines;
        public string[] targetLines;
        public string[] attack1Lines;
        public string[] attack2Lines;
        public string[] attack3Lines;

        public float readLineDeley;
        public float txtSpeed;
        public int sIndex;
        public int dIndex;
        public int tIndex;
        public int a1Index;
        public int a2Index;
        public int a3Index;

        [Header("enemy waves")]
        public GameObject[] firstWave;
        public GameObject[] secondWave;
        public GameObject[] thirdWave;

        #endregion


        private void Start()
        {
            txtObjetive.text = objetivo1;
            txtDAllied.text = string.Empty;
            txtDEnemy.text = string.Empty;
            txtDNeutral.text = string.Empty;
            timerCtl.StartLevelTimer();
        }

        private void FixedUpdate()
        {
            switch (currentTrigger)
            {
                //----------------------------------------------------------------
                case lvlTriggers.Start:
                    if (!lockdialogue[0])
                    {
                        StartCoroutine(DelayFirstLine());
                        lockdialogue[0] = true;
                    }
                    break;
                //----------------------------------------------------------------
                case lvlTriggers.EnteredArea:
                    if (!lockdialogue[1])
                    {
                        lockdialogue[1] = true;
                        DoorDialogue();
                    }


                    if (dIndex == doorLines.Length - 3 && !doorlock)
                    {
                        doorlock = true;
                    }

                    if (targetsD >= 4)
                    {
                        txtObjetive.text = objetivo2;
                        currentTrigger = lvlTriggers.ShotTargets;
                    }
                    break;
                //----------------------------------------------------------------
                case lvlTriggers.ShotTargets:

                    //spawn wave1
                    break;
                //----------------------------------------------------------------
                case lvlTriggers.wave1D:
                    //spaw wave2

                    break;
                //----------------------------------------------------------------
                case lvlTriggers.wave2D:
                    //spawn wave3

                    break;
                case lvlTriggers.wave3D:
                    //endlevel

                    break;




                default: break;
            }
        }


        public void ActivateWave(GameObject[] wave)
        {
            foreach (GameObject item in wave)
            {
                item.SetActive(true);
            }
        }


        #region Dialogue

        public void StartDialogue() =>  StartCoroutine(ReadDiLine(startLines, sIndex));
        public void DoorDialogue()  =>  StartCoroutine(ReadDiLine(doorLines, dIndex));
        public void TargetDialogue() => StartCoroutine(ReadDiLine(targetLines, tIndex));
        public void Wave1Dialogue() =>  StartCoroutine(ReadDiLine(attack1Lines, a1Index));
        public void Wave2Dialogue() =>  StartCoroutine(ReadDiLine(attack2Lines, a2Index));
        public void Wave3Dialogue() =>  StartCoroutine(ReadDiLine(attack2Lines, a2Index));


        public IEnumerator ReadDiLine(string[] _dialogue, int _index)
        {
            foreach (char c in _dialogue[_index].ToCharArray())
            {
                txtDNeutral.text += c;
                yield return new WaitForSeconds(txtSpeed);
            }

            if (_dialogue.Equals(doorLines) && _index >= doorLines.Length - 3)
            {
                firstDoor.openDoors();
            }

            StartCoroutine(NextLine(_dialogue, _index));
        }

        private IEnumerator NextLine(string[] _d, int i)
        {
            if (i < _d.Length - 1)
            {
                i++;
                yield return new WaitForSeconds(readLineDeley);
                txtDNeutral.text = string.Empty;
                StartCoroutine(ReadDiLine(_d, i));
            }

            else
            {
                yield return new WaitForSeconds(readLineDeley);
                txtDNeutral.text = string.Empty;
            }
        }

        private IEnumerator DelayFirstLine()
        {
            yield return new WaitForSeconds(2);
            StartDialogue();
        }

        #endregion
    }
}
