using System;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 게임 룰 디자인
    /// </summary>
    [Serializable]
    public abstract class GameRuleBase : ScriptableObject
    {
        /// <summary>
        /// 업데이트
        /// </summary>
        public virtual void OnUpdate(GameManager gameManager)
        {

        }

        /// <summary>
        /// 픽스드 업데이트
        /// </summary>
        public virtual void OnFixedUpdate(GameManager gameManager)
        {

        }

        /// <summary>
        /// 커스텀 업데이트
        /// </summary>
        public virtual void OnCustomUpdate(GameManager gameManager)
        {

        }

    }
}