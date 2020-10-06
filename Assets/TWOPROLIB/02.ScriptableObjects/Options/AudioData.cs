using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;


namespace TWOPROLIB.ScriptableObjects.Options
{
    [Serializable]
    public class AudioDataInfo
    {
        /// <summary>
        /// 오디오 타이틀
        /// </summary>
        [Tooltip("오디오 타이틀")]
        public string name;
        /// <summary>
        /// 오디오 볼륨
        /// </summary>
        [Range(0f, 1f)]
        [Tooltip("오디오 볼륨")]
        public float volume;
    }

    /// <summary>
    /// 오디오 관련 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "Options", menuName = "Options/AudioData")]
    public class AudioData : Entity
    {
        /// <summary>
        /// PlayerPrefs에 저장 할지 여부
        /// </summary>
        [Tooltip("PlayerPrefs에 저장 할지 여부")]
        public bool usePlayerPrefs;

        /// <summary>
        /// 초기 볼룸(usePlayerPrefs가 true일 경우 사용 하지 않음)
        /// </summary>
        [Tooltip("초기 볼룸(usePlayerPrefs가 true일 경우 사용 하지 않음)")]
        public float defaultVolume;

        /// <summary>
        /// 볼룸 리스트
        /// </summary>
        [Tooltip("볼룸 리스트")]
        [SerializeField]
        public List<AudioDataInfo> volumes;

        /// <summary>
        /// 초기 볼룸 설정
        /// </summary>
        public void InitAudio()
        {
            if (usePlayerPrefs)
            {
                for (int i = 0; i < volumes.Count; i++)
                {
                    volumes[i].volume = PlayerPrefs.GetFloat((this.Name + volumes[i].name), 1f);
                }
            }
            else
            {
                for (int i = 0; i < volumes.Count; i++)
                {
                    volumes[i].volume = defaultVolume;             // usePlayerPrefs가 false일 경우 기본 값으로 최초 세팅
                }
            }
        }

        /// <summary>
        /// 볼룸 저장
        /// </summary>
        public void SaveAudio()
        {
            if (usePlayerPrefs)
            {
                for (int i = 0; i < volumes.Count; i++)
                {
                    PlayerPrefs.SetFloat(Name + volumes[i].name, volumes[i].volume);
                }
            }
        }

        /// <summary>
        /// 볼룸 값 가져오기
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="volume"></param>
        public void SetVolume(string name, float volume)
        {
            for (int i = 0; i < volumes.Count; i++)
            {
                if(volumes[i].name.Equals(name))
                {
                    volumes[i].volume = volume;
                }
            }
                
        }

        /// <summary>
        /// 볼룸 값 불러오기
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public float GetValue(string name)
        {
            for (int i = 0; i < volumes.Count; i++)
            {
                if (volumes[i].name.Equals(name))
                {
                    return volumes[i].volume;
                }
            }
            return 0f;
        }
    }
}
