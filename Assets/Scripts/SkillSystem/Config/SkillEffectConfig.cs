using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideMonoScript]
[System.Serializable]
public class SkillEffectConfig
{
    [AssetList, LabelText("������Ч����"), PreviewField(70, ObjectFieldAlignment.Left)]
    public GameObject skillEffect;   //������Ч

    [LabelText("����֡")]
    public int triggerFrame;    //����֡

    [LabelText("����֡")]
    public int endFrame;        //����֡

    [LabelText("��Чƫ��λ��")]
    public Vector3 effectOffsetPos;    //��Чƫ��λ��

    [LabelText("��Чλ������")]
    public EffectPosType effectPosType;    //��Чλ������

    [ToggleGroup("isSetTransParent","�Ƿ����ø��ڵ�")]
    public bool isSetTransParent;    //�Ƿ�������Ч���ڵ�

    [ToggleGroup("isSetTransParent", "�ڵ�����")]
    public TransParentType transParentType;    //���ڵ�����

#if UNITY_EDITOR

    //�༭��ģʽ�´�������Ч
    private GameObject mCloneEffect;
    //��ǰ�߼�֡
    private int mCurLogicFrame = 0;
    //��Ч�������Ŵ���
    private AnimationAgent mAnimAgent;
    //��Ч���Ӳ��Ŵ���
    private ParticlesAgent mParticlesAgent;
    /// <summary>
    /// ��ʼ���ż���
    /// </summary>
    public void StartPlaySkill()
    {
        DestroyEffect();
        mCurLogicFrame = 0;
    }

    public void SkillPause()
    {
        DestroyEffect();
    }

    /// <summary>
    /// ���ż��ܽ���
    /// </summary>
    public void PlaySkillEnd()
    {
        DestroyEffect();
    }

    /// <summary>
    /// �߼�֡����
    /// </summary>
    public void OnLogicFrameUpdate()
    {
        if(mCurLogicFrame == triggerFrame)  //��ǰ�߼�֡���ڴ���֡��������Ч
        {
            CreateEffect();
        }
        else if(mCurLogicFrame == endFrame)  //��ǰ�߼�֡���ڽ���֡��������Ч
        {
            DestroyEffect();
        }
        mCurLogicFrame++;
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    public void CreateEffect()
    {
        if (skillEffect != null)
        {
            mCloneEffect = GameObject.Instantiate(skillEffect);
            mCloneEffect.transform.position = SkillCompilerWindow.GetCharacterPos();
            //��Editorģʽ�£������ļ���������Ч�����Ქ�ţ���Ҫͨ��������в���
            mAnimAgent = new AnimationAgent();
            mParticlesAgent = new ParticlesAgent();
            mAnimAgent.InitPlayAnim(mCloneEffect.transform);
            mParticlesAgent.InitPlayAnim(mCloneEffect.transform);
        }

    }

    /// <summary>
    /// ������Ч
    /// </summary>
    public void DestroyEffect()
    {
        if (mCloneEffect != null)
        {
            GameObject.DestroyImmediate(mCloneEffect);
        }
        if(mAnimAgent != null)
        {
            mAnimAgent.OnDestroy();
        }
        if (mParticlesAgent != null)
        {
            mParticlesAgent.OnDestroy();
        }
    }

#endif
}

public enum TransParentType
{
    [LabelText("������")] None,
    [LabelText("����")] LeftHand,
    [LabelText("����")] RightHand,
}

/// <summary>
/// ��Чλ������
/// </summary>
public enum EffectPosType
{ 
    [LabelText("�����ɫλ�úͷ���")]FollowPosDir,
    [LabelText("�����ɫ����")] FollowDir,
    [LabelText("��Ļ����λ��")] CenterPos,
    [LabelText("����λ��")] GuidePos,
    [LabelText("������Ч�ƶ�λ��")] FollowEffectMovePos,
}
