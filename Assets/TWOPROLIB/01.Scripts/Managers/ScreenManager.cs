using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TWOPROLIB.Scripts.Managers
{
    public class ScreenManager : MonoBehaviour
    {
        /// <summary>
        /// 관리 Scene List
        /// </summary>
        public enum ScenePage
        {
            NONE        = -1,      // NONE
            INTRO       = 0,       // INTRO
            LOADING     = 1,       // LOADING
            MENU        = 2,       // MENU
            GAMEPLAY    = 3,       // GAMEPLAY
            ADSSCENE    = 4,       // ADSSCENE

            OPTION      = 3,
            GAMEEND     = 4
        }

        /// <summary>
        /// Fade 이미지 캔버스
        /// </summary>
        [Tooltip("Fade 이미지 캔버스")]
        public Canvas canvas;

        /// <summary>
        /// Fade 이미지
        /// </summary>
        [Tooltip("Fade 이미지")]
        [SerializeField]
        public Image img;

        /// <summary>
        /// Fade 유무
        /// </summary>
        bool isFadeIn = false;
        /// <summary>
        /// Fade IN/OUT 속도
        /// </summary>
        [Range(0f, 1f)]
        [Tooltip("Fade IN/OUT 속도")]
        public float fadeSpeed = 0.05f;

        /// <summary>
        /// 시작 Scene
        /// </summary>
        [Tooltip("시작 Scene")]
        public ScenePage startScenePage = ScenePage.MENU;

        /// <summary>
        /// 현재 Scene
        /// </summary>
        ScenePage _currentScenePage;
        /// <summary>
        /// 현재 Scene
        /// </summary>
        public ScenePage CurrentScenePage
        {
            get { return _currentScenePage; }
        }
        /// <summary>
        /// 다음 Scene
        /// </summary>
        ScenePage _nextScenePage;

        public static ScreenManager Instance = null;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            _currentScenePage = ScenePage.NONE;
        }

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            _currentScenePage = startScenePage;
            //if (_currentScenePage == ScenePage.GAMEPLAY)
            //{
            //    // GAMEPLAY Scene의 경우 무조건 GameManager실행하도록 함
            //    GameManager.Instance.ChangeGameState(ScriptableObjects.GameStateType.Init);
            //    GameManager.Instance.ChangeGameState(ScriptableObjects.GameStateType.MainMenu);
            //}

        }

        /// <summary>
        /// 페이드 처리 유무
        /// </summary>
        public bool isFade = true;

        /// <summary>
        /// Scene 전환
        /// </summary>
        /// <param name="_nextScenePage">다음 Scene</param>
        public void ChangeScene(ScenePage _nextScenePage, bool isFade = true)
        {
            this.isFade = isFade;

            Time.timeScale = 0;
            this._nextScenePage = _nextScenePage;

            if (this.isFade)
            {
                // 숨기고
                AudioManager.Instance.CrossFadeMusicTrack();    // 음약 서서히 소거
                FadeAction(FadeType.FADEIN, LoadScene);
                //StartCoroutine("RunFadeAction", FadeType.FADEIN);
            }
            else
            {
                LoadScene();
            }
        }

        /// <summary>
        /// 다음 Scene 로드
        /// </summary>
        public void LoadScene()
        {
            SceneManager.LoadScene((int)_nextScenePage);
        }

        /// <summary>
        /// 다음 Scene 로드 완료
        /// </summary>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            // 게임 매니져가 있으면 초기화 진행
            if (GameManager.Instance != null)
            {
                GameManager.Instance.autoPlay = false;
                GameManager.Instance.GameInit();
            }
            else
            {
                // GameManager가 없으면 바로 다음로직 처리
                NextProcSceneLoaded();
            }

        }

        /// <summary>
        /// Scene Loaded 처리 후 콜백용
        /// </summary>
        public void NextProcSceneLoaded(bool? isFadeLoc = true)
        {
            // 화면 전환 후 최초 Game 상태로 변환(페이드 아웃 이전에 처리)
            GameManager.Instance.StartGameState();
            //GameManager.Instance.GameStart();

            DebugX.Log("NextProcSceneLoaded");
            if (isFadeLoc == true && this.isFade)
            {
                FadeAction(FadeType.FADEOUT, LoadedScene);
                //StartCoroutine("RunFadeAction", FadeType.FADEOUT);
            }
            else
                LoadedScene();
        }

        /// <summary>
        /// 로드 Scene 시작
        /// </summary>
        public void LoadedScene()
        {
            _currentScenePage = _nextScenePage;     // Scene 상태 전환
            Time.timeScale = 1;
        }


        Action CBFunction;
        /// <summary>
        /// Fade IN/OUT 처리
        /// </summary>
        /// <param name="fadeType"></param>
        /// <param name="CBFunction"></param>
        public void FadeAction(FadeType fadeType, Action CBFunction)
        {
            this.CBFunction = CBFunction;
            StartCoroutine("RunFadeAction", fadeType);
        }


        /// <summary>
        /// Fade IN/OUT 처리 코루틴
        /// 경고 : 직접 호출 하면 안됨
        /// </summary>
        /// <param name="fadeType"></param>
        /// <returns></returns>
        IEnumerator RunFadeAction(FadeType fadeType)
        {
            float currentTime = Time.realtimeSinceStartup;
            Color c;
            switch (fadeType)
            {
                case FadeType.FADEIN:
                    canvas.gameObject.SetActive(true);
                    for (float f = 0f; f <= 1f; f += fadeSpeed * (Time.realtimeSinceStartup - currentTime))
                    {
                        c = img.color;
                        c.a = f;
                        img.color = c;
                        yield return null;
                    }
                    c = img.color;
                    c.a = 1f;
                    img.color = c;
                    isFadeIn = true;
                    CBFunction();
                    break;

                case FadeType.FADEOUT:
                    for (float f1 = 1f; f1 >= 0f; f1 -= fadeSpeed * (Time.realtimeSinceStartup - currentTime))
                    {
                        c = img.color;
                        c.a = f1;
                        img.color = c;
                        yield return null;
                    }
                    c = img.color;
                    c.a = 0f;
                    img.color = c;
                    canvas.gameObject.SetActive(false);
                    isFadeIn = false;
                    CBFunction();
                    break;

                case FadeType.FADEINOUT:
                    // fade in
                    canvas.gameObject.SetActive(true);
                    for (float f = 0f; f <= 1f; f += fadeSpeed * (Time.realtimeSinceStartup - currentTime))
                    {
                        c = img.color;
                        c.a = f;
                        img.color = c;
                        yield return null;
                    }
                    c = img.color;
                    c.a = 1f;
                    img.color = c;
                    isFadeIn = true;

                    // fade out
                    for (float f1 = 1f; f1 >= 0f; f1 -= fadeSpeed * (Time.realtimeSinceStartup - currentTime))
                    {
                        c = img.color;
                        c.a = f1;
                        img.color = c;
                        yield return null;
                    }
                    c = img.color;
                    c.a = 0f;
                    img.color = c;
                    canvas.gameObject.SetActive(false);
                    isFadeIn = false;
                    CBFunction();
                    break;

                case FadeType.FillCHANGEIN:
                    //alpha = 0.0f;
                    img.fillAmount = 0.0f;
                    canvas.gameObject.SetActive(true);
                    while (true)
                    {
                        img.fillAmount += (fadeSpeed/2);
                        //alpha -= fillAmountValue;
                        //img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a);
                        if (img.fillAmount >= 1)
                        {
                            img.fillAmount = 1;
                            //alpha = 1;
                            CBFunction();
                            break;
                        }
                        yield return null;
                    }
                    break;

                case FadeType.FillCHANGEOUT:
                    //alpha = 1.0f;
                    img.fillAmount = 1.0f;
                    canvas.gameObject.SetActive(true);
                    while (true)
                    {
                        img.fillAmount -= (fadeSpeed/2);
                        //alpha -= fillAmountValue;
                        //img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a);
                        if (img.fillAmount <= 0)
                        {
                            img.fillAmount = 0;
                            //alpha = 0;
                            CBFunction();
                            canvas.gameObject.SetActive(false);
                            break;
                        }
                        yield return null;
                    }
                    break;
            }
        }


        /// <summary>
        /// Scene 이동
        /// </summary>
        /// <param name="sceneIdx"></param>
        public void OnChangeScene(int sceneIdx)
        {
            ChangeScene((ScenePage)sceneIdx);
        }

    }


    /// <summary>
    /// Fade 타입
    /// </summary>
    public enum FadeType
    {
        FADEIN,
        FADEOUT,
        FADEINOUT,
        FillCHANGEIN,
        FillCHANGEOUT,
    }

}
