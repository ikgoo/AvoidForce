using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [System.Serializable]
    public class Transition
    {
        /// <summary>
        /// 특정 조건
        /// </summary>
        public Decision decision;
        /// <summary>
        /// 조건 만족 시 실행
        /// </summary>
        public State trueState;
        /// <summary>
        /// 조건 불만족 시 실행
        /// </summary>
        public State falseState;
    }
}

