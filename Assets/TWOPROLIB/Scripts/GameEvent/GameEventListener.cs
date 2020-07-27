using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace TWOPROLIB.Scripts
{
    public class GameEventListener : MonoBehaviour
    {
        /// <summary>
        /// 사용 할 이벤트 이름
        /// </summary>
        public string EvnetName;

        /// <summary>
        /// 발생 이벤트
        /// </summary>
        public GameEvent Event;

        /// <summary>
        /// 이빈테 발생 시 호출 할 이벤트
        /// </summary>
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UngisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }

    }
}
