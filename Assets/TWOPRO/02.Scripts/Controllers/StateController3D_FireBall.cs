using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

public class StateController3D_FireBall : StateController3D
{
    protected override void Start()
    {
        base.Start();

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GameAudioManager.Instance.PlayAudio("AudioSourceBase", "etfx_shoot_fireball");
    }

}
