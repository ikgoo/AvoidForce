using System;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "DataType/Float")]
    public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
    {
        /// <summary>
        /// 기본적인 설명
        /// </summary>
        [Multiline]
        [Tooltip("기본적인 설명")]
        public string dersc;

        /// <summary>
        /// 데이터 값(최초값)
        /// </summary>
        [Tooltip("데이터 값(최초값)")]
        public float InitialValue;

        /// <summary>
        /// 데이터 값
        /// </summary>
        [Tooltip("데이터 값")]
        public float RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize()
        {

        }
    }

}
