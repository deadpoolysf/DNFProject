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

#if UNITY_EDITOR

    //编辑器模式下创建的特效
    private GameObject mCloneEffect;
    //当前逻辑帧
    private int mCurLogicFrame = 0;
    //特效动画播放代理
    private AnimationAgent mAnimAgent;
    //特效粒子播放代理
    private ParticlesAgent mParticlesAgent;
    /// <summary>
    /// 开始播放技能
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
    /// 播放技能结束
    /// </summary>
    public void PlaySkillEnd()
    {
        DestroyEffect();
    }

    /// <summary>
    /// 逻辑帧更新
    /// </summary>
    public void OnLogicFrameUpdate()
    {
        if(mCurLogicFrame == triggerFrame)  //当前逻辑帧等于触发帧，创建特效
        {
            CreateEffect();
        }
        else if(mCurLogicFrame == endFrame)  //当前逻辑帧等于结束帧，销毁特效
        {
            DestroyEffect();
        }
        mCurLogicFrame++;
    }

    /// <summary>
    /// 创建特效
    /// </summary>
    public void CreateEffect()
    {
        if (skillEffect != null)
        {
            mCloneEffect = GameObject.Instantiate(skillEffect);
            mCloneEffect.transform.position = SkillCompilerWindow.GetCharacterPos();
            //在Editor模式下，动画文件和粒子特效都不会播放，需要通过代码进行播放
            mAnimAgent = new AnimationAgent();
            mParticlesAgent = new ParticlesAgent();
            mAnimAgent.InitPlayAnim(mCloneEffect.transform);
            mParticlesAgent.InitPlayAnim(mCloneEffect.transform);
        }

    }

    /// <summary>
    /// 销毁特效
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
