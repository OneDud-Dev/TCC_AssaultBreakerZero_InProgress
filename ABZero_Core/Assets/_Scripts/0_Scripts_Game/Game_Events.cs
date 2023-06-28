using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABZ_GameSystems
{
    [CreateAssetMenu(menuName = "NewGameEvent")]
    public class Game_Events : ScriptableObject
    {
        
        public List<Game_EventListener> eventListeners = new List<Game_EventListener>();

        public void Raise()
        {
            for (int i = 0; i < eventListeners.Count; i++)
            { eventListeners[i].OnEventRaised(); }
        }
        
        public void Raise(Component _sender, object _data)
        {
            for (int i = 0; i < eventListeners.Count; i++)
            { eventListeners[i].OnEventRaised(_sender, _data);}
        }

       

        #region Listener registration

        public void RegisterListener(Game_EventListener _listener)
        {
            if (!eventListeners.Contains(_listener))
                {eventListeners.Add(_listener);}
        }

        public void UnRegisterListener(Game_EventListener _listener)
        {
            if (eventListeners.Contains(_listener))
                {eventListeners.Remove(_listener);}
        }


        #endregion
    }
}
