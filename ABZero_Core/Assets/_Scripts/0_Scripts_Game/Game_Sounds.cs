using UnityEngine.Audio;
using UnityEngine;

namespace ABZ_GameSystems
{
    [System.Serializable]
    public class Game_Sounds
    {
        public string soundName;
        public AudioSource thisSouce;

        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        public bool loops;

        [HideInInspector]
        public AudioSource source;
    }
}
