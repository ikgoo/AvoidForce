using TWOPROLIB.Scripts.Controller;

namespace TWOPROLIB.Scripts.Interactables
{
    public class PickupEntity2D : Interactable2D
    {
        public override bool Interact(string tag, StateController controller)
        {
            bool b = base.Interact(tag, controller);
            // 추가 작업

            return b;
        }
    }
}
