using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Managers
{
    [RequireComponent(typeof(GamePrefabPoolManager), typeof(UIManger))]
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

