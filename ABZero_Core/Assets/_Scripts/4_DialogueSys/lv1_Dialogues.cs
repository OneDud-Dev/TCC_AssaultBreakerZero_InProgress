using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace ABZ_Dialogues
{
    public class lv1_Dialogues : MonoBehaviour
    {
        [Header("Dialogue Setup")]
        public float firstLineDelay;
        public float readLineDelay;
        public float txtSpeed;
        public bool[] dialogueLock;

        [Header("CurrentDialogueState")]
        public DialogueState StartThisDialogue;
        public enum DialogueState {NotYet , Started, Range, Defended, Fliped, Exploded, AttackSounds, SawElevators, ArrivedGate, GateOpened, GotOutside }

        [Header("text reference")]
        public TextMeshProUGUI txtDialogueBox;

        [Header("Dialogue Lines")]
        #region Dialogues
        public string[] startLines;                 //     0
        public string[] enteredShootingRangeLines;  //     1
        public string[] usedTheShieldLines;         //     2
        public string[] bulletTargetLines;          //     3
        public string[] MissileTargetLines;         //     4
        public string[] UnderAttackLines;           //     5
        public string[] elevatorBrokenLines;        //     6
        public string[] arrivedAtGatesLines;        //     7
        public string[] gateOpenLines;              //     8
        public string[] outsideLines;               //     9
        public string[] elevator2Lines;             //     10
        #endregion



        [Header("text line Index")]
        #region Indexes
        public int startIndex;
        public int rangeIndex;
        public int shieldIndex;
        public int targetIndex;
        public int missileIndex;
        public int underAttackIndex;
        public int ElevatorIndex;
        public int atGateIndex;
        public int pastGateIndex;
        public int outsideIndex;
        public int elvtor2Index;
        #endregion


        private void FixedUpdate()
        {
            switch (StartThisDialogue)
            {
                case DialogueState.NotYet :
                    StartCoroutine(DelayFirstLine(firstLineDelay));
                    break;
                //=============================================Game has Started======================
                case DialogueState.Started:
                    LockAndRead(0,startLines, startIndex);

                    break;

                //=============================================Got to the right Corridor trigger, go defend===
                case DialogueState.Range:
                    LockAndRead(1, enteredShootingRangeLines, rangeIndex);
                    break;

                //=============================================Used shield, now go shoot======================
                case DialogueState.Defended:
                    LockAndRead(2, usedTheShieldLines, shieldIndex);
                    break;
                
                //=============================================Shot all targets, now shoot missile============
                case DialogueState.Fliped:
                    LockAndRead(3, bulletTargetLines, targetIndex);
                    break;

                //=============================================Exploded the target, now go back================
                case DialogueState.Exploded:
                    LockAndRead(4, MissileTargetLines, missileIndex);
                    break;

                //=============================================Fuck we are under attack, go to east gate======
                case DialogueState.AttackSounds:
                    LockAndRead(5, UnderAttackLines, underAttackIndex);
                    break;

                //=============================================Gate is opening================================
                case DialogueState.SawElevators:
                    LockAndRead(9, elevator2Lines, elvtor2Index);
                    break;


                //=============================================Gate is opening================================
                case DialogueState.ArrivedGate:
                    LockAndRead(6, arrivedAtGatesLines, atGateIndex);
                    break;
                //=============================================This side is ok, lets go=======================
                case DialogueState.GateOpened:
                    LockAndRead(7, gateOpenLines, pastGateIndex);
                    break;
                //=============================================We will arrive at a forward base, and end level
                case DialogueState.GotOutside:
                    LockAndRead(8, outsideLines, outsideIndex);
                    break;
            }
        }

        
        #region Dialogue display methods
        public IEnumerator ReadLine(string[] _dialogue, int _index)
        {
            foreach (char _char in _dialogue[_index].ToCharArray())
            {
                txtDialogueBox.text += _char;
                yield return new WaitForSeconds(txtSpeed);
            }

            StartCoroutine(NextLine(_dialogue, _index));
        }

        private IEnumerator NextLine(string[] _d, int i)
        {
            if (i < _d.Length - 1)
            {
                i++;
                yield return new WaitForSeconds(readLineDelay);
                txtDialogueBox.text = string.Empty;
                StartCoroutine(ReadLine(_d, i));
            }

            else
            {
                yield return new WaitForSeconds(readLineDelay);
                txtDialogueBox.text = string.Empty;
            }
        }


        #endregion
    
        private IEnumerator DelayFirstLine(float _seconds)
        {
            yield return new WaitForSeconds(_seconds);
            StartThisDialogue = DialogueState.Started;
        }

        private void LockAndRead(int _LockIndex, string[] _LineToRead, int _LineIndex)
        {
            if (!dialogueLock[_LockIndex])
            {
                StartCoroutine(ReadLine(_LineToRead, _LineIndex));

                dialogueLock[_LockIndex] = true;
            }
        }


        public void SwitchState2() => StartThisDialogue = DialogueState.Range;
        public void SwitchState3() => StartThisDialogue = DialogueState.Defended;
        public void SwitchState4() => StartThisDialogue = DialogueState.Fliped;
        public void SwitchState5() => StartThisDialogue = DialogueState.Exploded;
        public void SwitchState6() => StartThisDialogue = DialogueState.AttackSounds;
        public void SwitchState7() => StartThisDialogue = DialogueState.ArrivedGate;
        public void SwitchState8() => StartThisDialogue = DialogueState.GateOpened;
        public void SwitchState9() => StartThisDialogue = DialogueState.GotOutside;

        public void SwitchState10() => StartThisDialogue = DialogueState.SawElevators;




    }
}
