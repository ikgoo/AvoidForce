using System;
using System.Collections;
using System.Collections.Generic;
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

    [Serializable]
    public class UIHelpView
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
        public List<UIHelpViewDetile> HelpView;
    }

    [Serializable]
    public class UIHelpViewDetile
    {
        public float timeScale;
        public List<GameObject> HelpViewDetile;

        /// <summary>
        /// 기본적으로 도움말 시 숨김
        /// </summary>
        public List<GameObject> defaultHidden;
    }

    public class UIHelpManager : MonoBehaviour
    {

        /// <summary>
        /// 보여지 헬프 VIEW
        /// </summary>
        private List<UIHelpViewDetile> currentHelpView;

        /// <summary>
        /// 헬프 단계
        /// </summary>
        private int currentHelpViewStep;

        /// <summary>
        /// 헬프 단계(상세)
        /// </summary>
        private int currentHelpViewDetileStep;

        public List<UIHelpView> LsHelpView;


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

        // Start is called before the first frame update
        void Start()
        {
            InitHelp();
            AllHelpHide();
        }

        private void InitHelp()
        {
            currentHelpViewStep = 0;
            currentHelpViewDetileStep = 0;
        }

        /// <summary>
        /// 도움말 표현 
        /// </summary>
        /// <param name="helpType"></param>
        public void SelectHelp(HelpType helpType, bool isInit = true)
        {
            // VIEW 선택
            for (int i = 0; i < LsHelpView.Count; i++)
            {
                if (LsHelpView[i].helpType.Equals(helpType))
                {
                    currentHelpView = LsHelpView[i].HelpView;
                }
            }

            // VIEW Step 초기화
            if(isInit)
            {
                InitHelp();
            }

            DefaultHiddenProc(false);

            Invoke("ShowHelpStep", 0.5f);

        }

        /// <summary>
        /// 현재 상태의 헬프를 표현
        /// </summary>
        private void ShowHelpStep()
        {
            //AllHelpHide();

            // 도움말에 기본적으로 숨김 처리하는 오브젝트
            

            Time.timeScale = currentHelpView[currentHelpViewStep].timeScale;
            currentHelpView[currentHelpViewStep].HelpViewDetile[currentHelpViewDetileStep].SetActive(true);
        }

        /// <summary>
        /// 헬프 후 다시 플레이
        /// </summary>
        public void NextHelpAfterPlay()
        {
            Time.timeScale = 1;
        }

        private void AllHelpHide()
        {
            for (int i = 0; i < LsHelpView.Count; i++)
            {
                for (int j = 0; j < LsHelpView[i].HelpView.Count; j++)
                {
                    for(int z = 0; z < LsHelpView[i].HelpView[j].HelpViewDetile.Count; z ++)
                    {
                        LsHelpView[i].HelpView[j].HelpViewDetile[z].SetActive(false);
                    }
                }
            }
        }

        /// <summary>
        /// 도움말에 기본적으로 숨김 처리하는 오브젝트
        /// </summary>
        /// <param name="b"></param>
        private void DefaultHiddenProc(bool b)
        {
            // 도움말에 기본적으로 숨김 처리하는 오브젝트
            for (int i = 0; i < currentHelpView[currentHelpViewStep].defaultHidden.Count; i++)
            {
                currentHelpView[currentHelpViewStep].defaultHidden[i].SetActive(b);
            }

        }

        public void NextHelpStep()
        {
            // 시작할 가치가 없음
            if (currentHelpView == null || currentHelpView.Count <= currentHelpViewStep)
            {
                return;
            }

            if (currentHelpView[currentHelpViewStep].HelpViewDetile.Count <= currentHelpViewDetileStep)
            {
                // currentHelpViewDetileStep 스템을 넘기기 이전에 이미 넘어갔으므로
                // currentHelpViewStep을 증가해서 처리
                DefaultHiddenProc(true);        // 현 STEP이 종료이므로 현 STEP 의 기본 숨김을 풀어줌
                currentHelpViewStep++;
                currentHelpViewDetileStep = 0;

                if (currentHelpView.Count <= currentHelpViewStep)
                {
                    // Helpviewstep마져 넘어갔으므로 완전히 도움말이 끝난것임
                    return;
                }
                else
                {
                    DefaultHiddenProc(false);       // 다음 STEP으로 넘어갔으므로 현 STEP의 기본 숨김 처리
                    ShowHelpStep();
                    return;
                }
            }

            currentHelpView[currentHelpViewStep].HelpViewDetile[currentHelpViewDetileStep].SetActive(false);

            currentHelpViewDetileStep++;
            if (currentHelpView[currentHelpViewStep].HelpViewDetile.Count > currentHelpViewDetileStep)
            {
                ShowHelpStep();
                return;
            }
            else
            {
                // 상세 스템이 끝났음으로 한타임 쉬고 다시 호출시 메인부분을 변경해서 다시 처리함
                Time.timeScale = 1;
                return;
            }
        }

    }

}
