using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class ParticlesAgent : MonoBehaviour
{
#if UNITY_EDITOR
    private ParticleSystem[] mParticleSystemArr;
    private double mLastRunTime;

    public void InitPlayAnim(Transform trans)
    {
        mParticleSystemArr = trans.GetComponentsInChildren<ParticleSystem>();
        EditorApplication.update += OnUpdate;
    }

    public void OnDestroy()
    {
        EditorApplication.update -= OnUpdate;
    }

    public void OnUpdate()
    {
        if (mLastRunTime == 0)
        {
            mLastRunTime = EditorApplication.timeSinceStartup;
        }
        //获取当前运行的时间
        double curRunTime = EditorApplication.timeSinceStartup - mLastRunTime;
        
        if(mParticleSystemArr!=null)
        {
            foreach (var item in mParticleSystemArr)
            {
                if(item!=null)
                {
                    item.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    item.useAutoRandomSeed = false;
                    item.Simulate((float)curRunTime);
                }
            }
        }
    }
#endif
}
