using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Interactables;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// 스폰 관리자
    /// </summary>
    [SerializeField]
    public abstract class SpawnManager : MonoBehaviour
    {
        /// <summary>
        /// 스포너 활성화 유무
        /// </summary>
        [Tooltip("스포너 활성화 유무")]
        public bool isEnable = false;

        /// <summary>
        /// 스폰 딜레이
        /// </summary>
        [Tooltip("스폰 딜레이")]
        private float delayTime = 5f;

        /// <summary>
        ///  현재 딜레이 타임
        /// </summary>
        [Tooltip("현재 딜레이 타임")]
        public float currentTime = 0f;

        /// <summary>
        /// 스폰 시 추가될 앤티티 리스트
        /// </summary>
        [Tooltip("스폰 시 추가될 앤티티 리스트")]
        public List<Entity> LsEntity;

        /// <summary>
        /// 스폰 시 추가될 앤티티의 index
        /// </summary>
        [Tooltip("스폰 시 추가될 앤티티의 index")]
        public int SelectedEntityIndex = -1;

        /// <summary>
        /// 스폰 할 게임 오브젝트 리스트
        /// </summary>
        [Tooltip("스폰 할 게임 오브젝트 리스트")]
        public List<GameObject> LsSpawnGameObject;

        /// <summary>
        /// 스폰 할 게임 오브젝트 Index
        /// </summary>
        [Tooltip("스폰 할 게임 오브젝트 Index")]
        public int SelectedSpawnIndex = -1;

        /// <summary>
        /// 인터렉트 발생할 대상 타켓 리스트
        /// </summary>
        [Tooltip("인터렉트 발생할 대상 타켓 리스트")]
        public List<string> targetTag;

        protected virtual void Awake()
        {
            if(LsEntity.Count >= 1)
            {
                SelectedEntityIndex = 0;
            }


            if(LsSpawnGameObject.Count >= 1)
            {
                SelectedSpawnIndex = 0;
            }
        }


        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        /// <summary>
        /// 스폰 처리(스포너별 개별 처리해야 하는 로직임)
        /// </summary>
        /// <param name="defaultShow"></param>
        /// <returns></returns>
        protected virtual GameObject Spawn(bool defaultShow = true)
        {
            GameObject interactableGameObject = GamePrefabPoolManager.Instance.GetObjectForType(LsSpawnGameObject[SelectedSpawnIndex].name, false);
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
            catch (Exception err)
            {
                int i = 0;
            }

            return interactableGameObject;
        }

        protected virtual void FixedUpdate()
        {
            if (isEnable && GameManager.Instance.playState.playState == PlayStateType.Play)
            {

                currentTime += Time.deltaTime;
                if(delayTime <= currentTime)
                {
                    // 대기 시간 초과 시 스폰 처리
                    Spawn(true);

                    currentTime = 0;
                }
            }

        }

        protected virtual void OnEnable()
        {
            // 스포너 나타날때 기본 정보 초기화
            currentTime = 0;
        }

        public virtual void OnGameStateEvent()
        {

        }

        public virtual void OnPlayStateEvnet()
        {

        }
    }
}
