using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

public class BtnMenuScene : MonoBehaviour
{
    public void BtnClick_Menu()
    {
        ScreenManager.Instance.ChangeScene(ScreenManager.ScenePage.MENU);
    }
}
