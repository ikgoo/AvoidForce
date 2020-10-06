/**
 * Title : 게임 HUD 컨트롤
 * Desc :
 * 게임 플레이 중에 해당하는 UI를 대상으로 활성화 유무를 컨트롤
 **/
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLib.Scripts.Managers
{
    public class HUDManager : MonoBehaviour
    {
        /// <summary>
        /// HUD에 해당하는 게임 오브젝트 리스트
        /// </summary>
        [Tooltip("HUD에 해당하는 게임 오브젝트 리스트")]
        public List<GameObject> LsHUDRootObject;

        #region Public Methods : Start ====================================================

        /// <summary>
        /// HUD 상태 변경 시 호출
        /// </summary>
        /// <param name="state"></param>
        public virtual void HUDChange(StateController state)
        {
            
        }

        /// <summary>
        /// Game State 변경 시 이벤트
        /// </summary>
        public virtual void OnGameStateEvnet()
        {
            switch (GameManager.Instance.gameState.gameState)
            {
                case GameStateType.Init:
                case GameStateType.Start:
                    ViewHUDObject(false);
                    break;
            }
        }

        /// <summary>
        /// Play State 변경 시 이벤트
        /// </summary>
        public virtual void OnPlayStateEvnet()
        {
            switch (GameManager.Instance.playState.playState)
            {
                case PlayStateType.Init:
                    ViewHUDObject(true);
                    break;

                case PlayStateType.Reward:
                    ViewHUDObject(false);
                    break;
            }
        }

        /// <summary>
        /// HUD Show 유무
        /// </summary>
        /// <param name="isShow"></param>
        void ViewHUDObject(bool isShow)
        {
            for(int i = 0; i < LsHUDRootObject.Count; i++)
            {
                LsHUDRootObject[i].SetActive(isShow);
            }
        }

        #endregion Public Methods : End ==================================================
    }

}
