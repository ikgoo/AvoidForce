using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 게임 상태
    /// </summary>
    [CreateAssetMenu(menuName = "GameManager/GameState")]
    [Serializable]
    public class GameState : ScriptableObject
    {
        [SerializeField]
        public GameStateType gameState;

        private void Awake()
        {
            gameState = GameStateType.None;
        }

    }

    [Serializable]
    public enum GameStateType
    {
        None            =  0,               // 상태 없음
        Loading         =  1,               // 로딩중
        Init            =  2,               // 초기화
        MainMenu        =  3,               // 메인 메뉴 화면
        Ready           =  4,               // 게임 준비 상태
        Start           =  5,               // 게임 시작
        Running         =  6,               // 게임중
        Pause           =  7,               // 일시 정지
        Shop            =  8,               // 샵화면
        Resume          =  9,               // 재시작
        NextTurn        = 10,               // 다음 턴
        End             = 11,               // 게임 종료
        Reward          = 12,               // 보상 화면
        Option          = 13,               // 옵션 화면
        LeaderBoard     = 14,               // 리더 보드
        Rank            = 15,               // 랭크
    }
}
