using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/RotationReverseAction")]
    public class RotationReverseAction : Action
    {
        public override void Act(StateController controller)
        {
            switch (GameManager.Instance.gameDisplayMode)
            {
                case GameDisplayMode.Mode_2D:

                    if (controller.isInput == true)
                    {
                        controller.transform.Rotate(0, 0, Input.GetAxis(GameManager.Instance.Input_Horizontal) * controller.stats.angleSpeed * Time.deltaTime);
                    }

                    break;

                case GameDisplayMode.Mode_3D:

                    if (controller.isInput == true)
                    {
                        controller.transform.Rotate(0, 0, Input.GetAxis(GameManager.Instance.Input_Horizontal) * controller.stats.angleSpeed * Time.deltaTime);
                    }

                    break;

                default:
                    break;


            }
        }
    }
}