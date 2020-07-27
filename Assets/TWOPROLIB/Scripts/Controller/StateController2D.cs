using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using UnityEngine;

namespace TWOPROLIB.Scripts.Controller
{
    /// <summary>
    /// 오브젝트의 상태를 제어하는 클래스
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class StateController2D : StateController
    {

        protected override void Start()
        {
            rigid2d = GetComponent<Rigidbody2D>();
            rigid2d.gravityScale = 0;
            nextWayPoint = 0;
        }

        public override void StateUpdate(float deltaTime)
        {
            base.StateUpdate(deltaTime);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {

            DebugX.Log("충돌");
        }
    }

}
