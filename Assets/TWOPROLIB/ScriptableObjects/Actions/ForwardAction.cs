using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Forward")]
    public class ForwardAction : Action
    {
        /// <summary>
        /// 이동 방향
        /// </summary>
        [Tooltip("이동 방향")]
        public Direction dir = Direction.Forward;

        public override void Act(StateController controller)
        {
            switch (GameManager.Instance.gameDisplayMode)
            {
                case GameDisplayMode.Mode_2D:

                    if(controller.isRigidbody == true)
                    {
                        switch(dir)
                        {
                            case Direction.Forward:
                                controller.transform.Translate(Vector3.up * controller.stats.speed * Time.deltaTime);
                                break;

                            default:
                                break;
                        }
                    }

                    break;

                case GameDisplayMode.Mode_3D:

                    if (controller.isRigidbody == true)
                    {
                        switch (dir)
                        {
                            case Direction.Forward:
                                if(controller.isRigidbody)
                                {
                                    controller.rigid3d.velocity = controller.transform.forward * controller.stats.speed;
                                }
                                else
                                {
                                    controller.transform.Translate(Vector3.forward * controller.stats.speed * Time.deltaTime);
                                }
                                break;

                            default:
                                break;
                        }
                    }

                    break;

                default:
                    break;


            }
        }
    }
}
