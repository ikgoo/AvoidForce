using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 상태 유잇이 레인지에 들어왔는지 확인
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
    public class LookDecision : Decision
    {
        /// <summary>
        /// 조건이 만족하면 true를 반환
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public override bool Decide(StateController controller)
        {
            bool targetVisible = Look(controller);
            return targetVisible;
        }


        private bool Look(StateController controller)
        {
            Debug.DrawRay(controller.firePoint.position, controller.firePoint.up.normalized * controller.enemyStats.lookRange, Color.green);

            RaycastHit2D hit = Physics2D.CircleCast(controller.firePoint.position, controller.enemyStats.lookSphereCastRadius, controller.firePoint.up, controller.enemyStats.lookRange);
            
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            else
                return false;
        }

    }
}
