using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TWOPRO.Scripts.Spawners
{
    public class Spawner_FireBall : MonoBehaviour
    {
        #region Declaration : Start ============================================

        /// <summary>
        /// 최초 대기시간
        /// </summary>
        float currentFirstDelayTime;

        /// <summary>
        /// 현재 진행 시간(룰 전환용)
        /// </summary>
        float currentRuleDelayTime;

        /// <summary>
        /// 현재 발사 진행 시간(다음 발사 대기용)
        /// </summary>
        float currentFireDelayTime;

        /// <summary>
        /// 현재 발사항 발사체 카운트
        /// </summary>
        public int currentFireCount;

        /// <summary>
        /// 최초 룰 실행 유무
        /// </summary>
        public bool isFirst = true;

        /// <summary>
        /// 룰 종료
        /// </summary>
        [Tooltip("룰 종료")]
        public bool isDone = false;

        /// <summary>
        /// 마스터 스포터
        /// </summary>
        [Tooltip("마스터 스포터")]
        public SpawnerMaster spawnerMaster;

        /// <summary>
        /// 스폰너에 적용한 충돌체 스폰룰 리스트
        /// </summary>
        [Tooltip("스폰너에 적용한 충돌체 스폰룰 리스트")]
        public List<InteractableRule> LsRule;

        /// <summary>
        /// 튜토리얼 용 룰
        /// </summary>
        [Tooltip("튜토리얼 용 룰")]
        public InteractableRule TutorialRule;

        /// <summary>
        /// 현재 적용된 룰의 인덱스
        /// </summary>
        [Tooltip("현재 적용된 룰")]
        public int CurrentRuleIdx;

        /// <summary>
        /// 현재 적용된 룰 중 세부룰 인덱스
        /// 룰 하위에 세부 룰이 있음
        /// </summary>
        public int CurrentRuleDetailIdx;

        /// <summary>
        /// 현재 적용된 룰
        /// </summary>
        [Tooltip("현재 적용된 룰")]
        public InteractableRule CurrentRule;

        /// <summary>
        /// 회전 시작 방향
        /// </summary>
        public Quaternion startQuaternion;

        /// <summary>
        /// 회전 종료 방향 
        /// </summary>
        public Quaternion endQuaternion;

        /// <summary>
        /// 회전 진행률
        /// </summary>
        public float movePersent = 0f; 


        #endregion Declaration : End ===========================================

        #region Unity Methods : Start =============================================

        #endregion Unity Methods : End =============================================


        #region Public Methods : Start ==============================================================

        /// <summary>
        /// 서브 스포너 초기화
        /// </summary>
        /// <param name="spawnerMaster"></param>
        public void InitSapwner(SpawnerMaster spawnerMaster)
        {
            this.spawnerMaster = spawnerMaster;
        }

        /// <summary>
        /// 룰 선택
        /// </summary>
        public void SelectRule(int currentRuleIdx = -1)
        {
            // 기본값 초기화
            isFirst = true;
            isDone = false;
            CurrentRuleDetailIdx = 0;
            currentRuleDelayTime = 0;
            currentFireDelayTime = 0;
            currentFirstDelayTime = 0;
            currentFireCount = 0;

            if (GameManager.Instance.isTutorial.RuntimeValue)
            {
                // 튜토리얼 룰
                CurrentRule = TutorialRule;
            }
            else
            {
                this.CurrentRuleIdx = currentRuleIdx;
                if(LsRule.Count - 1 < CurrentRuleIdx)
                    CurrentRule = null;
                else
                    CurrentRule = LsRule[CurrentRuleIdx];
            }

        }

        /// <summary>
        /// 스폰 체크
        /// </summary>
        public bool CheckSapwn(float deltaTime)
        {
            if(CurrentRule == null)
            {
                // 현재 룰이 없다는건 다른 스포너가 끝날대가지 그냥 대기 함
                isDone = true;
                return isDone;
            }

            currentFirstDelayTime += deltaTime;
            if (isFirst && currentFirstDelayTime < CurrentRule.FirstDelay)
            {
                // 최초 딜레이 중
                return isDone;
            }

            currentRuleDelayTime += deltaTime;
            currentFireDelayTime += deltaTime;

            if (isDone)
                return isDone;

            // 스폰 체크
            if (currentRuleDelayTime >= CurrentRule.NextRuleDelay)
            {
                // 다음 룰 적용 시간이 된 경우
                if(CurrentRule.ruleList.Count - 1 >= CurrentRuleDetailIdx)
                {
                    RunSapwn();

                    // 다음 룰로 전환
                    CurrentRuleDetailIdx++;
                    currentRuleDelayTime = 0;
                    currentFireCount = 0;
                    isFirst = true;
                }
            }
            else if(CurrentRule.ruleList.Count - 1 < CurrentRuleDetailIdx)
            {
                // 모든 룰을 처리 한 후임
                isDone = true;
            }
            else if (isFirst && currentFireDelayTime >= CurrentRule.FirstDelay)
            {
                // 최초 생성
                RunSapwn();

                isFirst = false;
            }
            else if (!isFirst && currentFireDelayTime >= CurrentRule.ruleList[CurrentRuleDetailIdx].fireDelay)
            {
                // 다음 발사 시간이 된 경우
                RunSapwn();
            }

            return isDone;
        }

        /// <summary>
        /// 스폰 실행
        /// </summary>
        public void RunSapwn()
        {
            if(currentFireCount < CurrentRule.ruleList[CurrentRuleDetailIdx].FireCount
                && !CurrentRule.ruleList[CurrentRuleDetailIdx].cancelCount.Contains(currentFireCount) )
            {
                if(isFirst)
                {
                    // 최초에 시작 위치와 회전 진행률 초기화 처리
                    movePersent = 0;
                    startQuaternion = Quaternion.Euler(transform.rotation.eulerAngles.x, CurrentRule.ruleList[CurrentRuleDetailIdx].startDeg, transform.rotation.eulerAngles.z);
                    endQuaternion = Quaternion.Euler(transform.rotation.eulerAngles.x, CurrentRule.ruleList[CurrentRuleDetailIdx].endDeg, transform.rotation.eulerAngles.z);
                }

                // 발사 카운트를 넘기지 않고 취소카운트에 포함되어 있지 않으면 발사
                spawnerMaster.RunSapwn(this);
                currentFireCount++;
            }
            currentFireDelayTime = 0;
        }


        #endregion Public Methods : End =============================================================

    }
}
