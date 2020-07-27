using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 미사일 무기
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Weapons/MissileBase")]
    public class MissileBase : Weapon
    {
        
        public override void Fire(StateController controller)
        {
            MissileBaseFire(controller);
        }

        private void MissileBaseFire(StateController controller)
        {
            DebugX.Log("미사일 공격");
        }
    }

}
