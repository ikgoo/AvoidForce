using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 행동 관련
    /// </summary>
    public abstract class Action : ScriptableObject
    {
        /// <summary>
        /// 액션 명
        /// </summary>
        [Tooltip("액션명")]
        public string Name;

        /// <summary>
        /// 액션 설명
        /// </summary>
        [Tooltip("액션 설명")]
        [TextArea]
        public string Description;


        /// <summary>
        /// 액션 실행 할 대상
        /// </summary>
        /// <param name="controller"></param>
        public abstract void Act(StateController controller);
    }
}
