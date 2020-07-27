using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=pV7kx4NQvmo&list=PLkdmC1lUA21kIv7ghnHtOq8iVg2YRiOrf&index=4
namespace TWOPROLIB.Scripts.Managers
{

    [RequireComponent(typeof(GamePrefabPoolManager), typeof(GameUIManager), typeof(UIManger))]
    [SerializeField]
    public class GameManager : MonoBehaviour
    {

        /// <summary>
        /// 2D, 3D 상태 표시
        /// </summary>
        [Tooltip("2D, 3D 상태 표시")]
        public GameDisplayMode gameDisplayMode;

        public string Input_Horizontal = "Horizontal";

        /// <summary>
        /// 입력 방식
        /// </summary>
        [Tooltip("입력방식")]
        [SerializeField] public InputType inputType;

        /// <summary>
        /// 게임 플레이를 바로 시작하고 싶을때 선택
        /// 디버그용 
        /// </summary>
        [Tooltip("바로 플레이용")]
        public bool autoPlay = false;

        [Tooltip("게임 상태 정보")]
        [SerializeField] public GameState gameState;
        [Tooltip("플레이 상태 정보")]
        [SerializeField] public PlayState playState;
        [Tooltip("게임 룰")]
        [SerializeField] public GameRuleBase gameRule;

        /// <summary>
        /// 게임 데이터
        /// </summary>
        [Tooltip("게임 데이터")]
        public GameData gameData;
        [Tooltip("게임 옵션 정보")]
        [SerializeField] public GameOption gameOption;
        [Tooltip("게임 상태 변경에 따른 이벤트 처리용")]
        [SerializeField] public GameEvent GameStateEvent;

        /// <summary>
        /// 게임 레벨을 가져올 폴더명
        /// </summary>
        [Tooltip("게임 레벨을 가져올 폴더명")]
        [SerializeField] public string gameLevelFolder = "GameLevel";

        /// <summary>
        /// 게임 레벨 List
        /// </summary>
        public GameLevel[] lsGameLevel;
        [Tooltip("플레이어 리스트")]
        [SerializeField] public List<StateController> lsPlayer;


        /// <summary>
        /// 시작 시 Game State
        /// </summary>
        [Tooltip("시작 시 Game State")]
        public GameStateType InitGameStateType;

        /// <summary>
        /// 튜토리얼 상태
        /// </summary>
        [Tooltip("튜토리얼 상태")]
        public BooleanValue isTutorial;

        /// <summary>
        /// 게임 내 UI 관리자
        /// </summary>
        [Tooltip("게임 내 UI 관리자")]
        public GameUIManager gameUIManager;

        public static GameManager Instance = null;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);

            #region Get All Resource

            if(gameLevelFolder != "")
            {
                // Game Level Data
                lsGameLevel = Resources.LoadAll<GameLevel>("ScriptableObjects/GameManager/"+ gameLevelFolder);
            }

            #endregion

            gameUIManager = GetComponent<GameUIManager>();

            // 게임 옵션 초기화
            gameOption.Init();

