using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Script.EntitysAttributes;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.ScriptableObjects.EntitysSkills;
using TWOPROLIB.Scripts.Entitys;
using TWOPROLIB.Scripts.Interactables;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Audio
{
    public class AudioPlayControll : MonoBehaviour
    {
        public string audioType;
        public string audioClipType;

        private void OnEnable()
        {
            GameAudioManager.Instance.PlayAudio(audioType, audioClipType);
        }
    }

}
