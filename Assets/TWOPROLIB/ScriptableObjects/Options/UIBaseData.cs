using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects.Options
{
    [CreateAssetMenu(fileName = "Options", menuName = "Options/AudioData")]
    public class UIBaseData : Entity
    {
        /// <summary>
        /// PlayerPrefs에 저장 할지 여부
        /// </summary>
        [Tooltip("PlayerPrefs에 저장 할지 여부")]
        public bool usePlayerPrefs;

        /// <summary>
        /// 초기 볼룸(usePlayerPrefs가 true일 경우 사용 하지 않음)
        /// </summary>
        [Tooltip("초기 세팅 값)")]
        public string defaultValue;

        /// <summary>
        /// 볼룸 리스트
        /// </summary>
        [Tooltip("UI 출력 데이터")]
        [Range(0f, 1f)]
        public string uival;

        /// <summary>
        /// 초기 볼룸 설정
        /// </summary>
        public void InitAudio()
        {
            if (usePlayerPrefs)
                uival = PlayerPrefs.GetString(Name.ToString(), "");
            else
                uival = defaultValue;             // usePlayerPrefs가 false일 경우 기본 값으로 최초 세팅
        }

        /// <summary>
        /// 볼룸 저장
        /// </summary>
        public void SaveAudio()
        {
            if (usePlayerPrefs)
            {
                PlayerPrefs.SetString(Name, uival);
            }
        }

        /// <summary>
        /// 값 가져오기
        /// </summary>
        /// <param name="val"></param>
        public void SetVolume(string val)
        {
            uival = val;
        }

        /// <summary>
        /// 값 불러오기
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public string GetValue()
        {
            return uival;
        }
    }
}
