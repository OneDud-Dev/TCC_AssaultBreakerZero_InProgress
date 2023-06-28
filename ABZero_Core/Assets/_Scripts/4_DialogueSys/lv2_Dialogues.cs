using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ABZ_Dialogues
{
    public class lv2_Dialogues : MonoBehaviour
    {
        [Header("Dialogue Setup")]
        public float firstLineDelay;
        public float readLineDelay;
        public float txtSpeed;
        public bool[] dialogueLock;

        [Header("CurrentDialogueState")]
        public DialogueState StartThisDialogue;
        public enum DialogueState { NotYet, Started, sawDestruction, FirstEnemy, topRamp, everything, Kill, end  }

        [Header("text reference")]
        public TextMeshProUGUI txtDialogueBox;

        [Header("Dialogue Lines")]
        #region Dialogues
        public string[] startLines;                 //     0
        public string[] SawDestrutionLines;  //     1
        public string[] FirstEnemiesLines;         //     2
        public string[] TopRampLines;          //     3
        public string[] EverythingIsDestroyedLines;         //     4
        public string[] KillEveryoneLines;           //     5
        public string[] EndingLines;        //     6
        
        #endregion



        [Header("text line Index")]
        #region Indexes
        public int startIndex;
        public int SawIndex;
        public int EnemiesIndex;
        public int RampIndex;
        public int Everything;
        public int KillIndex;
        public int EndIndex;
        #endregion


        private void FixedUpdate()
        {
            switch (StartThisDialogue)
            {
                case DialogueState.NotYet:
                    StartCoroutine(DelayFirstLine(firstLineDelay));
                    break;
                //=============================================Game has Started======================
                case DialogueState.Started:
                    LockAndRead(0, startLines, startIndex);

                    break;

                //=============================================Got to the right Corridor trigger, go defend===
                case DialogueState.sawDestruction:
                    LockAndRead(1, SawDestrutionLines, SawIndex);
                    break;

                //=============================================Used shield, now go shoot======================
                case DialogueState.FirstEnemy:
                    LockAndRead(2, FirstEnemiesLines, EnemiesIndex);
                    break;

                //=============================================Shot all targets, now shoot missile============
                case DialogueState.topRamp:
                    LockAndRead(3, TopRampLines, RampIndex);
                    break;

                //=============================================Exploded the target, now go back================
                case DialogueState.everything:
                    LockAndRead(4, EverythingIsDestroyedLines, Everything);
                    break;

                //=============================================Fuck we are under attack, go to east gate======
                case DialogueState.Kill:
                    LockAndRead(5, KillEveryoneLines, KillIndex);
                    break;

                //=============================================Gate is opening================================
                case DialogueState.end:
                    LockAndRead(6, EndingLines, EndIndex);
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


        public void SwitchState2() => StartThisDialogue = DialogueState.Started;
        public void SwitchState3() => StartThisDialogue = DialogueState.sawDestruction;
        public void SwitchState4() => StartThisDialogue = DialogueState.FirstEnemy;
        public void SwitchState5() => StartThisDialogue = DialogueState.topRamp;
        public void SwitchState6() => StartThisDialogue = DialogueState.everything;
        public void SwitchState7() => StartThisDialogue = DialogueState.Kill;
        public void SwitchState8() => StartThisDialogue = DialogueState.end;
        
    }
}
