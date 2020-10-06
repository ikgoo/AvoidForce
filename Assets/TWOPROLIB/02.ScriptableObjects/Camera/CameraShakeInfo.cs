using System;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Camera/Shake")]
    public class CameraShakeInfo : ScriptableObject
    {
        /// <summary>
        /// 흔들림 크기
        /// </summary>
        [Tooltip("흔들림 크기")]
        public float shakeAmount;

        /// <summary>
        /// 흔들림 시간
        /// </summary>
        [Tooltip("흔들림 시간")]
        public float shakeTime;

    }
}
