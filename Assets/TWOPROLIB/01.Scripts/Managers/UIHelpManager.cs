using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TWOPROLIB.ScriptableObjects;
using UnityEditor;
//using TWOPROLIB.Scripts.Util;
using UnityEngine;

namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// 헬프 상태
    /// </summary>
    public enum HelpState
    {
        /// <summary>
        /// 헬프 보임
        /// </summary>
        Show,
        /// <summary>
        /// 헬프 숨김
        /// </summary>
        Hide
    }

    /// <summary>
    /// 도움말 종류
    /// </summary>
    public enum HelpType
    {
        None,
        Tutorial01,

    }

    /// <summary>
    /// 헬프 키 단위로 묶여 하나의 도우말 묶음 처리
    /// </summary>
    [Serializable]
    public class UIHelpViewMst
    {
        /// <summary>
        /// 게임 상태
        /// </summary>
        [Tooltip("헬프 키")]
        public HelpType helpType;

        /// <summary>
        /// 해당 게임에 보여야 할 오브젝트 리스트
        /// </summary>
        [Tooltip("해당 게임에 보여야 할 오브젝트 리스트")]
        public List<UIHelpViewDetail> HelpView;

    }

    /// <summary>
    /// HelpViewDetail 단위로 도움말을 이어서 출력 해줌
    /// </summary>
    [Serializable]
    public class UIHelpViewDetail
    {
        [Tooltip("설명글")]
        public string title;

        [Tooltip("시간 스케일")]
        public float timeScale;
        [Tooltip("상세 뷰")]
        public List<UIHelpViewDetailStep> HelpViewDetail;

        /// <summary>
        /// 기본적으로 도움말 시 숨김
        /// </summary>
        [Tooltip("기본적으로 도움말 시 숨김")]
        public List<GameObject> defaultHidden;
    }

    /// <summary>
    /// 한번 표현할 도움말
    /// </summary>
    [Serializable]
    public class UIHelpViewDetailStep
    {
        [Tooltip("설명글")]
        public string title;

        [Tooltip("자동 다음 처리")]
        public bool autoNext = false;

        /// <summary>
        /// 보여질 이미지
        /// </summary>
        [Tooltip("보여질 이미지")]
        public List<GameObject> HelpViewObj;


        public TextMeshProUGUI txtTitle;

        public TextMeshProUGUI txtContext;
        
        /// <summary>
        /// 타이틀, 내용을 언어별 등록
        /// </summary>
        [Tooltip("타이틀, 내용을 언어별 등록")]
        public MultiLangHelpContent multiLangHelpContent;

        [Tooltip("현재 시점부터 보여질 게임 오브젝트")]
        public List<GameObject> ViewObj;
    }

    public class UIHelpManager : MonoBehaviour
    {
        /// <summary>
        /// 현재 진행중인 도움말 타입
        /// </summary>
        private HelpType currentHelpType;


        /// <summary>
        /// 보여지 헬프 VIEW
        /// </summary>
        private List<UIHelpViewDetail> currentHelpView;

        /// <summary>
        /// 헬프 단계
        /// </summary>
        private int currentHelpViewMstStep;

        /// <summary>
        /// 헬프 단계(상세)
        /// </summary>
        private int currentHelpViewDetailStep;

        public List<UIHelpViewMst> LsHelpMstView;


        public static UIHelpManager Instance = null;
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            InitHelp();
            AllHelpHide();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void InitHelp()
        {
            currentHelpViewMstStep = 0;
            currentHelpViewDetailStep = 0;
        }

        /// <summary>
        /// 도움말 표현 
        /// </summary>
        /// <param name="helpType">선택한 헬프 타입이 보여짐</param>
        /// <param name="isInit">헬프 step 초기와 유무</param>
        public void SelectHelp(HelpType helpType, bool isInit = true)
        {
            this.currentHelpType = helpType; ;

            // VIEW 선택
            for (int i = 0; i < LsHelpMstView.Count; i++)
            {
                if (LsHelpMstView[i].helpType.Equals(helpType))
                {
                    currentHelpView = LsHelpMstView[i].HelpView;
                }
            }

            // VIEW Step 초기화
            if (isInit)
            {
                InitHelp();
            }

            DefaultHiddenProc(false);

            Invoke("ShowHelpStep", 0.1f);

        }

        /// <summary>
        /// 현재 상태의 헬프를 표현
        /// </summary>
        private void ShowHelpStep()
        {
            //AllHelpHide();

            // 도움말에 기본적으로 숨김 처리하는 오브젝트


            if(currentHelpView[currentHelpViewMstStep].timeScale != -1)
                Time.timeScale = currentHelpView[currentHelpViewMstStep].timeScale;

            // 해당 헬프 문구 관련 오브젝트를 보여주도록 처리
            for (int i = 0; i < currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewObj.Count; i++)
            {
                if(currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewObj[i] != null)
                    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewObj[i].SetActive(true);
            }

            // 해당 헬프 부터 보여질 오브젝트를 처리
            for (int i = 0; i < currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].ViewObj.Count; i++)
            {
                if (currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].ViewObj[i] != null)
                    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].ViewObj[i].SetActive(true);
            }

            // 제목
            if (currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].txtTitle != null)
            {
                currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].txtTitle.text =
                    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].multiLangHelpContent.GetTitle();
            }

            // 내용
            if(currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].txtContext != null)
            {
                currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].txtContext.text =
                    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].multiLangHelpContent.GetContent();
            }


            //if (GameManager.Instance.systemLanguage == SystemLanguage.)
            //{
            //    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewText.text =
            //        GameManager.Instance.systemLanguage.RuntimeValue == (int)SystemLanguage.Korean
            //                ? currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpString_Kor
            //                : currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpString_Eng;
            //}

            //if (currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewText)
            //{
            //    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewText.text =
            //        GameManager.Instance.systemLanguage.RuntimeValue == (int)SystemLanguage.Korean
            //                ? currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpString_Kor
            //                : currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpString_Eng;
            //}

            // 자동 다음 스텝으로 넘김
            // 멘트가 아닌 이미지 뷰, 히든 처리용으로 사용
            if (currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].autoNext)
                NextHelpStep();
        }

        /// <summary>
        /// 헬프 후 다시 플레이
        /// </summary>
        public void NextHelpAfterPlay()
        {
            Time.timeScale = 1;
        }

        public virtual void HelpDone()
        {
            switch(currentHelpType)
            {
                case HelpType.Tutorial01:
                    GameManager.Instance.SetTutorial(false);
                    break;
            }
        }

        private void AllHelpHide()
        {
            for (int i = 0; i < LsHelpMstView.Count; i++)
            {
                for (int j = 0; j < LsHelpMstView[i].HelpView.Count; j++)
                {
                    for (int z = 0; z < LsHelpMstView[i].HelpView[j].HelpViewDetail.Count; z++)
                    {
                        if(LsHelpMstView[i].HelpView[j].HelpViewDetail[z].HelpViewObj != null)
                        {
                            for(int e = 0; e < LsHelpMstView[i].HelpView[j].HelpViewDetail[z].HelpViewObj.Count; e++)
                            {
                                if(LsHelpMstView[i].HelpView[j].HelpViewDetail[z].HelpViewObj[e] != null)
                                    LsHelpMstView[i].HelpView[j].HelpViewDetail[z].HelpViewObj[e].SetActive(false);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 도움말에 기본적으로 숨김 처리하는 오브젝트
        /// </summary>
        /// <param name="b"></param>
        private void DefaultHiddenProc(bool b = false)
        {
            // 도움말에 기본적으로 숨김 처리하는 오브젝트
            for (int i = 0; i < currentHelpView[currentHelpViewMstStep].defaultHidden.Count; i++)
            {
                currentHelpView[currentHelpViewMstStep].defaultHidden[i].SetActive(b);
            }

        }

        public void NextHelpStep()
        {
            // 시작할 가치가 없음
            if (currentHelpView == null || currentHelpView.Count <= currentHelpViewMstStep)
            {
                return;
            }

            if (currentHelpView[currentHelpViewMstStep].HelpViewDetail.Count <= currentHelpViewDetailStep)
            {
                // currentHelpViewDetailStep 스템을 넘기기 이전에 이미 넘어갔으므로
                // currentHelpViewMstStep을 증가해서 처리
                DefaultHiddenProc(true);        // 현 STEP이 종료이므로 현 STEP 의 기본 숨김을 풀어줌
                currentHelpViewMstStep++;
                currentHelpViewDetailStep = 0;

                if (currentHelpView.Count <= currentHelpViewMstStep)
                {
                    // Helpviewstep마져 넘어갔으므로 완전히 도움말이 끝난것임
                    HelpDone();
                    return;
                }
                else
                {
                    DefaultHiddenProc(false);       // 다음 STEP으로 넘어갔으므로 현 STEP의 기본 숨김 처리
                    ShowHelpStep();
                    return;
                }
            }

            // 해당 헬프에서 보여줬던 문구를 다시 숨김
            for(int i = 0; i < currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewObj.Count; i++)
            {
                if(currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewObj[i] != null)
                    currentHelpView[currentHelpViewMstStep].HelpViewDetail[currentHelpViewDetailStep].HelpViewObj[i].SetActive(false);
            }

            currentHelpViewDetailStep++;
            if (currentHelpView[currentHelpViewMstStep].HelpViewDetail.Count > currentHelpViewDetailStep)
            {
                ShowHelpStep();
                return;
            }
            else
            {

                // 상세 스템이 끝났음으로 한타임 쉬고 다시 호출시 메인부분을 변경해서 다시 처리함
                Time.timeScale = 1;

                if (currentHelpView[currentHelpViewMstStep].HelpViewDetail.Count <= currentHelpViewDetailStep + 1 && (currentHelpView.Count <= currentHelpViewMstStep + 1))
                {
                    DefaultHiddenProc(true);        // 현 STEP이 종료이므로 현 STEP 의 기본 숨김을 풀어줌
                    currentHelpViewMstStep++;
                    currentHelpViewDetailStep = 0;
                    HelpDone();
                }
                return;
            }
        }

    }

}
