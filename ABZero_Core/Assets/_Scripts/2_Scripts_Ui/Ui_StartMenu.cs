using UnityEngine;
using UnityEngine.SceneManagement;
using ABZ_GameSystems;

namespace ABZ_Ui
{
    public class Ui_StartMenu : MonoBehaviour
    {
        public Game_AudioController _audio;

        public Canvas Cvs_Splash;
        public Canvas Cvs_Start;
        public Canvas Cvs_Options;
        public Canvas Cvs_Campaing;
        public Canvas Cvs_ChapterSelect;


        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }


        #region Screen Changers
        public void Bton_Splash()
        {
            _audio.Play("Clicked");
            TurnOffCanvas(Cvs_Splash);
            TurnOnCanvas(Cvs_Start);
        }
        public void Bton_Campaing()
        {
            _audio.Play("Clicked");
            TurnOffCanvas(Cvs_Start);
            TurnOnCanvas(Cvs_Campaing);
        }
        public void Bton_Campaing_B()
        {
            _audio.Play("Back");

            TurnOffCanvas(Cvs_Campaing);
            TurnOnCanvas(Cvs_Start);
        }
        public void Bton_Options()
        {
            _audio.Play("Back");

            TurnOffCanvas(Cvs_Start);
            TurnOnCanvas(Cvs_Options);
        }
        public void Bton_Options_B()
        {

            _audio.Play("Back");
            TurnOffCanvas(Cvs_Options);
            TurnOnCanvas(Cvs_Start);
        }
        public void Bton_Chapters()
        {
            _audio.Play("Clicked");
            TurnOffCanvas(Cvs_Campaing);
            TurnOnCanvas(Cvs_ChapterSelect);
        }
        public void Bton_Chapters_B()
        {
            _audio.Play("Back");
            TurnOffCanvas(Cvs_ChapterSelect);
            TurnOnCanvas(Cvs_Campaing);
        }

        public void Bton_Exit()
        {
            Application.Quit();
        }

        #endregion

        #region Socials

        public void Btn_linkedin()
        {

        }
        public void Btn_Twitter()
        {

        }
        public void Btn_GitHub()
        {

        }

        #endregion

        #region Option Controls
        public void SetAudioMaster()
        {

        }
        public void SetAudioMusic()
        {

        }
        public void SetAudioSfx()
        {

        }
        public void SetResolution()
        {

        }
        public void SetFOV()
        {

        }

        #endregion

        #region ChapterSelect
        public void LoadLvl1()
        {
            SceneManager.LoadScene("02_MissionBriefingStart");
        }
        public void LoadLvl2()
        {
            SceneManager.LoadScene("");
        }
        
        #endregion
    

        private void TurnOnCanvas(Canvas cvs) => cvs.gameObject.SetActive(true);
        private void TurnOffCanvas(Canvas cvs) => cvs.gameObject.SetActive(false);
    
    
    
    
    
    
    }
}
