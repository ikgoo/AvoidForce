using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 무기 관련
    /// </summary>
    public abstract class Weapon : ScriptableObject
    {
        /// <summary>
        /// PrefabObjectPool에서 호출 될 이름 
        /// </summary>
        public string weaponName = "None";
        /// <summary>
        /// 발사 쿨다운
        /// </summary>
        public float coolDown = 1f;

        /// <summary>
        /// 공격
        /// </summary>
        /// <param name="controller"></param>
        public abstract void Fire(StateController controller);

    }
}
