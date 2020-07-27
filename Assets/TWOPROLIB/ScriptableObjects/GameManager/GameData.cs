using System;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 게임 데이터
    /// </summary>
    [CreateAssetMenu(menuName = "GameManager/GameData")]
    [Serializable]
    public class GameData : ScriptableObject
    {
        /// <summary>
        /// 현재 레벨
        /// </summary>
        [Tooltip("현재 레벨")]
        int currentLevel = 0;

        //현재 점수
        /// <summary>
        /// 현재 점수 - 여러 스테이지의 경우 누적됨
        /// </summary>
        [Tooltip("현재 점수")]
        public int currentScore;

        //최고 점수
        [Tooltip("최고 점수")]
        public int highScore;

        //버티는 시간
        public float time;
        //최고 시간
        public float highTime;

        public bool useClearTime = false;

        /// <summary>
        /// 목표 클리어 시간 - 0이되면 게임 오버가 됨
        /// </summary>
        [Tooltip("목표 클리어 시간 - 0이되면 게임 오버가 됨")]
        public float currenClearTime;

        /// <summary>
        /// 현재 스케이지 점수 - 다음 스테이지에 초기화
        /// </summary>
        [Tooltip("현재 맞춘 오브젝트 수")]
        public int currentClearScore;


        /// <summary>
        /// 게임 클리어 여부
        /// </summary>
        [Tooltip("게임 클리어 여부")]
        public bool isGameClear = false;

        /// <summary>
        /// 생존여부
        /// </summary>
        [Tooltip("생존 여부")]
        public bool isLive = false;

        private void OnEnable()
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        }

        /// <summary>
        /// 현재 게임 레벨 반환
        /// </summary>
        /// <returns>리턴 레벨 값</returns>
        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        //public void SetCurrentLevel(int lv)
        //{

        //}

        /// <summary>
        /// 다음 레벨로 이동
        /// </summary>
        /// <param name="addLV"></param>
        public void NextLevel(int addLV)
        {
            currentLevel += addLV;
        }

        /// <summary>
        /// GameLevel중 필요한 데이터를 바인딩 함
        /// </summary>
        /// <param name="gameLV"></param>
        public void InitGameData(GameLevel gameLV)
        {
            this.isGameClear = false;
            this.currentClearScore = 0;
            this.currenClearTime = gameLV.clearGoalTime;

            UpdateUI();
        }

        /// <summary>
        /// 게임 플레이시 초기화 부분
        /// </summary>
        public void GamePlayInit()
        {
            this.isGameClear = false;
            this.isLive = true;
        }

        /// <summary>
        /// 미션 성공 시 처리
        /// </summary>
        public void GameStageClear()
        {
            this.isGameClear = true;
        }

        /// <summary>
        /// 미선 실패 시 처리
        /// </summary>
        public void GameStageFail()
        {
            this.isGameClear = false;
        }

        /// <summary>
        /// 클리어 점수 추가
        /// </summary>
        /// <param name="addClearCount"></param>
        public void AddClearScore(int addClearCount, bool isAddScore = false, int addScore = 0)
        {
            this.currentClearScore += addClearCount;
            if (isAddScore)
                AddScore(addScore);
            else
            {
                // UI 업데이트
                UpdateUI();
            }
        }

        /// <summary>
        /// 스코어 추가
        /// </summary>
        /// <param name="addScore">추가할 스코어</param>
        /// <param name="udpHighScore">하이스코어 업데이트 유무</param>
        public void AddScore(int addScore, bool udpHighScore = false)
        {
            currentScore += addScore;
            if (udpHighScore)
                SetHighScore();

            // UI 업데이트
            UpdateUI();
        }

        /// <summary>
        /// 하이 스코어 세팅
        /// </summary>
        public void SetHighScore()
        {
            if(currentScore > highScore)
            {
                highScore = currentScore;
                SaveHighScoreData();
            }
        }

        /// <summary>
        /// UI업데이트
        /// </summary>
        void UpdateUI()
        {
            UIManger.Instance.UpdateUI();
        }

        public void LoadHighScoreData()
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }

        public void SaveHighScoreData()
        {
            PlayerPrefs.SetInt("highScore", highScore);
        }


    }
}