            // 상태 초기화
            if(GameManager.Instance.playState != null)
            {
                GameManager.Instance.playState.playState = PlayStateType.None;
            }
        }

        private void Start()
        {
            // 새로운 GameManager의 경우 상태를 초기화 처리
            gameState.gameState = GameStateType.None;

            if (autoPlay)
            {
                GameManager.Instance.ChangeGameState(ScriptableObjects.GameStateType.Init);
                // GAMEPLAY Scene의 경우 무조건 GameManager실행하도록 함
                GameManager.Instance.ChangeGameState(InitGameStateType);
            }

            //AudioManager.Instance.PlayMusicTrack(0, true, true);
        }

        private void Update()
        {
            for (int i = 0; i < lsPlayer.Count; i++)
            {
                if(lsPlayer[i] != null)
                    lsPlayer[i].StateUpdate(Time.deltaTime);
            }
            //playerState.speed = gameLV.playerSpeed;
        }

        private void FixedUpdate()
        {
            if (Instance.playState == null)
            {
                return;
            }

            for (int i = 0; i < lsPlayer.Count; i++)
            {
                if (lsPlayer[i] != null)
                    lsPlayer[i].StateFixedUpdate(Time.deltaTime);
            }

            switch (GameManager.Instance.playState.playState)
            {
                case PlayStateType.Play:
                    if(gameRule != null)
                    {
                        gameRule.OnFixedUpdate(this);
                    }
                    break;
            }
        }

        #region Change Game State ===========================================

        /// <summary>
        /// Game Manager 최초에 적용된 상태로 전환
        /// </summary>
        public virtual void StartGameState()
        {
            // GAMEPLAY Scene의 경우 무조건 GameManager실행하도록 함
            GameManager.Instance.ChangeGameState(InitGameStateType);
        }

        /// <summary>
        /// Scene 초기화
        /// </summary>
        public virtual void GameInit()
        {
            // 카메라 위치 등등 게임 화면 보이기 전에 처리해줘야 하는 것들 처리
            //GameInitend();

            if (!autoPlay)
                Invoke("GameInitend", 0f);
        }

        public virtual void GameInitend()
        {
            ScreenManager.Instance.NextProcSceneLoaded();
        }

        /// <summary>
        /// Scene 시작
        /// </summary>
        public virtual void GameMainMenu()
        {
            
        }

        /// <summary>
        /// 게임 시작
        /// </summary>
        public virtual void GameStart()
        {
            GameManager.Instance.ChangeGameState(GameStateType.Running);
        }

        /// <summary>
        /// 게임중
        /// </summary>
        public virtual void GameRunning()
        {
            // play 상태를 초기화로 전환
            GameManager.Instance.ChangePlayState(PlayStateType.Init);
            
        }

        public virtual void GamePause()
        {
            
        }

        public virtual void GameShop()
        {

        }

        public virtual void GameResume()
        {
            
        }

        public virtual void GameNextTurn()
        {
            //gameLV = lsGameLevel[gameLV.level++];
        }

        /// <summary>
        /// 게임 종료 처리
        /// </summary>
        public virtual void GameEnd()
        {

        }

        public virtual void GameReward()
        {

        }

        /// <summary>
        /// 게임 상태 변경
        /// </summary>
        /// <param name="gameeState"></param>
        public void ChangeGameState(GameStateType gameState, float waitTime = 0f)
        {
            if (this.gameState.gameState != gameState)
            {
                this.gameState.gameState = gameState;

                // Game State에 따라 UI 변경 처리
                UIManger.Instance.ViewGameStateUI();

                // 상태에 대한 처리
                switch (gameState)
                {
                    case GameStateType.Init:
                        //Invoke("GameInit", waitTime);
                        GameInit();
                        break;

                    case GameStateType.MainMenu:
                        Invoke("GameMainMenu", waitTime);
                        break;

                    case GameStateType.Start:
                        Invoke("GameStart", waitTime);
                        break;

                    case GameStateType.Running:
                        Invoke("GameRunning", waitTime);
                        break;

                    case GameStateType.Pause:
                        Invoke("GamePause", waitTime);
                        break;

                    case GameStateType.Resume:
                        Invoke("GameResume", waitTime);
                        break;

                    case GameStateType.Shop:
                        Invoke("GameShop", waitTime);
                        break;

                    case GameStateType.End:
                        Invoke("GameEnd", waitTime);
                        break;
                }

                // 이벤트 처리
                GameStateEvent.Raise();
            }
        }

        #endregion

        #region Change Play State ===================================

        public void PlayInit()
        {
            // 초기 레벨
            gameData.InitGameData(lsGameLevel[gameData.GetCurrentLevel()]);

            ChangePlayState(PlayStateType.Play);

            //timeOut = gameLV.timeOut;
            //goalScore = gameLV.goalScore;
            //ScreenManager.Instance.FadeAction(FadeType.FADEIN, () =>
            //{
            //    //lsPlayers[0].transform.position = startPlayerPosition;
            //    //ball.transform.position = startBallPosition;
            //    //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //    //isLive = true;

            //    //gameLV.LoadObstacle();

            //    ScreenManager.Instance.FadeAction(FadeType.FADEOUT, () =>
            //    {
            //        ChangePlayState(PlayStateType.Play);
            //    });

            //});

        }

        public virtual void PlayPreView()
        {

        }

        public virtual void PlayPlay()
        {

        }

        public virtual void PlayEnd()
        {
            // 플레이가 종료 되면 profabpool gameobject를 회수함
            PrefabPoolManager.Instance.AllDestroy();
            GamePrefabPoolManager.Instance.AllDestroy();

            // 플레이어가 죽었는지 분기를 하여 처리
            //DebugX.Log(isLive.ToString());
            GameManager.Instance.ChangePlayState(PlayStateType.Reward);
        }

        public virtual void PlayReward()
        {
            //timeOut = 0;
            if(gameData.isLive)
            {
                ChangePlayState(PlayStateType.NextTurn);
            }
            else
            {
                ChangeGameState(GameStateType.End);
            }


        }

        public virtual void PlayNextTurn()
        {
            gameData.NextLevel(1);
            //if (isLive)
            //{
            //    // 다음 게임 상태로 넘김
            //    gameLV = lsGameLevel[gameLV.level];
            //    ChangePlayState(PlayStateType.Init);
            //}
            //else
            //{
            //    // 초기 화면으로 이동
            //    GameManager.Instance.ChangeGameState(GameStateType.MainMenu);
            //}
        }


        public void ChangePlayState(PlayStateType playState, float waitTime = 0f)
        {
            if (this.playState != null && this.playState.playState != playState)
            {
                this.playState.playState = playState;

                // 상태에 대한 처리
                switch (this.playState.playState)
                {
                    case PlayStateType.Init:
                        Invoke("PlayInit", waitTime);
                        break;

                    case PlayStateType.PreView:
                        Invoke("PlayPreView", waitTime);
                        break;

                    case PlayStateType.Play:
                        Invoke("PlayPlay", waitTime);
                        break;

                    case PlayStateType.End:
                        Invoke("PlayEnd", waitTime);
                        break;

                    case PlayStateType.Reward:
                        Invoke("PlayReward", waitTime);
                        break;

                    case PlayStateType.NextTurn:
                        Invoke("PlayNextTurn", waitTime);
                        break;
                }
            }
        }
        #endregion

        public void InvorkByGameState()
        {
            DebugX.Log(this.gameState);
        }

        public void NextScene()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        #region Public Method : Start ================================
        /// <summary>
        /// 클리어 스코어 추가 처리
        /// </summary>
        /// <param name="addClearScore"></param>
        public virtual void AddClearScore(int addClearScore)
        {
            GameManager.Instance.gameData.AddClearScore(addClearScore);
            if (GameManager.Instance.IsGameClear())
            {
                ChangePlayState(PlayStateType.End);
            }
        }

        /// <summary>
        /// 게임 클리여 여부 체크
        /// </summary>
        /// <returns>true : 클리어, false : 미클리어</returns>
        public virtual bool IsGameClear()
        {
            //지정된 만큼 맞췄다.
            if (lsGameLevel[gameData.GetCurrentLevel()].clearGoalScore <= gameData.currentClearScore)
            {
                GameManager.Instance.gameData.isGameClear = true;
                return true;
            }
            else
                return false;

        }


        /// <summary>
        /// 플레이어가 죽었을 경우 or, 플레이어가 졌을 경우
        /// </summary>
        /// <param name="state">죽은 플레이어 정보</param>
        public virtual void PlayerDeath(StateController state)
        {

            //PrefabPoolManager.Instance.AllDestroy("Enemy", false);
            //PrefabPoolManager.Instance.AllDestroy("Skull", false);

            //리더보드에 점수 갱신
            //Social.ReportScore(PlayerPrefs.GetInt("highScore", 0), GPGSIds.leaderboard_rank, null);

            // 시간 지연 후 상태 전환
            //Invoke("SetGameStateNone", 0.01f);

            gameData.isLive = false;

            DebugX.Log("플레이어 죽음");

            GameObject obj = GamePrefabPoolManager.Instance.GetObjectForTypeWithPoolDestroy(state.destroyGameObject.name, false, true, 3, false, null, 0, false, false);
            obj.transform.position = state.gameObject.transform.GetChild(0).transform.position;
            obj.SetActive(true);

            // 죽음 처리
            state.gameObject.SetActive(false);


            ChangePlayState(PlayStateType.End, 2);
        }

        /// <summary>
        /// 광고를 보여주게 할 처리함수
        /// </summary>
        public virtual void SetAdMobCalculation()
        {
            //if (adMob != null)
            //{
            //    //특정조건이 되면 값을 올려준다.
            //    adMobTemp++;
            //    if (adMobCount == adMobTemp)
            //    {
            //        adMobTemp = 0;             //초기화
            //                                   //광고를 보여준다.
            //        adMob.ShowInterstitial();
            //    }
            //    else
            //    {
            //        //죽을때마다 보상광고
            //        DebugX.Log("보상광고");
            //        // adMob.ShowRewardedAd();
            //    }
            //}
        }

        /// <summary>
        /// 진동 울림
        /// </summary>
        public void RunVibrate()
        {
            if (GameManager.Instance.gameOption.isVibrate)
            {
#if UNITY_ANDROID
                Handheld.Vibrate();
#elif UNITY_IOS
                // ios 부분 추가되어야 함
#endif
            }

        }

        public void debug1()
        {
            DebugX.Log("test");
        }
        #endregion Public Method : End ===============================


    }

   
}
