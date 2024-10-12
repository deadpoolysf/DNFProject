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
