//using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 구글 플레이 연동
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

namespace TWOPROLIB.Scripts.Managers
{
    public class GooglePlayManager : MonoBehaviour
    {
        bool bWait = false;

        public static GooglePlayManager Instance = null;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);

            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

        }

        public void OnLogin()
        {
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate((bool bSuccess) =>
                {
                    if (bSuccess)
                    {
                        Debug.Log("Success : " + Social.localUser.userName);
                    }
                    else
                    {
                        Debug.Log("Fall");
                    }
                });
            }
        }

        public void OnLogOut()
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        }
    }
}
