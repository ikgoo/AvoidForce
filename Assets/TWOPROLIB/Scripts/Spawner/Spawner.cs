using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Interactables;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Spawner
{
    /// <summary>
    /// Entity 스포너
    /// </summary>
    public class Spawner : SpawnManager
    {

        protected override GameObject Spawn(bool defaultShow = true)
        {
            GameObject obj = base.Spawn(defaultShow);

            return obj;
        }

    }
}
