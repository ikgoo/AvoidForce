using System;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 게임 레벨 디자인
    /// </summary>
    [CreateAssetMenu(menuName = "GameManager/GameLevel")]
    [Serializable]
    public class GameLevel : ScriptableObject
    {
        #region Game Gata : Start ===========================
        /// <summary>
        /// 레벨
        /// </summary>
        [Tooltip("레벨 정보")]
        public int level;

        /// <summary>
        /// 미션시간
        /// </summary>
        [Tooltip("목표 클리어 시간")]
        public float clearGoalTime;

        /// <summary>
        /// 목표 클리어 점수
        /// </summary>
        [Tooltip("목표 클리어 점수")]
        public int clearGoalScore;

        #endregion Game Gata : End ===========================

        #region Player Data :  Start =========================

        [Header("[Player Data]")]
        public float playerSpeed = 0;
        public float playerAngleSpeed = 0;

        #endregion Player Data :  End ========================

        #region Enemy Data : Start ===========================

        [Header("[Enemy Data]")]
        [SerializeField]
        public int enemyCount;
                
        // 5f~10f
        public float enemySpeed;

        #endregion Enemy Data : End ==========================

    }
}
