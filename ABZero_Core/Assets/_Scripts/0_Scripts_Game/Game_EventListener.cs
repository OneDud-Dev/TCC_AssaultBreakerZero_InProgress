using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ABZ_GameSystems
{
    [System.Serializable]
    public class EventWithSender: UnityEvent<Component, object> { }
    


    public class Game_EventListener : MonoBehaviour
    {
        public Game_Events        gameEvent;
        public UnityEvent         u_EventResponse;
        public EventWithSender    s_EventResponse;
        

        public void OnEventRaised()
            { u_EventResponse?.Invoke(); }

        public void OnEventRaised(Component _sender, object _data)
            { s_EventResponse?.Invoke(_sender, _data);  }

    
        private void OnEnable() => gameEvent.RegisterListener(this);
        private void OnDisable() => gameEvent.UnRegisterListener(this);
    
    }
}
