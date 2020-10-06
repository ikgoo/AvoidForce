using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Entitys;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects.Entitys
{
    /// <summary>
    /// 게임의 모든 물체는 entity를 상속 받음
    /// </summary>
    public class Entity : ScriptableObject
    {
        [Header("--- Base Entity Data ---")]

        /// <summary>
        /// Entity 타입을 정의
        /// </summary>
        [Tooltip("Entity 타입을 정의")]
        public EntityTypes EntityType;

        /// <summary>
        /// Entity 이름을 정의
        /// </summary>
        [Tooltip("Entity 이름을 정의")]
        public string Name;

        /// <summary>
        /// Entity 설명을 정의
        /// </summary>
        [Tooltip("Entity 설명")]
        [TextArea]
        public string Description;

        /// <summary>
        /// 섬네일 이미지(스프라이트)
        /// </summary>
        [Tooltip("섬네일 이미지")]
        public Sprite Thumbnail;

        [Header("--- 생존 타입 ---")]

        /// <summary>
        /// 충돌 시 사라짐
        /// </summary>
        [Tooltip("충돌 시 사라짐")]
        public bool isJustOnce = false;

        /// <summary>
        /// 시간 종료 시 사라짐
        /// </summary>
        [Tooltip("시간 종료 시 사라짐")]
        public bool isLifeTime = false;

        /// <summary>
        /// 엔티티의 생존 시간(초)
        /// </summary>
        [Tooltip("엔티티의 생존 시간(초)")]
        public float lifeTime = 0f;

        /// <summary>
        /// 목적이 되는 오브젝트
        /// 거리 계산시에 대상 위치가 된다.
        /// </summary>
        [Tooltip("목적이 되는 오브젝트 : 거리 계산시에 대상 위치가 된다.")]
        public GameObject targetObj;

        /// <summary>
        /// 거리를 체크하여 사라짐
        /// </summary>
        [Tooltip("거리를 체크하여 사라짐")]
        public bool isDistance = false;

        /// <summary>
        /// 체크 할 거리
        /// </summary>
        [Tooltip("체크 할 거리")]
        public float Distance = 0f;

        /// <summary>
        /// 무적(절대 사라지지 않음)
        /// </summary>
        [Tooltip("무적(절대 사라지지 않음)")]
        public bool isForever = false;



    }
}
