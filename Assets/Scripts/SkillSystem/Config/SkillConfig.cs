using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[HideMonoScript]
[System.Serializable]
public class SkillConfig
{
    [HideInInspector]
    public bool showStockPileData = false;    //是否显示蓄力技能数据
    [HideInInspector]
    public bool showPosGuideData = false;    //是否显示蓄力技能数据

    [LabelText("技能图标"), LabelWidth(0.1f), PreviewField(70, ObjectFieldAlignment.Left),SuffixLabel("技能图标")]
    public Sprite skillIcon;

    [LabelText("技能Id")]
    public int skillId;

    [LabelText("技能名称")]
    public string skillName;

    [LabelText("技能所需蓝量")]
    public int needSPValue;  //技能所需蓝量

    [LabelText("技能前摇时间")]
    public int skillShakeBeforeTimeMs;   //技能前摇时间

    [LabelText("技能攻击持续时间")]
    public int skillAttackDurationMs;    //技能攻击持续时间

    [LabelText("技能攻击后摇时间")]
    public int skillShakeAfterMs;       //技能攻击后摇时间

    [LabelText("技能冷却时间")]
    public int skillCDTimes;            //技能冷却时间

    [LabelText("技能类型"), OnValueChanged("OnSkillTypeChange")]
    public SkillType skillType;

    [LabelText("蓄力阶段配置(若第一段触发时间不为0，则空档时间为动画表现时间)"), ShowIf("showStockPileData")]
    public List<StockPileData> sockPileData;

    [LabelText("位置引导技能特效"), ShowIf("showPosGuideData"),BoxGroup("位置引导技能配置")]
    public GameObject skillGuideObj;

    [LabelText("位置引导技能范围"), ShowIf("showPosGuideData"), BoxGroup("位置引导技能配置")]
    public float skillGuideRange;

    [LabelText("组合技能id"),Tooltip("比如：技能A由技能B C D组成")]
    public int CombinationSkillId;


    //技能渲染相关

    [LabelText("技能命中特效"), TitleGroup("技能渲染", "所有英雄渲染数据会在技能一开始触发")]
    public GameObject skillHitEffect;

    [LabelText("技能击中特效存活时间")]
    public int hitEffectSurvivalTimeMs;

    [LabelText("技能命中音效")]
    public AudioClip skillHitAudio;

    [LabelText("是否显示技能立绘")]
    public bool showSkillProject;

    [LabelText("技能立绘"),ShowIf("showSkillProject")]
    public GameObject skillProjectObj;

    [LabelText("技能描述")]
    public string skillDes;


    public void OnSkillTypeChange(SkillType skillType)
    {
        showStockPileData = skillType == SkillType.StockPile;
        showPosGuideData = skillType == SkillType.PosGuide;
    }
}


public enum SkillType
{ 
    [LabelText("无配置(瞬发技能)")]None,
    [LabelText("吟唱技能")] Chant,
    [LabelText("弹道技能")] Ballistic,
    [LabelText("蓄力技能")] StockPile,
    [LabelText("位置引导技能")] PosGuide,
}

/// <summary>
/// 蓄力阶段数据
/// </summary>
[System.Serializable]
public class StockPileData
{
    [LabelText("蓄力阶段id")]
    public int stage;

    [LabelText("当前蓄力阶段触发的技能id")]
    public int skillId;

    [LabelText("当前阶段触发开始时间")]
    public int startTimeMs;

    [LabelText("当前阶段结束时间")]
    public int endTimeMs;
}
