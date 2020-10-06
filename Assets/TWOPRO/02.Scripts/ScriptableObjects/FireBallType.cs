using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Spawners
{
    [CreateAssetMenu(menuName = "Spawn/FireBallType")]
    public class FireBallType : ScriptableObject
    {
        /// <summary>
        /// 불꽃 이름
        /// </summary>
        public string FireName;

        /// <summary>
        /// 불꽃 색상
        /// </summary>
        [Tooltip("불꽃 색상")]
        public Color FireBallColor;

        /// <summary>
        /// 불꽃에 사용할 색
        /// </summary>
        [Tooltip("불꽃에 사용할 색")]
        public Gradient[] FireBallGrandient;

    }

}
