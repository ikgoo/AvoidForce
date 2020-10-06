using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;

namespace TWOPROLIB.Scripts.Managers
{
    public class PrefabPoolDestroy : MonoBehaviour
    {
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



        /// <summary>
        /// 충돌 시 액션 처리를 할 것인지 유무
        /// </summary>
        [Tooltip("충돌 시 액션 처리를 할 것인지 유무")]
        public bool isDestroyAction = false;

        /// <summary>
        /// 충돌 파괴 시 관련 오브젝트
        /// </summary>
        [Tooltip("충돌 파괴 시 관련 오브젝트")]
        public GameObject destroyGameObject;

        private void OnEnable()
        {
            if (isLifeTime == true)
                Invoke("Destroy", lifeTime);
        }

        private void FixedUpdate()
        {
            if(isDistance && targetObj != null)
            {
                if(Vector3.Distance(targetObj.transform.position, transform.position) >= Distance)
                {
                    Destroy();
                }
            }
        }

        /// <summary>
        /// 초기화(생성자의 소명을 어떻게 할지 정의)
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Init(Entity entity, GameObject targetObj)
        {
            Init(entity.isJustOnce, entity.isLifeTime, entity.lifeTime, targetObj, entity.isDistance, entity.Distance, entity.isForever);
        }

        /// <summary>
        /// 초기화(생성자의 소명을 어떻게 할지 정의)
        /// </summary>
        /// <param name="isJustOnce"></param>
        /// <param name="isLifeTime"></param>
        /// <param name="lifeTime"></param>
        /// <param name="isForever"></param>
        public virtual void Init(bool isJustOnce, bool isLifeTime, float lifeTime, GameObject targetObj, bool isDistance, float Distance, bool isForever)
        {
            this.isJustOnce = isJustOnce;
            this.isLifeTime = isLifeTime;
            this.lifeTime = lifeTime;
            this.isForever = isForever;
            this.isDistance = isDistance;
            this.Distance = Distance;
            this.targetObj = targetObj;
        }

        /// <summary>
        /// 사라질 때 액션 처리(LifeTime 기준)
        /// : 현재 오프젝트는 사라지고 파괴관련 오브젝트를 표현하고
        ///   파괴관련 오브젝트는 시간을 설정하여 지정 시간에 사라짐
        /// </summary>
        /// <param name="destroyObject_lifeTime">사라질 시간(초) 지정</param>
        public virtual void DestroyActionDistance(float destroyObject_distance, GameObject targetObj)
        {
            if (isDestroyAction && destroyGameObject != null)
            {
                try
                {
                    GameObject interactableGameObject = GamePrefabPoolManager.Instance.GetObjectForTypeWithPoolDestroy(destroyGameObject.name, false, false, 0, true, targetObj, destroyObject_distance, false, false);
                    interactableGameObject.transform.position = transform.position;
                    interactableGameObject.SetActive(true);
                }
                catch (Exception ex)
                {
                    DebugX.Log("DestroyAction", ex);
                }
            }

            Destroy();
        }

        /// <summary>
        /// 사라질 때 액션 처리(LifeTime 기준)
        /// : 현재 오프젝트는 사라지고 파괴관련 오브젝트를 표현하고
        ///   파괴관련 오브젝트는 시간을 설정하여 지정 시간에 사라짐
        /// </summary>
        /// <param name="destroyObject_lifeTime">사라질 시간(초) 지정</param>
        public virtual void DestroyActionLiftTime(float destroyObject_lifeTime)
        {
            if(isDestroyAction && destroyGameObject != null)
            {
                try
                {
                    GameObject interactableGameObject = GamePrefabPoolManager.Instance.GetObjectForTypeWithPoolDestroy(destroyGameObject.name, false, true, destroyObject_lifeTime, false, null, 0, false, false);
                    interactableGameObject.transform.position = transform.position;
                    interactableGameObject.SetActive(true);
                }
                catch (Exception ex)
                {
                    DebugX.Log("DestroyAction", ex);
                }
            }

            Destroy();
        }

        /// <summary>
        /// 오브젝트 제거
        /// </summary>
        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

    }
}
