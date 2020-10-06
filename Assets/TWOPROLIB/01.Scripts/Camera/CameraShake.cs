using System.Collections;
using System.Collections.Generic;
using TWOPRO.Scripts.Managers;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Audio
{
    /// <summary>
    /// 카메라 흔들림 처리
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        /// <summary>
        /// 최초 위치
        /// </summary>
        Vector3 originPosition;

        private void Awake()
        {
            originPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }

        /// <summary>
        /// 흔들림 관련 정보
        /// </summary>
        CameraShakeInfo cameraShakeInfo;

        /// <summary>
        /// 카메라 흔들림 메소드
        /// </summary>
        /// <param name="time"></param>
        /// <param name="amount"></param>
        public void Shake(CameraShakeInfo cameraShakeInfo)
        {
            // 튜토리얼에서는 흔들이 않음
            if (GameManager_Game.Instance.isTutorial.RuntimeValue)
                return;

            this.cameraShakeInfo = cameraShakeInfo;
            StartCoroutine(CoShake());
        }

        public IEnumerator Shake2(float duration, float magnitude)
        {
            // 튜토리얼에서는 흔들이 않음
            if (!GameManager_Game.Instance.isTutorial.RuntimeValue)
            {
                Vector3 originalPos = transform.localPosition;

                float elapsed = 0.0f;

                while (elapsed < duration)
                {
                    float x = Random.Range(-1f, 1f) * magnitude;
                    float y = Random.Range(-1f, 1f) * magnitude;

                    transform.localPosition = new Vector3(x, y, originalPos.z);

                    elapsed += Time.deltaTime;
                    yield return null;
                }

                transform.localPosition = originalPos;
            }
        }

        IEnumerator CoShake()
        {
            float startTime = Time.realtimeSinceStartup;
            while (true)
            {
                //Vector3 ShakePos = Random.insideUnitCircle * cameraShakeInfo.shakeAmount;
                float x = Random.Range(-1f, 1f) * cameraShakeInfo.shakeAmount;
                float y = Random.Range(-1f, 1f) * cameraShakeInfo.shakeAmount;
                transform.localPosition = new Vector3(originPosition.x + x, originPosition.y + y, originPosition.z);
                //transform.position = transform.position + new Vector3(ShakePos.x * 1.1f, 0, ShakePos.z * 1.1f);
                yield return new WaitForFixedUpdate();

                if (Time.realtimeSinceStartup - startTime >= cameraShakeInfo.shakeTime)
                {
                    break;
                }

            }

            ReleaseShake();
            //Invoke("ReleaseShake", time);
        }

        /// <summary>
        /// 카메라 원목 메소드
        /// </summary>
        public void ReleaseShake()
        {
            transform.localPosition = originPosition;    //원래 포지션 값을 넣어줌
                                                         //원래 색으로
                                                         //ColorOrigin();
        }


        
    }
}
