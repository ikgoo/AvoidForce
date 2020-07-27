using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 지정 된 방향 보기
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/DirectionLook")]
    public class DirectionLookAction : Action
    {
        public override void Act(StateController controller)
        {
            if (GameManager.Instance.gameDisplayMode == Scripts.Managers.GameDisplayMode.Mode_2D)
            {
                // 2D의 경우 처리
                controller.rigid2d.velocity = controller.transform.up * controller.stats.speed * Time.deltaTime;

                if (controller.isInput == true)
                {
                    Vector2 direction = (Vector2)Input.GetNormalizedAxis() + (Vector2)controller.transform.position - controller.rigid2d.position;
                    direction.Normalize();

                    float rotateValue = Vector3.Cross(direction, controller.transform.up).z;
                    controller.rigid2d.angularVelocity = -rotateValue * controller.stats.angleSpeed;
                    controller.rigid2d.velocity = controller.transform.up * controller.stats.speed * Time.deltaTime;
                }
            }
        }
    }
}
