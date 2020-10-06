using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPRO.Scripts.Controller
{
    public class StateController3D_Test : StateController3D
    {
        public override void StateUpdate(float deltaTime)
        {
            base.StateUpdate(deltaTime);
        }

        public override void StateFixedUpdate(float deltaTime)
        {
            base.StateFixedUpdate(deltaTime);
        }

        public override void OnStateTriggerEnter(GameObject childGameObject, GameObject targetObject)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateTriggerExit(GameObject childGameObject, GameObject targetObject)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateTriggerStay(GameObject childGameObject, GameObject targetObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
