using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.Scripts.ObjectControlls
{
    public class CameraAction : MonoBehaviour
    {
        /// <summary>
        /// 목표 오브젝트
        /// </summary>
        [Tooltip("타켓 오브젝트")]
        public GameObject targetObject;

        public float followSpeed = 5f;

        // 3D용
        //public float offsetX = 0f;
        //public float offsetY = 25f;
        //public float offsetZ = -35f;

        void Start()
        {

        }

        private void LateUpdate()
        {
            if(targetObject.activeSelf)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -1), new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, -1), followSpeed * Time.deltaTime);
            }
        }
    }
}
