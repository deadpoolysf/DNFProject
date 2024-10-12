using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideMonoScript]
[System.Serializable]
public class SkillEffectConfig
{
    [AssetList, LabelText("技能特效对象"), PreviewField(70, ObjectFieldAlignment.Left)]
    public GameObject skillEffect;   //技能特效

    [LabelText("触发帧")]
    public int triggerFrame;    //触发帧

    [LabelText("结束帧")]
    public int endFrame;        //结束帧

    [LabelText("特效偏移位置")]
    public Vector3 effectOffsetPos;    //特效偏移位置

    [LabelText("特效位置类型")]
    public EffectPosType effectPosType;    //特效位置类型

    [ToggleGroup("isSetTransParent","是否设置父节点")]
    public bool isSetTransParent;    //是否设置特效父节点

    [ToggleGroup("isSetTransParent", "节点类型")]
    public TransParentType transParentType;    //父节点类型
}

public enum TransParentType
{
    [LabelText("无配置")] None,
    [LabelText("左手")] LeftHand,
    [LabelText("右手")] RightHand,
}

/// <summary>
/// 特效位置类型
/// </summary>
public enum EffectPosType
{ 
    [LabelText("跟随角色位置和方向")]FollowPosDir,
    [LabelText("跟随角色方向")] FollowDir,
    [LabelText("屏幕中心位置")] CenterPos,
    [LabelText("引导位置")] GuidePos,
    [LabelText("跟随特效移动位置")] FollowEffectMovePos,
}
