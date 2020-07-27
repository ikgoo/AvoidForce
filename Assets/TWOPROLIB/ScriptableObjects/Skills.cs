using System.Collections.Generic;
using UnityEngine;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Script.EntitysAttributes;
using TWOPROLIB.Scripts.Controller;

namespace TWOPROLIB.ScriptableObjects.EntitysSkills
{
    [CreateAssetMenu(menuName = "Stats/Skill")]
    public class Skills : Entity
    {
        public int LevelNeed;
        public int XPNeed;

        public List<LiveEntityAttributes> AffectedAttributes = new List<LiveEntityAttributes>();

        public void SetValues(GameObject SkillDisplayObjet, StateController state)
        {

        }
    }
}
