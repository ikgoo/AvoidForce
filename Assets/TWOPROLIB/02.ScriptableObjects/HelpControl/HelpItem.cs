using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Dialog/HelpItem")]
    public class HelpItem : ScriptableObject
    {
        public List<GameObject> ViewObjs;
    }

}
