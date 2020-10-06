/**
 * Title : 게임 HUD 컨트롤
 * Desc :
 * 게임 플레이 중에 해당하는 UI를 대상으로 활성화 유무를 컨트롤
 **/
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using TWOPRO.Scripts.TWOPRO;
using TWOPROLib.Scripts.Managers;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TWOPRO.Scripts.Managers
{
    public class HUDManager_Game : HUDManager
    {
        public BooleanValue isShield;


        public GameObject ShieldObj;

        public List<Image> LsHPObjs;

        public TextMeshProUGUI txtComboCount;

        public IntegerValue Score;

        public TextMeshProUGUI txtScore;

        public override void HUDChange(StateController state)
        {
            if (state is StateController3D_Player)
            {
                ShieldObj.SetActive(((StateController3D_Player)state).isShield.RuntimeValue);

                if (LsHPObjs.Count > state.stats.hp)
                {
                    for (int i = 0; state.stats.hp < i; i++)
                    {
                        LsHPObjs[i].color = Color.red;
                    }

                    for (int i = state.stats.hp; i < LsHPObjs.Count; i++)
                    {
                        LsHPObjs[i].color = Color.white;
                    }
                }

                txtComboCount.text = ((StateController3D_Player)state).comboCount.RuntimeValue.ToString();
                txtScore.text = Score.RuntimeValue.ToString();
            }

        }
    }

}
