using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.EntitysSkills;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TWOPROLIB.Script.Displays
{
    public class Skilldisplay : MonoBehaviour
    {
        public Skills skill;
        public TextAlignment skillName;
        public TextAlignment skillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillXPNeeded;
        public Text skillAttribute;
        public Text skillAttrAmount;

        [SerializeField]
        private StateController m_PlayerHandler;

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
