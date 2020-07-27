using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TWOPROLIB.Scripts.Controller;

namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// 오프젝트 플링 매니져
    /// </summary>
    public class PrefabPoolManager : MonoBehaviour
    {
        /// <summary>
        /// GameObject를 poolName으로 생성
        /// </summary>
        string poolName = "ObjectPool";

        /// <summary>
        /// 등록된 오브젝트 또는 프리팹 리스트
        /// </summary>
        [Tooltip("등록된 오브젝트 또는 프리팹 리스트")]
        public GameObject[] objectPrefabs;

        /// <summary>
        /// 실행시 인스턴스화 된 오프젝트 또는 프리팹 리스트
        /// </summary>
        [Tooltip("실행시 인스턴스화 된 오프젝트 또는 프리팹 리스트")]
        public Dictionary<string, List<GameObject>> pooledObjects;

        /// <summary>
        /// 인스턴스 리스트의 State정보 관리
        /// </summary>
        [Tooltip("인스턴스 리스트의 State정보 관리")]
        public Dictionary<string, List<StateController>> poolStates;

        /// <summary>
        /// 오디오 소스 버퍼
        /// </summary>
        [Tooltip("오프젝트 인스턴스 버퍼")]
        public int[] amountToBuffer;

        /// <summary>
        /// 기본 오디오 소스 버퍼
        /// </summary>
        [Tooltip("기본 오프젝트 인스턴스 버퍼")]
        public int defaultBufferAmount = 3;

        ///// <summary>
        ///// 각 오브젝트의 상위 오브젝트(없을 경우 기본 poolmanager쪽 오프젝트를 상위로 함)
        ///// </summary>
        //[Tooltip("부모 오브젝트")]
        //public GameObject[] parentGameObject;

        /// <summary>
        /// 버퍼 부족시 자동 인스턴스 생성 여부
        /// </summary>
        [Tooltip("버퍼 부족시 자동 인스턴스 생성 여부")]
        public bool willGrow = true;

        /// <summary>
        /// poolmanager 최상의 오브젝트
        /// </summary>
        protected GameObject containerObject;

        /// <summary>
        /// 각 오브젝트의 중간 오브젝트
        /// </summary>
        protected Dictionary<string, GameObject> midParentObject;

        public static PrefabPoolManager Instance = null;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        // 각 오프젝트를 버퍼 수만큼 생성
        void Start()
        {
            containerObject = new GameObject(poolName);
            containerObject.transform.parent = transform;
            containerObject.transform.position = Vector3.zero;
            pooledObjects = new Dictionary<string, List<GameObject>>();
            poolStates = new Dictionary<string, List<StateController>>();
            midParentObject = new Dictionary<string, GameObject>();

            int i = 0;
            GameObject tmpGameObject;

            foreach (GameObject objectPrefab in objectPrefabs)
            {
                objectPrefab.SetActive(false);      // 오프젝트 풀에 넣기 전에 모두 비활성화 함
                pooledObjects.Add(objectPrefab.name, new List<GameObject>());
                poolStates.Add(objectPrefab.name, new List<StateController>());

                int bufferAmount;
                if (i < amountToBuffer.Length) bufferAmount = amountToBuffer[i];
                else
                    bufferAmount = defaultBufferAmount;

                // sub group 생성
                tmpGameObject = new GameObject(objectPrefab.name);

                tmpGameObject.transform.parent = containerObject.transform;

                // 상위 오프젝트 개념 없어짐(무조건 지정된 위치에 저장)
                //if (parentGameObject.Length > i && parentGameObject[i] != null)
                //{
                //    if (parentGameObject[i].GetComponent<Canvas>() != null)
                //    {
                //        tmpGameObject.AddComponent<RectTransform>();
                //        tmpGameObject.transform.SetParent(parentGameObject[i].transform, false);
                //    }
                //    else
                //        tmpGameObject.transform.SetParent(parentGameObject[i].transform);
                //}
                //else
                //    tmpGameObject.transform.parent = containerObject.transform;
                midParentObject.Add(objectPrefab.name, tmpGameObject);

                for (int n = 0; n < bufferAmount; n++)
                {
                    GameObject newObj = Instantiate(objectPrefab) as GameObject;
                    newObj.name = objectPrefab.name;
                    AddPoolObject(newObj);
                }
                i++;
            }

        }

        private void Update()
        {

            for (int i = 0; i < poolStates.Keys.Count; i++)
            {
                string key = poolStates.Keys.ToArray()[i];

                for (int j = 0; j < poolStates[key].Count; j++)
                {
                    if (poolStates[key][j] != null && poolStates[key][j].gameObject.activeSelf)
                        poolStates[key][j].StateUpdate(Time.deltaTime);
                }
            }

        }

        private void FixedUpdate()
        {
            for (int i = 0; i < poolStates.Keys.Count; i++)
            {
                string key = poolStates.Keys.ToArray()[i];

                for (int j = 0; j < poolStates[key].Count; j++)
                {
                    if (poolStates[key][j] != null && poolStates[key][j].gameObject.activeSelf)
                        poolStates[key][j].StateFixedUpdate(Time.deltaTime);
                }
            }
        }

        /// <summary>
        /// pool 오프젝트의 상위 오프젝트 추출
        /// </summary>
        /// <param name="objectType">대상 오브젝트 명</param>
        /// <returns></returns>
        public GameObject GetRootParent(string objectType)
        {
            if (midParentObject.ContainsKey(objectType) == true)
            {
                GameObject tt = midParentObject[objectType].transform.parent.gameObject;
                RectTransform ttt = tt.GetComponent<RectTransform>();
                return midParentObject[objectType].transform.parent.gameObject;
            }

            return null;

        }

        /// <summary>
        /// pool 오프젝트의 상위 오프젝트에 위치 정보 추출
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public RectTransform GetRootParentRectTransform(string objectType)
        {
            if (midParentObject.ContainsKey(objectType) == true)
            {
                return midParentObject[objectType].transform.parent.gameObject.GetComponent<RectTransform>();
            }

            return null;

        }

        /// <summary>
        /// poolmanager에 오프젝트 추출
        /// : 오프젝트 종료 관련 로직 추가
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="isJustOnce"></param>
        /// <param name="isLifeTime"></param>
        /// <param name="lifeTime"></param>
        /// <param name="isForever"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public GameObject GetObjectForTypeWithPoolDestroy(string objectType, bool isJustOnce, bool isLifeTime, float lifeTime, bool isDistance, GameObject targetObj, float distance, bool isForever, bool active = false)
        {
            GameObject obj = GetObjectForType(objectType, active);
            PrefabPoolDestroy prefabPoolDestory = obj.GetComponent<PrefabPoolDestroy>();
            if (prefabPoolDestory == null)
            {
                prefabPoolDestory = obj.AddComponent<PrefabPoolDestroy>();
            }

            // 사라질 시간 정의
            prefabPoolDestory.Init(isJustOnce, isLifeTime, lifeTime, targetObj, isDistance, distance, isForever);

            return obj;
        }


        /// <summary>
        /// poolmanager에 오프젝트 추출
        /// </summary>
        /// <param name="objectType">오프젝트 명</param>
        /// <param name="active">활성화 유무</param>
        /// <returns></returns>
        public GameObject GetObjectForType(string objectType, bool active = false)
        {
            List<GameObject> tmpList = pooledObjects[objectType];
            if (tmpList == null)
            {
                DebugX.Log("Not prefab[" + objectType + "]");
                return null;
            }

            for (int i = 0; i < tmpList.Count; i++)
            {
                if (!tmpList[i].activeInHierarchy)
                {
                    tmpList[i].SetActive(active);
                    return tmpList[i];
                }
            }

            if (willGrow)
            {
                foreach (GameObject objectPrefab in objectPrefabs)
                {
                    if (objectPrefab.name.Equals(objectType))
                    {
                        GameObject newObj = Instantiate(objectPrefab) as GameObject;
                        newObj.name = objectPrefab.name;
                        AddPoolObject(newObj, active);
                        return newObj;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 대상 오프젝트를 poolmanager에 추가
        /// </summary>
        /// <param name="obj">게임 오프젝트</param>
        /// <param name="active">활성화 여부</param>
        public void AddPoolObject(GameObject obj, bool active = false)
        {
            obj.SetActive(active);
            List<GameObject> tmpList = pooledObjects[obj.name];
            if (tmpList != null)
            {
                tmpList.Add(obj);
                obj.transform.transform.SetParent(midParentObject[obj.name].transform, false);
            }

            List<StateController> tmpStateList = poolStates[obj.name];
            if (tmpStateList == null)
                return;

            tmpStateList.Add(obj.GetComponent<StateController>());

        }

        /// <summary>
        /// 모든 오브젝트를 활성화또는 비활성화 시킴
        /// </summary>
        public void AllDestroy(string name = "")
        {
            if (string.IsNullOrEmpty(name)) // 이름이 없으면 전체를 대상으로 처리
            {
                for (int i = 0; i < pooledObjects.Keys.Count; i++)
                {
                    string key = pooledObjects.Keys.ToArray()[i];

                    for (int j = 0; j < pooledObjects[key].Count; j++)
                    {

                        pooledObjects[key][j].SetActive(false);
                    }
                }

            }
            else
            {
                for (int i = 0; i < pooledObjects[name].Count; i++)
                {
                    //모든 오브젝트 수만큼
                    //돌리고 비활성화된것만 활성화 시킴
                    if (!pooledObjects[name][i].activeSelf)
                    {
                        pooledObjects[name][i].SetActive(false);
                    }
                }
            }
        }
    }
}
