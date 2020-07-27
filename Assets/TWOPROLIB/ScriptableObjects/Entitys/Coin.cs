using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects.Entitys
{
    [CreateAssetMenu(menuName = "Stats/Entity/Coin")]
    public class Coin : Entity
    {
        public int coinValue = 0;

        public Coin()
        {
            EntityType = Scripts.Managers.EntityTypes.COIN;
        }
    }
}
