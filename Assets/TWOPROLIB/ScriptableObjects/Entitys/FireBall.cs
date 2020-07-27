using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 적이 발사는 발사체
    /// </summary>
    [CreateAssetMenu(menuName = "Stats/Entity/Projectile")]
    public class FireBall : Entity
    {
        [Header("--- 개 별 ---")]

        /// <summary>
        /// 데미지
        /// </summary>
        [Tooltip("데미지")]
        public int damage = 1;
    }
}
