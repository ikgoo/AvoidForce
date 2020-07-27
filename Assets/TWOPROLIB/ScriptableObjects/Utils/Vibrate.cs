using UnityEngine;
using System.Collections;
using System;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 진동 설정
    /// </summary>
    [CreateAssetMenu(menuName = "Utils/Vibrate")]
    [Serializable]
    public class Vibrate : ScriptableObject
    {
        AndroidJavaClass unityPlayer = null;
        AndroidJavaObject currentActivity = null;
        AndroidJavaObject sysService = null;

        /// <summary>
        /// 진동 길이
        /// </summary>
        [Tooltip("진동 길이")]
        public long milliseconds;

        private void OnEnable()
        {
            if(unityPlayer == null)
            {
                //unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                //currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                //sysService = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            }
        }


        //Functions from https://developer.android.com/reference/android/os/Vibrator.html
        public void vibrate()
        {
            sysService.Call("vibrate");
        }


        public void vibrate(long milliseconds)
        {
            sysService.Call("vibrate", milliseconds);
        }

        public void vibrate(long[] pattern, int repeat)
        {
            sysService.Call("vibrate", pattern, repeat);
        }


        public void cancel()
        {
            sysService.Call("cancel");
        }

        public bool hasVibrator()
        {
            return sysService.Call<bool>("hasVibrator");
        }
    }
}
