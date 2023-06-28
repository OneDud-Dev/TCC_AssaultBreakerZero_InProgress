using UnityEngine.Audio;
using UnityEngine;
using System;

namespace ABZ_GameSystems
{
    public class Game_AudioController : MonoBehaviour
    {
        public Game_Sounds[] sounds;
        
        public AudioSource clip;


        private void Awake()
        {
            foreach (var item in sounds)
            {
                item.source = item.thisSouce;
                item.source.clip = item.clip;
                item.source.volume = item.volume;
                item.source.loop = item.loops;
            }
        }



        public void Play( string name)
        {
       
            Game_Sounds sound = Array.Find(sounds, _sound => _sound.soundName == name);

            if (sound == null)
            {
                Debug.Log("Sound error, maybe Wrong Name");
                return;
            }
            sound.source.Play();
        }

        public void Stop( string name )
        {
            Game_Sounds sound = Array.Find(sounds, _sound => _sound.soundName == name);
            sound.source.Stop();
        }
        public void StopOST(string name)
        {
            Game_Sounds sound = Array.Find(sounds, _sound => _sound.soundName == name);
            sound.source.volume = Mathf.Lerp(sound.volume, 0, 1);
            sound.source.Stop();
        }


    }
}
