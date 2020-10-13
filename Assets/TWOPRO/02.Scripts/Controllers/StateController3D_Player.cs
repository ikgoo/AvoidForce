using System.Collections;
using System.Collections.Generic;
using TWOPRO.Scripts.Managers;
using TWOPRO.Scripts.Spawners;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.TWOPRO
{
    
    public class StateController3D_Player : StateController3D
    {
        /// <summary>
        /// 파티클 변수
        /// </summary>
        [Tooltip(tooltip: "파티클 변수")]
        public ParticleSystem[] ps = new ParticleSystem[2];

        /// <summary>
        /// 실드 유무
        /// </summary>
        [Tooltip("실드 유무")]
        public BooleanValue isShield;

        /// <summary>
        /// 콤보 카운트
        /// </summary>
        [Tooltip("콤보 카운트")]
        public IntegerValue comboCount;

        /// <summary>
        /// 점수
        /// </summary>
        [Tooltip("점수")]
        public IntegerValue Score;

        /// <summary>
        /// 실드 게임오브젝트
        /// </summary>
        [Tooltip(tooltip: "실드 게임오브젝트")]
        public GameObject shieldObj;

        /// <summary>
        /// 플레이어 색상
        /// </summary>
        [Tooltip("플레이어 색상")]
        public FireBallType playerColor;

        /// <summary>
        /// 능력이 적용될 콤보 카운트
        /// </summary>
        [Tooltip("능력이 적용될 콤보 카운트")]
        public int ApplyAbilityComboCount = 5;

        /// <summary>
        /// 플레이어가 데미지를 받았을때 이벤트
        /// </summary>
        [Tooltip("플레이어가 데미지를 받았을때 이벤트")]
        public GameEvent PlayerDamageEvent;

        #region UNITY METHOD : Start =================================================================
        protected override void Start()
        {
            base.Start();

            CallPlayerStateEvent();
        }

        public override void StateUpdate(float deltaTime)
        {
            base.StateUpdate(deltaTime);
        }

        public override void StateFixedUpdate(float deltaTime)
        {
            base.StateFixedUpdate(deltaTime);
        }

        public override void OnStateTriggerEnter(GameObject childGameObject, GameObject targetObject)
        {
            //other = transform.GetChild(0).GetComponent<Collider>();
            if (targetObject.tag == "Laser")
            {
                // 레이저 충돌 (즉사)
            }
            else if (targetObject.tag.Equals("Enemy"))
            {
                // 적 충돌 체 (데미지)
            }

            DebugX.Log("OnStateTriggerEnter");
        }

        public override void OnStateTriggerExit(GameObject childGameObject, GameObject targetObject)
        {
            DebugX.Log("OnStateTriggerExit");
        }

        public override void OnStateTriggerStay(GameObject childGameObject, GameObject targetObject)
        {
            DebugX.Log("OnStateTriggerStay");
        }

        #endregion UNITY METHOD : End ================================================================


        #region Overload Methods : Start ==============================================================

        public override void Interactable(string tag, Entity interactable, int amount, GameObject gameobj)
        {
            base.Interactable(tag, interactable, amount, gameobj);

            switch (interactable.EntityType)
            {
                case EntityTypes.Projectile:        // 충동체

                    if (interactable is FireBall)
                    {
                        // 충돌이면 무조건 점수
                        Score.RuntimeValue++;

                        if (tag.Equals("PlayerSide"))
                        {
                            bool b = false;
                            FireBallType fbt = gameobj.GetComponent<ChangeFireBallColor>().fireBallType;
                            b = CheckCombo(fbt);


                            // 콤보 처리
                            if (b)
                            {
                                comboCount.RuntimeValue++;

                                // 콤보가 2회 이상일 경우 한번당 1점씩 추가 점수
                                Score.RuntimeValue += 1;

                                // 5 콤보 이상 시 능력 제공
                                if (comboCount.RuntimeValue >= ApplyAbilityComboCount)
                                {
                                    ApplyAbility();
                                }
                            }
                            else
                            {
                                comboCount.RuntimeValue = 1;
                            }


                            ChangePlayerColor(fbt.FireBallColor);

                            if (GameManager_Game.Instance.isTutorial.RuntimeValue)
                            {
                                UIHelpManager.Instance.NextHelpPassExit();
                            }

                        }
                        else if (tag.Equals("Player"))
                        {
                            DebugX.Log(this.tag + " 데미지 받음");

                            ChangePlayerColor(Color.white);

                            // 콤보 초기화
                            comboCount.RuntimeValue = 0;

                            if(isShield.RuntimeValue == true)
                            {
                                ChangeShild(false);
                            }
                            else
                            {
                                if (stats.hp < ((FireBall)interactable).damage)
                                    stats.hp = 0;
                                else
                                    stats.hp -= ((FireBall)interactable).damage;
                            }
                        }

                        CallPlayerStateEvent();
                        PlayerDamageEvent.Raise();
                    }


                    if (stats.hp == 0 && this.tag.Equals("Player"))
                    {
                        GameManager.Instance.PlayerDeath(this);
                    }

                    break;
            }
        }

        #endregion Overload Methods : End =============================================================


        #region Public Mehtods : Start ===============================================================

        /// <summary>
        /// 플레이어 사망
        /// </summary>
        public void PlayerDeath()
        {
            GameManager.Instance.PlayerDeath(this);
        }

        /// <summary>
        /// 플레이서 색상 변경
        /// </summary>
        /// <param name="color"></param>
        public void ChangePlayerColor(Color color)
        {
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 0), new GradientColorKey(color, 1) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0), new GradientAlphaKey(1.0f, 0.532f), new GradientAlphaKey(0, 1) });
            var col = ps[0].colorOverLifetime;
            col.color = grad;

            grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 0), new GradientColorKey(color, 1) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0), new GradientAlphaKey(1.0f, 0.532f), new GradientAlphaKey(0, 1) });
            col = ps[1].colorOverLifetime;
            col.color = grad;
        }

        #endregion Public Mehtods : End ==============================================================

        #region Private Methods : Start ======================================================

        /// <summary>
        /// 실드 상태 변경 처리
        /// </summary>
        /// <param name="b"></param>
        void ChangeShild(bool b)
        {
            isShield.RuntimeValue = b;
            shieldObj.SetActive(b);
        }

        /// <summary>
        /// 콤보 인지 확인
        /// </summary>
        /// <param name="colorName"></param>
        bool CheckCombo(FireBallType fbt)
        {
            bool b = false; ;
            if (this.playerColor != null && fbt.FireBallColor.Equals(this.playerColor.FireBallColor))
            {
                DebugX.Log(this.tag + " 콤보 적용");
                b = true;
            }
            else
            {
                comboCount.RuntimeValue = 1;
                b = false;
            }

            this.playerColor = fbt;

            return b;
        }

        /// <summary>
        /// 콤보에 따른 능력 제공
        /// </summary>
        /// <param name="colorName"></param>
        void ApplyAbility()
        {
            switch (playerColor.FireName)
            {
                case "Blue":
                    // 실드 생성
                    if(comboCount.RuntimeValue == 5)
                    {
                        isShield.RuntimeValue = true;
                        shieldObj.SetActive(true);
                    }

                    break;
                case "Green":
                    // 현재 불꽃들은 제거 및 보너스 점수 10점
                    if (comboCount.RuntimeValue == 5)
                    {
                        Score.RuntimeValue += 10;
                        GamePrefabPoolManager.Instance.AllDestroy();
                    }
                    break;
                case "Red":
                    // ApplyAbilityComboCount의 배수마다 추가 점수 10점(ApplyAbilityComboCount가 5일 경우 5, 10, 15....)
                    if (comboCount.RuntimeValue % ApplyAbilityComboCount == 0)
                        Score.RuntimeValue += 10;
                    break;
                case "Pink":
                default:
                    // 불꽃의 속도를 느리게 해줌(최조 속도가 정해저 있음, 그 이하로는 내려가지 않음)
                    if (comboCount.RuntimeValue % ApplyAbilityComboCount == 0)
                    {
                        ((GameManager_Game)GameManager_Game.Instance).spawnerMaster.SubFireSpeed();
                        DebugX.Log("속도 느려짐");
                    }
                    break;
            }
        }

        #endregion Private Methods : End ====================================================

    }
}
