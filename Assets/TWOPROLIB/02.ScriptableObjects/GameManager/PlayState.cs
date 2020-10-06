using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 플레이 상태
    /// </summary>
    [CreateAssetMenu(menuName = "GameManager/PlayState")]
    [Serializable]
    public class PlayState : ScriptableObject
    {
        [SerializeField]
        public PlayStateType playState;

        private void Awake()
        {
            playState = PlayStateType.None;
        }
    }

    public enum PlayStateType
    {
        None        = 0,               // 상태 없음
        Init        = 1,               // 게임 시작을 했을대 초기값 
        PreView     = 2,                // 초기 값 기준 화면 출력
        Play        = 3,               // 게임 플레이
        End         = 4,                // 게임 종료
        Reward      = 5,                // 현재 게임 결과 처리
        NextTurn    = 6,            // 다음 턴(GameStat를 바꿈)
    }
}
