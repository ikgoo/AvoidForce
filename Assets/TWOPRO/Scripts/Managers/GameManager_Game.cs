using System.Collections;
using System.Collections.Generic;
using TWOPRO.Scripts.Spawners;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Managers
{
    [RequireComponent(typeof(GamePrefabPoolManager), typeof(GameUIManager), typeof(UIManger))]
    [SerializeField]
    public class GameManager_Game : GameManager
    {
        /// <summary>
        /// 마스터 스포너
        /// </summary>
        [Tooltip("마스터 스포너")]
        public SpawnerMaster spawnerMaster;

        public override void GameStart()
        {
            base.GameStart();

            //int i = 0;
        }

        /// <summary>
        /// Scene이 모두 로드 되고 게임 로직이 실행 가능 한 상태
        /// </summary>
        public override void GameRunning()
        {
            base.GameRunning();

            AudioManager.Instance.PlayMusicTrack(0);

            if(spawnerMaster != null)
            {
                spawnerMaster.StartSapwnMaster();
            }

            //StartCoroutine("InitIntro");

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

            if (spawnerMaster != null)
            {
                spawnerMaster.EndSapwnMaster();
            }

        }
    }
}
