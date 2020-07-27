using System.Collections;
using System.Collections.Generic;
using TWOPRO.Scripts.Managers;
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
        /// 플레이서 상태 변경 시 발생하는 이벤트
        /// </summary>
        [Tooltip("플레이서 상태 변경 시 발생하는 이벤트")]
        public GameEvent ChgPlayerStateEvent;

        public string colorName = "";

        #region UNITY METHOD : Start =================================================================
        protected override void Start()
        {
            base.Start();

            UpdateHUD();
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
            else if(targetObject.tag.Equals("Enemy"))
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

        /// <summary>
        /// 실드 체크
        /// </summary>
        public void UpdateHUD()
        {
            shieldObj.SetActive(isShield.RuntimeValue);
            ChgPlayerStateEvent.Raise();
        }

        public override void Interactable(string tag, Entity interactable, int amount, GameObject gameobj)
        {
            base.Interactable(tag, interactable, amount, gameobj);

            switch (interactable.EntityType)
            {
                case EntityTypes.Projectile:        // 충동체

                    if(interactable is FireBall)
                    {
                        // 충돌이면 무조건 점수
                        Score.RuntimeValue++;

                        Color color;
                        if (tag.Equals("PlayerSide"))
                        {
                            if (gameobj.name.Contains("Blue"))
                            {
                                color = Color.blue;
                                CheckCombo("Blue");
                            }
                            else if (gameobj.name.Contains("Red"))
                            {
                                color = Color.red;
                            }
                            else if (gameobj.name.Contains("Green"))
                            {
                                color = Color.green;
                                CheckCombo("Green");
                            }
                            else
                            {
                                // pink
                                color = Color.HSVToRGB(199, 21, 133);
                                CheckCombo("Pink");
                            }

                            ChangePlayerColor(color);
                        }
                        else if(tag.Equals("Player"))
                        {
                            DebugX.Log(this.tag + " 데미지 받음");

                            color = Color.white;
                            ChangePlayerColor(color);

                            comboCount.RuntimeValue= 0;
                            stats.hp -= ((FireBall)interactable).damage;
                        }

                        UpdateHUD();
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

        void CheckCombo(string colorName)
        {
            if (this.colorName.Equals(colorName))
            {
                DebugX.Log(this.tag + " 콤보 적용");
                comboCount.RuntimeValue++;
            }
            else
                comboCount.RuntimeValue = 0;

            this.colorName = colorName;
        }

        #endregion Private Methods : End ====================================================

    }
}
