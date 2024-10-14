using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class AnimationAgent
{
#if UNITY_EDITOR
    private Animation mAnim;
    private double mLastRunTime;

    public void InitPlayAnim(Transform trans)
    {
        mAnim = trans.GetComponentInChildren<Animation>();
        EditorApplication.update += OnUpdate;
    }

    public void OnDestroy()
    {
        EditorApplication.update -= OnUpdate;
    }

    public void OnUpdate()
    {
        if(mAnim!=null && mAnim.clip!=null)
        {
            if (mLastRunTime == 0)
            {
                mLastRunTime = EditorApplication.timeSinceStartup;
            }
            //��ȡ��ǰ���е�ʱ��
            double curRunTime = EditorApplication.timeSinceStartup - mLastRunTime;
            //���㶯�����Ž���
            //float curAnimNormalizationValue = (float)curRunTime / mAnim.clip.length;
            //��������
            mAnim.clip.SampleAnimation(mAnim.gameObject, (float)curRunTime);
        }
    }
#endif
}
