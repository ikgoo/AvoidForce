using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
    public class ScanDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool noEnemyIsSigh = Scan(controller);
            return noEnemyIsSigh;
        }

        private bool Scan(StateController controller)
        {
            // MeshAgent 사용시
            /***
            controller.navMeshAgent.stop();
            ***/
            controller.transform.Rotate(0, 0, controller.stats.angleSpeed * Time.deltaTime);
            return controller.CheckIfCountDownElapsed(controller.enemyStats.searchDuration);
        }
    }
}
