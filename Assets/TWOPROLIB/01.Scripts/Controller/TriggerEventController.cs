using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Controller
{
    /// <summary>
    /// 하위 게임 오브젝트에서 사용할 충돌일 경우 root StateController에게 충돌체 정보를 보내줌
    /// </summary>
    public class TriggerEventController : MonoBehaviour
    {

        /// <summary>
        /// 충돌체 이름(옵션)
        /// </summary>
        [Tooltip("충돌체 이름(옵션)")]
        public string objName = "";

        private void OnTriggerEnter(Collider other)
        {
            try
            {
                StateController[] target = transform.GetComponentsInParent<StateController>();
                if (target.Length != 0)
                    target[0].OnStateTriggerEnter(this.gameObject, other.gameObject);
            }
            catch { }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            try
            {
                StateController[] target = transform.GetComponentsInParent<StateController>();
                if (target.Length != 0)
                    target[0].OnStateTriggerEnter(this.gameObject, collision.gameObject);
            }
            catch { }
        }

        private void OnTriggerStay(Collider other)
        {
            try
            {
                StateController[] target = transform.GetComponentsInParent<StateController>();
                if (target.Length != 0)
                    target[0].OnStateTriggerStay(this.gameObject, other.gameObject);
            }
            catch { }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            try
            {
                StateController[] target = transform.GetComponentsInParent<StateController>();
                if (target.Length != 0)
                    target[0].OnStateTriggerStay(this.gameObject, collision.gameObject);
            }
            catch { }
        }

        private void OnTriggerExit(Collider other)
        {
            try
            {
                StateController[] target = transform.GetComponentsInParent<StateController>();
                if (target.Length != 0)
                    target[0].OnStateTriggerExit(this.gameObject, other.gameObject);
            }
            catch { }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            try
            {
                StateController[] target = transform.GetComponentsInParent<StateController>();
                if (target.Length != 0)
                    target[0].OnStateTriggerExit(this.gameObject, collision.gameObject);
            }
            catch { }
        }
    }

}