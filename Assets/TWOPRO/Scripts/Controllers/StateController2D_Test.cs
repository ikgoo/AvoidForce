using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPRO.Scripts.Controller
{
    public class StateController2D_Test : StateController2D
    {
        protected override void Start()
        {
            base.Start();
            //rigid2d.gravityScale = 0f;
        }
        public override void StateUpdate(float deltaTime)
        {
            base.StateUpdate(deltaTime);
        }

        public override void StateFixedUpdate(float deltaTime)
        {
            base.StateFixedUpdate(deltaTime);
        }


    }
}
