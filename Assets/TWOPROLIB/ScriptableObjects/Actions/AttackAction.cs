using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 공격
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/AttackAction")]
    public class AttackAction : Action
    {
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        private void Attack(StateController controller)
        {
            Debug.DrawRay(controller.firePoint.position, controller.firePoint.up.normalized * controller.enemyStats.attackRange, Color.red);

            RaycastHit2D hit = Physics2D.CircleCast(controller.firePoint.position, controller.enemyStats.lookSphereCastRadius, controller.firePoint.up, controller.enemyStats.attackRange);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                if (controller.CheckIfCountDownElapsed(controller.weapon.coolDown))
                {
                    controller.weapon.Fire(controller);
                }
            }
        }
    }
}
