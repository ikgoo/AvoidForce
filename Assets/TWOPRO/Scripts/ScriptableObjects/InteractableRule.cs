using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Spawners
{
    [CreateAssetMenu(menuName = "Rule/Interactable")]
    public class InteractableRule : ScriptableObject
    {
        /// <summary>
        /// 설명
        /// </summary>
        [Tooltip("설명")]
        [Multiline]
        public string Desc;

        /// <summary>
        /// 다음 룰 적용 대기 시간
        /// </summary>
        [Tooltip("다음 룰 적용 대기 시간")]
        public float NextRuleDelay;

        /// <summary>
        /// 연이어서 적용 할 룰 (-1일 경우 랜덤 처리)
        /// </summary>
        public int NextRule = -1;

        /// <summary>
        /// 최초 시 대기 후 스폰을 위한 것
        /// </summary>
        public float FirstDelay = 0f;

        /// <summary>
        /// 충돌체 룰 리스트
        /// </summary>
        [Tooltip("충돌체 룰 리스트")]
        public List<InteractableUnitRule> ruleList = new List<InteractableUnitRule>();



    }

    /// <summary>
    /// 충돌체 개별 룰
    /// </summary>
    [Serializable]
    public class InteractableUnitRule
    {
        /// <summary>
        /// 충동체 룰 종류
        /// </summary>
        [Tooltip("충동체 룰 종류[현재 미사용]")]
        public InteractableRuleType interactableRuleType;

        /// <summary>
        /// 시작 각도
        /// </summary>
        [Tooltip("시작 각도")]
        [Range(140, 220)]
        public float startDeg;

        /// <summary>
        /// 종료 각도
        /// </summary>
        [Tooltip("종료 각도")]
        [Range(140, 220)]
        public float endDeg;

        /// <summary>
        /// 충돌체 수량
        /// </summary>
        [Tooltip("충돌체 수량 : 시작 ~ 종료까지동안 발사 수량")]
        [Range(1, 100)]
        public int FireCount;

        /// <summary>
        /// 발사 간격 시간
        /// </summary>
        [Tooltip("발사 간격 시간")]
        [Range(0, 2)]
        public float fireDelay;

        /// <summary>
        /// 회전 속도
        /// </summary>
        [Tooltip("회전 속도")]
        [Range(0, 100)]
        public float angleSpeed;

        /// <summary>
        /// 발사체 이동 속도값
        /// </summary>
        [Tooltip("발사체 이동 속도값")]
        [Range(0, 2000)]
        public float speed;

        /// <summary>
        /// 발사 하지 않는 카운트
        /// </summary>
        [Tooltip("발사 하지 않는 카운트")]
        public List<int> cancelCount = new List<int>();

        /// <summary>
        /// 랜덤 유무
        /// </summary>
        [Tooltip("랜덤 유무")]
        public bool isRandomFireRule = false;

        /// <summary>
        /// 충돌체 종류 = 0 : 화이어볼,1 : 녹색,2 : 핑크,3 :블루
        /// 리스트가 없으면 랜덤 컬러로 발사
        /// FireCount와 같은 List를 사용함
        /// </summary>
        [Tooltip("충돌체 종류 = 0 : 화이어볼,1 : 녹색,2 : 핑크,3 :블루 / 리스트가 없으면 랜덤 컬러로 발사")]
        public List<int> InteractableTypes = new List<int>();


    }


    /// <summary>
    /// 충돌체 룰 종류
    /// </summary>
    public enum InteractableRuleType//이넘으로 상태값 설정할 수 있게
    {
        RANDOM,//랜덤
        WAVE,//웨이브(지정된 형태)
        ITEM //아이템

    }
}
