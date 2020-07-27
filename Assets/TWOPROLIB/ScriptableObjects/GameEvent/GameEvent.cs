using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "GameEvent/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [Multiline][Tooltip("간단한 설명")]
        public string desc;

        private List<GameEventListener> listeners = new List<GameEventListener>();

        /// <summary>
        /// 이벤트 발생하여 각 리스너에게 이벤트 전달
        /// </summary>
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UngisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
