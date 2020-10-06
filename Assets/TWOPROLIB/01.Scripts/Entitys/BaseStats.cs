using System;
using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;

namespace TWOPROLIB.Scripts.Entitys
{
    [Serializable]
    public class BaseStats
    {
        /// <summary>
        /// 최대 HP
        /// </summary>
        [Tooltip("최대 HP")]
        [SerializeField] public int maxHP = 10;

        /// <summary>
        /// HP
        /// </summary>
        [Tooltip("HP")]
        public int hp = 10;

        /// <summary>
        /// 최대 MP
        /// </summary>
        [Tooltip("최대 MP")]
        public int maxMP = 10;

        /// <summary>
        /// MP
        /// </summary>
        [Tooltip("MP")]
        public int mp = 10;

        /// <summary>
        /// 공격력
        /// </summary>
        [Tooltip("공격력")]
        public int attack = 10;

        /// <summary>
        /// 방어력
        /// </summary>
        [Tooltip("방어력")]
        public int defense = 0;

        /// <summary>
        /// 이동 속도
        /// </summary>
        [Tooltip("이동 속도")]
        public float speed = 200;

        /// <summary>
        /// 회전 속도
        /// </summary>
        [Tooltip("회전 속도")]
        public float angleSpeed = 200;

        /// <summary>
        /// 소지금
        /// </summary>
        [Tooltip("소지금")]
        public int coin = 0;

        public void InitData(BaseStatsScriptableObject baseState)
        {
            this.maxHP = baseState.maxHP;
            this.hp = baseState.hp;
            this.maxMP = baseState.maxMP;
            this.mp = baseState.mp;
            this.attack = baseState.attack;
            this.defense = baseState.defense;
            this.speed = baseState.speed;
        }
    }
}
