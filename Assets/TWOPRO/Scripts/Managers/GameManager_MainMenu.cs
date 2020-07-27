using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Managers
{
    [RequireComponent(typeof(GamePrefabPoolManager), typeof(GameUIManager), typeof(UIManger))]
    [SerializeField]
    public class GameManager_MainMenu : GameManager
    {
        public override void GameMainMenu()
        {
            base.GameMainMenu();

            AudioManager.Instance.PlayMusicTrack(0);
        }

    }
}

