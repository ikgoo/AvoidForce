using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TWOPRO.Scripts.Spawners;
using UnityEngine;

public class ChangeFireBallColor : MonoBehaviour
{
    public FireBallType fireBallType;

    /// <summary>
    /// 파이어 볼 색상 교체
    /// </summary>
    /// <param name="fireBallType"></param>
    public void ChgFireBallColor(FireBallType fireBallType)
    {
        if(!fireBallType.Equals(this.fireBallType))
        {
            this.fireBallType = fireBallType;

            ParticleSystem[] childobj = gameObject.GetComponentsInChildren<ParticleSystem>();
            ParticleSystem.ColorOverLifetimeModule colorModule;

            if (childobj.Length == 5)
            {
                gameObject.GetComponent<Light>().color = this.fireBallType.FireBallColor;

                for(int i = 1; i < childobj.Length; i++)
                {
                    colorModule = childobj[i].colorOverLifetime;
                    colorModule.color = this.fireBallType.FireBallGrandient[i-1];
                }
            }
        }
    }

}
