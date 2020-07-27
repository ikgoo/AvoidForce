using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWOPROLIB.ScriptableObjects.Options;
using UnityEditor.PackageManager;

namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// 사운드 관련 매니저
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// GameObject를 poolName으로 생성
        /// </summary>
        string poolName = "AudioPool";

        /// <summary>
        /// 오디오 클립 리스트
        /// </summary>
        [Tooltip("오디오 클립 리스트")]
        public AudioClip[] audioClips;
        /// <summary>
        /// 오디오 클립을 Play할 오디오 소스 리스트
        /// </summary>
        [Tooltip("오디오 클립을 Play할 오디오 소스 리스트")]
        public GameObject[] liveAudioSources;
        /// <summary>
        /// 오디오 소스를 인스턴스 한 리스트(유니티에 GameObject 생성 시 같이 생성됨
        /// </summary>
        public Dictionary<string, List<GameObject>> liveSources;
        /// <summary>
        /// 오디오 소스의 버퍼를 설정하지 않은 경우 기본으로 생기는 수량
        /// </summary>
        public int liveDefaultBufferAmount = 3;
        /// <summary>
        /// 오디오 소스 버퍼 수량 설정
        /// </summary>
        [Tooltip("오디오 소스 버퍼 수량 설정")]
        public int[] liveAmountToBuffer;
        /// <summary>
        /// 오디오 소스 자동 추가
        /// </summary>
        [Tooltip("오디오 소스 자동 추가")]
        public bool willGrow = true;

        /// <summary>
        /// 배경 음악
        /// </summary>
        [Tooltip("배경음악")]
        public List<AudioSource> musicTracks;

        public List<bool> tracksActive;
        public float maxMusicVol = .4f;

        //오디오 데이터
        /// <summary>
        /// 오디오 데이터
        /// </summary>
        [Tooltip("오디오데이터 스크립트블오브젝트")]
        public AudioData audioData;


        /// <summary>
        /// 부모가 되는 오브젝트
        /// </summary>
        protected GameObject containerObject;
        /// <summary>
        /// 중분류가 되는 오브젝트
        /// </summary>
        protected Dictionary<string, GameObject> midParentObject;

        /// <summary>
        /// 게임내 소리 설정 데이터
        /// </summary>
        [Tooltip("게임내 소리 설정 데이터")]
        AudioSource audioSource;


        public static AudioManager Instance = null;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start()
        {
            containerObject = new GameObject(poolName);
            containerObject.transform.parent = transform;
            containerObject.transform.position = Vector3.zero;
            liveSources = new Dictionary<string, List<GameObject>>();
            midParentObject = new Dictionary<string, GameObject>();

            GameObject tmpGameObject;

            // live audio sources
            int i = 0;
            foreach (GameObject audioPrefab in liveAudioSources)
            {
                liveSources.Add(audioPrefab.name, new List<GameObject>());

                int bufferAmount;
                if (i < liveAmountToBuffer.Length) bufferAmount = liveAmountToBuffer[i];
                else
                    bufferAmount = liveDefaultBufferAmount;


                // sub group 생성
                tmpGameObject = new GameObject(audioPrefab.name);
                tmpGameObject.transform.SetParent(containerObject.transform);

                midParentObject.Add(audioPrefab.name, tmpGameObject);

                for (int n = 0; n < bufferAmount; n++)
                {
                    GameObject newas = Instantiate(audioPrefab) as GameObject;
                    newas.name = audioPrefab.name;
                    PoolAudioSource(newas);
                }

                i++;
            }

        }

        List<GameObject> tmpAudioSource;
        public void Update()
        {
            try
            {
                if (liveSources != null)
                {
                    // 최초 오디오 오브젝트 정리(인스턴스 생성)
                    foreach (string tmpid in liveSources.Keys)
                    {
                        tmpAudioSource = liveSources[tmpid];
                        for (int i = 0; i < tmpAudioSource.Count; i++)
                        {
                            audioSource = tmpAudioSource[i].GetComponent<AudioSource>();
                            if (tmpAudioSource[i].activeSelf == true && audioSource.isPlaying == false)
                            {
                                tmpAudioSource[i].transform.SetParent(midParentObject[tmpAudioSource[i].name].transform, false);
                                tmpAudioSource[i].SetActive(false);
                            }
                            audioSource = null;
                        }
                    }
                }
            } catch(Exception err)
            {
                int i = 0;
            }

            // 삭제 대상
            //for (int i = 0; i < musicTracks.Count; i++)
            //{
            //    if (musicTracks[i].isPlaying == false)
            //    {
            //        musicTracks[i].clip = null;
            //        musicTracks.RemoveAt(i);
            //        tracksActive.RemoveAt(i);
            //    }
            //}

            // tracksActive 상태에 따로 플레이를 자도으로 처리해줌(현재 기능은 빼둠)
            //for (int i = 0; i < musicTracks.Count; i++)
            //{
            //    CrossFadeAudioSource(musicTracks[i], tracksActive[i]);
            //}


        }

        /// <summary>
        /// 해당 오브젝트를 일정시간 후에 종료 처리(Disable 처리)
        /// </summary>
        /// <param name="obj">대상 오디오 오브젝트</param>
        /// <param name="delay_sec">종료 대기시간</param>
        /// <returns></returns>
        IEnumerator DelayDisable(GameObject obj, float delay_sec)
        {
            yield return new WaitForSeconds(delay_sec);
            obj.SetActive(false);
            yield return null;
        }

        /// <summary>
        /// Fadde 효과를 주어서 서서히 켜지고 서서히 꺼지게함
        /// </summary>
        /// <param name="audioSource">플레이할 오디오 소스</param>
        /// <param name="fadingIn">페이드 인아웃 플래그</param>
        public IEnumerator CrossFadeAudioSource(AudioSource audioSource, bool fadingIn = false, float duration = 1f)
        {
            //Don't bother if it is null. Graceful failing
            if (audioSource)
            {
                float startd = GetNowTime();
                float timed = 0f;
                while(true)
                {
                    if (fadingIn)
                    {
                        if (!audioSource.isPlaying)
                            audioSource.Play();

                        // 서서히 소리를 키워줌
                        if (audioSource.volume < maxMusicVol - .1f)
                        {
                            audioSource.volume = Mathf.Lerp(audioSource.volume, maxMusicVol, 0.75f * timed);
                        }
                        else
                        {
                            audioSource.volume = maxMusicVol;
                            break;
                        }
                    }
                    else
                    {
                        // 서서히 소리를 줄임
                        if (audioSource.volume > 0.05)
                        {
                            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.0f, timed);
                        }
                        else
                        {
                            audioSource.volume = 0;
                            audioSource.Stop();
                            break;
                        }
                    }

                    yield return new WaitForSecondsRealtime(0.1f);
                    timed += 0.1f;
                }

            }
        }

        public int GetNowTime()
        {

            System.DateTime now = System.DateTime.Now.ToLocalTime();
            System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            int nowTime = (int)span.TotalSeconds;

            return nowTime;
        }

        /// <summary>
        /// 트랙 음악을 서서히 줄임(현재 플레이 중인 트랙)
        /// </summary>
        /// <param name="musicTrackIndex"></param>
        /// <param name="fadingIn"></param>
        public void CrossFadeMusicTrack(bool fadingIn = false, float duration = 1f)
        {
            try
            {
                // 플레중인 모든 리스트를 처리
                for (int i = 0; i < tracksActive.Count; i++)
                {
                    if (tracksActive[i] == true)
                    {
                        CrossFadeMusicTrack(i, fadingIn, duration);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 트랙 음악을 서서히 줄임
        /// </summary>
        /// <param name="musicTrackIndex"></param>
        /// <param name="fadingIn"></param>
        public void CrossFadeMusicTrack(int musicTrackIndex, bool fadingIn = false, float duration = 1f)
        {
            try
            {
                StartCoroutine(CrossFadeAudioSource(musicTracks[musicTrackIndex], fadingIn, duration));
                tracksActive[musicTrackIndex] = false;
            }
            catch { }
        }

        /// <summary>
        /// 트랙 음악을 모두 바로 종료 처리
        /// </summary>
        public void PlayMusicTrackAllStop()
        {
            for (int i = 0; i < tracksActive.Count; i++)
            {
                if (tracksActive[i] == true)
                {
                    PlayMusicTrack(i, false);
                }
            }
        }

        /// <summary>
        /// 순번에 있는 배경음을 플레이 해줌
        /// </summary>
        /// <param name="musicTrackIndex"></param>
        /// <param name="play"></param>
        /// <param name="playonly"></param>
        /// <returns></returns>
        public bool PlayMusicTrack(int musicTrackIndex, bool play = true, bool playonly = false)
        {
            AudioSource tmpas = null;
            if (play == true && playonly == true)
            {
                // 현재 배경음악만 플레이 하고 싶을 경우
                for (int i = 0; i < tracksActive.Count; i++)
                {
                    if (tracksActive[i] == true)
                    {
                        tracksActive[i] = false;
                        musicTracks[i].Stop();
                    }
                }
            }

            if (musicTracks.Count <= musicTrackIndex)
                return false;

            // 배경음악을 플레이 또는 정지
            tmpas = musicTracks[musicTrackIndex];
            try
            {
                if (tmpas != null)
                {
                    if (play == true)
                    {
                        tmpas.volume = audioData.volumes[0].volume;
                        tmpas.loop = true;
                        tmpas.Play();
                    }
                    else
                        tmpas.Stop();

                    tracksActive[musicTrackIndex] = play;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                DebugX.Log(ex);
                return false;
            }
            finally
            {
                tmpas = null;
            }
        }

        AudioSource audioSource2;
        /// <summary>
        /// 지정된 오디오를 불러옮
        /// </summary>
        /// <param name="audioType">등록된 오디오의 이름</param>
        /// <param name="audioClipType">등록된 오디오 클립의 이름</param>
        /// <param name="gameObject">소리의 위치</param>
        /// <returns></returns>
        public AudioSource PlayAudio(string audioType, string audioClipType, Transform loc = null)
        {
            GameObject tmpas = GetAudioSource(audioType);
            try
            {
                if (tmpas != null)
                {
                    for (int i = 0; i < audioClips.Length; i++)
                    {
                        if (audioClipType.Equals(audioClips[i].name))
                        {
                            DebugX.Log("Shoot");

                            tmpas.SetActive(true);
                            audioSource2 = tmpas.GetComponent<AudioSource>();
                            if (gameObject != null)
                            {
                                //tmpas.transform.SetParent(loc, false);
                                tmpas.transform.position = loc.position;
                            }
                            audioSource2.volume = audioData.volumes[1].volume;
                            audioSource2.PlayOneShot(audioClips[i]);

                            return audioSource2;
                        }
                    }
                    return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                DebugX.Log(ex);
                return null;
            }
            finally
            {
                tmpas = null;
                audioSource2 = null;
            }
        }

        //public void PlayAudioRandom(string audioClipType, params string[] audioTypes)
        //{

        //}

        //public void PlayAudioRandomOnVolumn(float maxRange, float minRange, string audioClipType, string audioType)
        //{

        //}

        //public void PlayAudioFullOption(float maxRange, float minRange, string audioClipType, params string[] audioTypes)
        //{

        //}

        /// <summary>
        /// 지정한 타입의 오디오 가져오기
        /// </summary>
        /// <param name="audioType"></param>
        /// <returns></returns>
        GameObject GetAudioSource(string audioType)
        {
            if (liveSources.ContainsKey(audioType) == true)
            {
                for (int i = 0; i < liveSources[audioType].Count; i++)
                {
                    if (liveSources[audioType][i].activeInHierarchy == false)
                    {
                        return liveSources[audioType][i];
                    }
                }
            }
            else
            {
                return null;
            }

            if (willGrow)
            {
                foreach (GameObject audioPrefab in liveAudioSources)
                {
                    if (audioPrefab.name.Equals(audioType))
                    {
                        GameObject newas = Instantiate(audioPrefab) as GameObject;
                        newas.name = audioPrefab.name;
                        PoolAudioSource(newas);
                        return newas;
                    }
                }

            }
            return null;
        }

        /*
        /// <summary>
        /// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
        /// then null will be returned.
        /// </summary>
        /// <returns>
        /// The object for type.
        /// </returns>
        /// <param name='objectType'>
        /// Object type.
        /// </param>
        /// <param name='onlyPooled'>
        /// If true, it will only return an object if there is one currently pooled.
        /// </param>
        public AudioSource GetObjectForType(string objectType)
        {
            List<AudioSource> tmpList = pooledAudioSources[objectType];
            if (tmpList == null)
                return null;

            for (int i = 0; i < tmpList.Count; i++)
            {
                if (!tmpList[i].activeInHierarchy)
                {
                    tmpList[i].SetActive(true);
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
                        PoolObject(newObj);
                        return newObj;
                    }
                }
            }

            return null;
        }
        */

        /// <summary>
        /// 지정된 오디오 소스를 매니저에 등록
        /// </summary>
        /// <param name="obj">오디오 소스가 포함된 오브젝트</param>
        public void PoolAudioSource(GameObject obj)
        {
            List<GameObject> tmpList;
            tmpList = liveSources[obj.name];

            if (tmpList == null)
                return;

            tmpList.Add(obj);
            obj.transform.transform.SetParent(midParentObject[obj.name].transform, false);

        }

        /// <summary>
        /// 변경됨 볼륨을 조정해 줌
        /// </summary>
        /// <param name="idx">0:music(배경음), 1:effect(효과음)</param>
        /// <param name="volume">0~1f</param>
        public void ChangeVolume(int idx)
        {
            if(idx == 0)
            {
                // 플레이중인 배경은 볼륨을 모두 조정
                for(int i = 0; i < tracksActive.Count; i++)
                {
                    if(tracksActive[i] == true)
                    {
                        musicTracks[i].volume = audioData.volumes[idx].volume;
                    }
                }
            }
            else if(idx == 1)
            {
                foreach(string k in liveSources.Keys)
                {
                    for(int i = 0; i < liveSources[k].Count; i++)
                    {
                        if(liveSources[k][i] != null && liveSources[k][i].activeSelf == true)
                        {
                            liveSources[k][i].GetComponent<AudioSource>().volume = audioData.volumes[idx].volume;
                        }
                    }
                }

            }
        }
    }

}
