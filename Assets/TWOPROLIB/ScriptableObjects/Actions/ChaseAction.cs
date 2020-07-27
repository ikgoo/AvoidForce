using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            // navMeshAgent 일 경우
            /***
            controller.navMeshAgent.destination = controller.chaseTarget.position;
            controller.navMeshAgent.Resume();
            **/


            if (controller.chaseTarget != null)
            {
                if (GameManager.Instance.gameDisplayMode == Scripts.Managers.GameDisplayMode.Mode_2D)
                {
                    // 2d일 경우
                    controller.rigid2d.velocity = controller.transform.up * controller.stats.speed * Time.deltaTime;

                    Vector2 direction = (Vector2)controller.chaseTarget.position - controller.rigid2d.position;
                    direction.Normalize();

                    float rotateValue = Vector3.Cross(direction, controller.transform.up).z;
                    controller.rigid2d.angularVelocity = -rotateValue * controller.stats.angleSpeed;
                    controller.rigid2d.velocity = controller.transform.up * controller.stats.speed * Time.deltaTime;
                }
            }

        }
    }
}
