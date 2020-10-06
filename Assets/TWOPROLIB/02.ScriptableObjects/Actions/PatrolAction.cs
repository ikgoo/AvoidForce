using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Act(StateController controller)
        {
            Patrol(controller);
        }

        /// <summary>
        /// patrol 처리 로직
        /// </summary>
        /// <param name="controller"></param>
        private void Patrol(StateController controller)
        {
            // navMeshAgent 사용일 경우
            /***
            controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
            controller.navMeshAgent.Resume();
            if(controller.navMeshAgent.remoiningDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
            {
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
            }
            ***/

            // not navMeshAgent 일 경우

            controller.rigid2d.velocity = controller.transform.up * controller.stats.speed * Time.deltaTime;
            Vector2 direction = (Vector2)controller.wayPointList[controller.nextWayPoint].position - controller.rigid2d.position;
            direction.Normalize();

            float rotateValue = Vector3.Cross(direction, controller.transform.up).z;
            controller.rigid2d.angularVelocity = -rotateValue * controller.stats.angleSpeed;
            controller.rigid2d.velocity = controller.transform.up * controller.stats.speed * Time.deltaTime;

            if (Vector3.Distance(controller.transform.position, controller.wayPointList[controller.nextWayPoint].position) < 0.8f)
            {
                // 거리가 0.2f 이하일 경우 다음 이동 경로로 변경
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
            }
        }
    }
}
