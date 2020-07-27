using TWOPROLIB.Scripts.Controller;
using UnityEngine;


namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.blue;

        public void UpdateState(StateController controller)
        {
            DoActions(controller);                  // 액션 실행
            CheckTransitions(controller);           // 조건별 실행
        }

        /// <summary>
        /// 항상 실행되는 액션들
        /// </summary>
        /// <param name="controller"></param>
        private void DoActions(StateController controller)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        /// <summary>
        /// 특정 조건이 만족 할 경우 특정 액션 실행
        /// </summary>
        /// <param name="controller"></param>
        private void CheckTransitions(StateController controller)
        {
            for(int i = 0; i < transitions.Length; i++)
            {
                // 특정 조건 판단
                bool decisionSucceeded = transitions[i].decision.Decide(controller);
                if(decisionSucceeded)
                {
                    // 조건 만족 시 실행
                    controller.TrnasitionToState(transitions[i].trueState);
                } else
                {
                    // 조건 불 만족 시 실행
                    controller.TrnasitionToState(transitions[i].falseState);
                }
                
            }
        }
    }
}
