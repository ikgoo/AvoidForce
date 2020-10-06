using System.Collections.Generic;
using System.Linq;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Interactables;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Spawners
{
    public class SpawnerMaster : SpawnManager
    {
        #region Declaration : Start ============================================

        /// <summary>
        /// 불꽃의 속도
        /// </summary>
        [Tooltip("불꽃의 속도")]
        public FloatValue FireSpeed;

        /// <summary>
        /// 스포너 실행중 유무
        /// </summary>
        public bool isRun = false;

        /// <summary>
        /// 다른 스포너 리스트 등록 - 없는 경우는 리스트가 있는 스포너가 컨트롤 함
        /// </summary>
        [Tooltip("다른 스포너 리스트 등록 - 없는 경우는 리스트가 있는 스포너가 컨트롤 함 / 경고 : 마스터에게만 넣으세요.")]
        public List<Spawner_FireBall> OtherSapwner_FireBall;

        /// <summary>
        /// 불꽃 종류 리스트
        /// </summary>
        [Tooltip("불꽃 종류 리스트")]
        public List<FireBallType> LsFireBallType;

        #endregion Declaration : End ===========================================

        #region Unity Methods : Start =============================================
        protected override void Awake()
        {
            if (LsEntity.Count >= 1)
            {
                SelectedEntityIndex = 0;
            }


            if (LsFireBallType.Count >= 1)
            {
                SelectedSpawnIndex = 0;
            }

        }

        protected override void Start()
        {
            base.Start();
            for (int i = 0; i < OtherSapwner_FireBall.Count; i++)
            {
                OtherSapwner_FireBall[i].InitSapwner(this);
            }
        }

        protected override void FixedUpdate()
        {
            if (OtherSapwner_FireBall.Count == 0)
            {
                DebugX.Log("스포너가 업습니다.");
                return;
            }

            if (isRun && isEnable && GameManager.Instance.playState.playState == PlayStateType.Play)
            {
                // true가 넘어오면 서브 스포너 하나 이상이 종료된것 이므로 전체가 종료 되었는지 확인해보고 모두 종료되었다면 룰 선택 로직으로 이동
                bool rtnChk = false;
                
                // 시간 누적
                for (int i = 0; i < OtherSapwner_FireBall.Count; i++)
                {
                    rtnChk = OtherSapwner_FireBall[i].CheckSapwn(Time.deltaTime);
                }

                if(rtnChk)
                {
                    if(OtherSapwner_FireBall.Count() == OtherSapwner_FireBall.Where(r => r.isDone == true).Count())
                    {
                        // 모든 서브 스포너가 종료 되었으므로 룰 선택으로 이동 함
                        SelectRule();
                    }
                }

            }

        }
        #endregion Unity Methods : End =============================================


        #region Public Methods : Start ==============================================================

        /// <summary>
        /// 불꽃 속도 조절 메소드(증가)
        /// </summary>
        /// <param name="s"></param>
        public void AddFireSpeed(float s = 0.2f)
        {
            FireSpeed.RuntimeValue += s;
        }

        /// <summary>
        /// 불꽃 속도 조절 메소드(감소)
        /// </summary>
        /// <param name="s"></param>
        public void SubFireSpeed(float s = 0.2f)
        {
            if(FireSpeed.RuntimeValue > 0)
                FireSpeed.RuntimeValue -= s;

            if (FireSpeed.RuntimeValue < 0)
                FireSpeed.RuntimeValue = 0;
        }

        /// <summary>
        /// 마스터 스포너 시작
        /// </summary>
        public void StartSapwnMaster()
        {
            if (OtherSapwner_FireBall.Count == 0)
            {
                DebugX.Log("스포너가 없습니다.");
                return;
            }

            isRun = true;
            SelectRule();
        }

        public void EndSapwnMaster()
        {
            isRun = false;
        }

        /// <summary>
        /// 룰 선택
        /// </summary>
        public void SelectRule()
        {
            isEnable = false;

            // 최초 적용 할 룰 등록
            if (GameManager.Instance.isTutorial.RuntimeValue)
            {
                // 선택 된 룰을 다른 스포너에게 알려줌
                for (int i = 0; i < OtherSapwner_FireBall.Count; i++)
                {
                    OtherSapwner_FireBall[i].SelectRule();
                }
            }
            else
            {
                // 첫번재 스포너의 룰 리스트를 가지고 무작위 선택
                int CurrentRuleIdx = Random.Range(0, OtherSapwner_FireBall[0].LsRule.Count);

                // 선택 된 룰을 다른 스포너에게 알려줌
                for (int i = 0; i < OtherSapwner_FireBall.Count; i++)
                {
                    OtherSapwner_FireBall[i].SelectRule(CurrentRuleIdx);
                }
            }

            isEnable = true;
        }

        /// <summary>
        /// 스폰 실행
        /// </summary>
        public GameObject RunSapwn(Spawner_FireBall spawner_FireBall)
        {
            // 스폰할 오프젝트 선택
            // 랜넘 룰이거나 파이어 카운트가 선택되어야 할 카운트보다 크면 그냥 랜덤으로 처리(에러 방지를 위함)
            if(spawner_FireBall.CurrentRule.ruleList[spawner_FireBall.CurrentRuleDetailIdx].isRandomFireRule
                || spawner_FireBall.currentFireCount > spawner_FireBall.CurrentRule.ruleList[spawner_FireBall.CurrentRuleDetailIdx].InteractableTypes.Count - 1)
            {
                SelectedSpawnIndex = Random.Range(0, LsSpawnGameObject.Count - 1);
            }
            else
            {
                SelectedSpawnIndex = spawner_FireBall.CurrentRule.ruleList[spawner_FireBall.CurrentRuleDetailIdx].InteractableTypes[spawner_FireBall.currentFireCount];
            }

            GameObject tmpObj = this.Spawn(false);
            tmpObj.transform.position = spawner_FireBall.transform.position;
            tmpObj.transform.rotation = spawner_FireBall.transform.rotation = CalcQuaternion(spawner_FireBall.transform.rotation, spawner_FireBall);
            tmpObj.SetActive(true);
            
            // 불꽃의 속도 (룰의 속도 + 가중속도)
            tmpObj.GetComponent<StateController>().stats.speed = spawner_FireBall.CurrentRule.ruleList[spawner_FireBall.CurrentRuleDetailIdx].speed + FireSpeed.RuntimeValue;
            return tmpObj;
        }

        /// <summary>
        /// 충돌체 각도 계산
        /// </summary>
        /// <param name="boxRuleIdx">서브 룰 번호</param>
        /// <param name="isStart">초기 세팅일 경우 true</param>
        /// <returns></returns>
        Quaternion CalcQuaternion(Quaternion qobj, Spawner_FireBall spawner_FireBall)
        {
            InteractableRule CurrentRule = spawner_FireBall.CurrentRule;
            InteractableUnitRule interactableUnitRule = spawner_FireBall.CurrentRule.ruleList[spawner_FireBall.CurrentRuleDetailIdx];

            switch (interactableUnitRule.interactableRuleType)
            {
                case InteractableRuleType.RANDOM:                   //랜덤형태로 뿌리는 형식
                    return Quaternion.Euler(0, Random.Range(interactableUnitRule.startDeg, interactableUnitRule.endDeg), 0);       //각도 값 세팅

                case InteractableRuleType.WAVE:                     //좌우로 왔다갔다 하는 형식
                    if (spawner_FireBall.isFirst)
                        return spawner_FireBall.startQuaternion;                                                          //각도 값 세팅
                    else
                    {
                        if(spawner_FireBall.movePersent < 1)
                            spawner_FireBall.movePersent += (Time.deltaTime * interactableUnitRule.angleSpeed);

                        if(spawner_FireBall.movePersent > 1)
                            spawner_FireBall.movePersent = 1;

                        return Quaternion.Lerp(spawner_FireBall.startQuaternion, spawner_FireBall.endQuaternion, spawner_FireBall.movePersent);
                        //return Quaternion.Euler( Quaternion.Slerp(spawner_FireBall.transform.rotation, Quaternion.Euler(0, interactableUnitRule.endDeg, 0), Time.deltaTime * interactableUnitRule.angleSpeed);
                    }
                case InteractableRuleType.ITEM:

                    return Quaternion.Euler(0, interactableUnitRule.startDeg, 0);

                default:
                    return Quaternion.Euler(Vector3.zero);

            }
        }

        /// <summary>
        /// 스폰 처리(스포너별 개별 처리해야 하는 로직임)
        /// </summary>
        /// <param name="defaultShow"></param>
        /// <returns></returns>
        protected override GameObject Spawn(bool defaultShow = true)
        {
            GameObject interactableGameObject = GamePrefabPoolManager.Instance.GetObjectForType(LsSpawnGameObject[0].name, false);
            // 불꽃 색상 교체
            interactableGameObject.GetComponent<ChangeFireBallColor>().ChgFireBallColor(LsFireBallType[SelectedSpawnIndex]);

            Interactable interactable = interactableGameObject.GetComponent<Interactable>();
            try
            {
                if (interactable == null)
                {
                    if (GameManager.Instance.gameDisplayMode == GameDisplayMode.Mode_2D)
                    {
                        interactable = interactableGameObject.AddComponent<Interactable2D>();
                    }
                    else
                    {
                        interactable = interactableGameObject.AddComponent<Interactable3D>();
                    }
                }

                // life관련이면 관련 컴포던트를 추가 함
                if ((LsEntity[SelectedEntityIndex].isLifeTime || LsEntity[SelectedEntityIndex].isJustOnce || LsEntity[SelectedEntityIndex].isDistance))
                {
                    PrefabPoolDestroy prefabPoolDestory = interactableGameObject.GetComponent<PrefabPoolDestroy>();
                    if (prefabPoolDestory == null)
                    {
                        prefabPoolDestory = interactableGameObject.AddComponent<PrefabPoolDestroy>();
                    }
                    prefabPoolDestory.Init(LsEntity[SelectedEntityIndex], gameObject);
                }

                interactableGameObject.SetActive(defaultShow);
                interactable.Spawn(transform.position, transform.rotation, LsEntity[SelectedEntityIndex], 1, targetTag);
            }
            catch (System.Exception err)
            {
                int i = 0;
            }

            return interactableGameObject;
        }

        #endregion Public Methods : End =============================================================

    }

}
