using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Options;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "GameManager/GameOption")]
    public class GameOption : ScriptableObject
    {
        [SerializeField]
        public AudioData audioData;

        //진동 유무 변수
        /// <summary>
        /// 진동 유무
        /// </summary>
        [Tooltip("진동 유무")]
        public bool isVibrate = true;

        public void Init()
        {
            // 오디오 초기화
            audioData.InitAudio();

            // 오디오 이외의 옵션은 이곳에서 초기화 하고 관리함
            isVibrate = Convert.ToBoolean(PlayerPrefs.GetInt("isVibrate", 1));
        }

        //public float musicVol;

        #region Public Methods
        /// <summary>
        /// 배경음악 볼룸 가져오기
        /// </summary>
        /// <returns></returns>
        public float GetMusic()
        {
            return audioData.volumes[0].volume;
        }

        /// <summary>
        /// 배경음악 볼륨 저장
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusic(float val)
        {
            audioData.volumes[0].volume = val;
            if(AudioManager.Instance != null)
                AudioManager.Instance.ChangeVolume(0);
        }

        /// <summary>
        /// 효과음 볼륨 가져오기
        /// </summary>
        /// <returns></returns>
        public float GetEffect()
        {
            return audioData.volumes[1].volume;
        }

        /// <summary>
        /// 효과음 볼륨 셋업
        /// </summary>
        /// <param name="volume"></param>
        public void SetEffect(float val)
        {
            audioData.volumes[1].volume = val;
            if(AudioManager.Instance != null)
                AudioManager.Instance.ChangeVolume(1);
        }

        /// <summary>
        /// 진동 가져오기
        /// </summary>
        /// <returns></returns>
        public bool GetVibrate()
        {
            return isVibrate;
        }

        /// <summary>
        /// 진동 셋업
        /// </summary>
        /// <param name="isVibrate"></param>
        public void SetVibrate(bool isVibrate)
        {
            PlayerPrefs.SetInt("isVibrate", Convert.ToInt32(isVibrate));
            this.isVibrate = isVibrate;
        }

        #endregion
    }
}
