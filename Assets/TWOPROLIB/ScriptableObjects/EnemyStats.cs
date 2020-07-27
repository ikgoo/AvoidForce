using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        /// <summary>
        /// 원으로 된 광선의 지름
        /// </summary>
        public float lookSphereCastRadius;
        /// <summary>
        /// 적을 감지 할 거리
        /// </summary>
        public float lookRange;
        /// <summary>
        /// 공격 거리
        /// </summary>
        public float attackRange;
        /// <summary>
        /// 적 검색 시간
        /// </summary>
        public float searchDuration;
    }
}
