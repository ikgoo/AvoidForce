using System.Collections;
using System.Collections.Generic;
using TWOPRO.Scripts.Spawners;
using TWOPROLib.Scripts.Managers;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace TWOPRO.Scripts.Managers
{
    [RequireComponent(typeof(GamePrefabPoolManager), typeof(HUDManager), typeof(UIManger))]
    [SerializeField]
    public class GameManager_Game : GameManager
    {
        /// <summary>
        /// 게임 속도 없을 위한 카운트
        /// </summary>
        float currentFireSpeedUp = 0f;

        /// <summary>
        /// 5초마다 스피트 업
        /// </summary>
        float maxFireSpeedUp = 5f;

        /// <summary>
        /// 마스터 스포너
        /// </summary>
        [Tooltip("마스터 스포너")]
        public SpawnerMaster spawnerMaster;

        public override void Update()
        {
            base.Update();

            switch(playState.playState)
            {
                case PlayStateType.Play:
                    
                    if(!isTutorial.RuntimeValue)
                    {
                        // 불꽃 속도 제어
                        currentFireSpeedUp += Time.deltaTime;
                        if(currentFireSpeedUp >= maxFireSpeedUp)
                        {
                            currentFireSpeedUp = 0f;
                            spawnerMaster.AddFireSpeed();
                        }

                    }

                    break;
            }

        }

        public override void GameStart()
        {
            base.GameStart();

            //int i = 0;
        }

        public override void GameOption()
        {
            Time.timeScale = 0;
            base.GameOption();

        }

        public override void GameResume()
        {
            //base.GameResume();
            //StartCoroutine(GameResumeWaitRun(1f));

            Time.timeScale = 1;
            AudioManager.Instance.MusicTrackPause(false);
        }

        IEnumerator GameResumeWaitRun(float wait = 0f)
        {
            //yield return new WaitForSeconds(wait);
            yield return new WaitForSeconds(.1f);

            Time.timeScale = 1;
            AudioManager.Instance.MusicTrackPause(false);

            yield return null;
        }

        /// <summary>
        /// Scene이 모두 로드 되고 게임 로직이 실행 가능 한 상태
        /// </summary>
        public override void GameRunning()
        {
            base.GameRunning();

            AudioManager.Instance.PlayMusicTrack(1);

            if(spawnerMaster != null)
            {
                spawnerMaster.StartSapwnMaster();
            }

        }

        public override void PlayInit()
        {
            base.PlayInit();

            currentFireSpeedUp = 0;
        }

        public override void PlayPlay()
        {
            if (isTutorial.RuntimeValue)
            {
                UIHelpManager.Instance.SelectHelp(HelpType.Tutorial01);
            }
        }


        public override void PlayEnd()
        {
            base.PlayEnd();

            if (spawnerMaster != null)
            {
                spawnerMaster.EndSapwnMaster();
            }
        }

        public override void PlayerDeath(StateController state)
        {
            base.PlayerDeath(state);
        }

    }
}
