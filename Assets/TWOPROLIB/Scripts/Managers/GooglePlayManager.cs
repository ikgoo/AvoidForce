using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayManager : MonoBehaviour
{
    public Text LogText;
    void Start()
    {
        try
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
        catch(Exception err)
        {
            //int i = 0;
        }

    }

    public void LogIn()
    {
        LogText.text = "구글 로그인 시도";
        try
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success) LogText.text = Social.localUser.id + " \n " + Social.localUser.userName;
                else LogText.text = "구글 로그인 실패";
            });
        } catch(Exception err) {
            LogText.text = "로그인 오류";
        }
    }

    public void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        LogText.text = "구글 로그아웃";
    }

    void Update()
    {
        
    }
}
