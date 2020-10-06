using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TWOPRO.Scripts.Managers
{
    [RequireComponent(typeof(GamePrefabPoolManager), typeof(UIManger))]
    [SerializeField]
    public class GameManager_Intro : GameManager
    {
        /// <summary>
        /// Scene이 모두 로드 되고 게임 로직이 실행 가능 한 상태
        /// </summary>
        public override void GameRunning()
        {
            base.GameRunning();

            AudioManager.Instance.PlayMusicTrack(0);

            StartCoroutine("InitIntro");

        }

        /// <summary>
        /// 인트로 중 초기화 처리 
        /// 배경 버벅임을 박기 위해 코루틴 사용
        /// </summary>
        /// <returns></returns>
        IEnumerator InitIntro()
        {
            // 튜토리얼 정보 세팅
            GetTutorial();

#if UNITY_EDITOR
            DebugX.Log("Unity Editor 상태");
            yield return new WaitForSeconds(1.3f);
#else
            // 인터넷 확인


            // 구글 로그인

#endif

            yield return new WaitForSeconds(1.3f);

            // 다음 신으로 이동(메뉴)
            ScreenManager.Instance.ChangeScene(ScreenManager.ScenePage.MENU);

        }
    }
}
