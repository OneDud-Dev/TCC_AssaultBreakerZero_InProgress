using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ABZCore
{
    [System.Serializable]
    public class CustomGameEvent: UnityEvent<Component, object> { }



    public class Game_EventListener : MonoBehaviour
    {
        public Game_Events      gameEvent;
        public CustomGameEvent  eventResponse;



        private void OnEnable() => gameEvent.RegisterListener(this);
        private void OnDisable() => gameEvent.UnRegisterListener(this);
    
        public void OnEventRaised(Component _sender, object _data)
            {   eventResponse?.Invoke(_sender, _data);  }
    }
}
