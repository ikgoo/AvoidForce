using System.Collections;
using System.Collections.Generic;
using TMPro;
using TWOPRO.Scripts.Spawners;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TWOPRO.Scripts.Managers
{
    public class UIManager_Game : UIManger
    {
        [Header("==== 추가 변수 ====")]
        public GameObject JoystickObj;
        public GameObject PlayerObj;

        [Header("==== Game Over ====")]

        public IntegerValue currentValue;

        /// <summary>
        /// 게임 오버 창에 표현되는 스코어
        /// </summary>
        public TextMeshProUGUI score;

        /// <summary>
        /// 
        /// </summary>
        public TextMeshProUGUI highScore;

        /// <summary>
        /// 종료 관련 버튼(앞단 처리 후 보이게 처리함)
        /// </summary>
        public List<GameObject> buttons;

        public override void OnGameStateEvent()
        {
            base.OnGameStateEvent();

            switch(GameManager.Instance.gameState.gameState)
            {
                case GameStateType.Running:
                    if(GameManager.Instance.isTutorial.RuntimeValue == false)
                        JoystickObj.SetActive(true);
                    break;

                case GameStateType.Resume:
                    JoystickObj.SetActive(true);
                    break;

                default:
                    JoystickObj.SetActive(false);
                    break;
            }
        }

        public override void OnPlayStateEvnet()
        {
            base.OnPlayStateEvnet();

            switch(GameManager.Instance.playState.playState)
            {
                case PlayStateType.Reward:

                    StartCoroutine(GameOver());
                    JoystickObj.SetActive(false);

                    break;
            }
        }


        IEnumerator GameOver()
        {
            int viewScore = 0;
            float speed = 0.1f;
            float per = 0f;
            int s = 0;

            // 버튼 숨김
            for(int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

            score.text = viewScore.ToString();

            while (true)
            {
                per += Time.deltaTime;
                s = Mathf.FloorToInt((int)Mathf.Lerp((float)viewScore, (float)currentValue.RuntimeValue, per));
                if (s > currentValue.RuntimeValue)
                {
                    s = currentValue.RuntimeValue;
                }
                score.text = s.ToString();

                yield return new WaitForFixedUpdate();

                if(s == currentValue.RuntimeValue)
                {
                    break;
                }
            }

            yield return new WaitForSeconds(0.4f);

            highScore.text = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore").ToString() : "0";
            if(PlayerPrefs.HasKey("HighScore") == false || currentValue.RuntimeValue > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", currentValue.RuntimeValue);
                highScore.text = currentValue.RuntimeValue.ToString();

                highScore.color = Color.red;
            }
            else
            {
                highScore.color = Color.white;
            }

            yield return new WaitForSeconds(0.4f);

            // 버튼 나타남
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(true);
            }


            yield return null;

        }
    }

}
