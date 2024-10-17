using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillCompilerWindow : OdinEditorWindow
{
    [TabGroup("Skill","角色", SdfIconType.PersonFill,TextColor = "orange")]
    public SkillCharacterConfig character = new SkillCharacterConfig();

    [TabGroup("SkillCompiler", "Skill", SdfIconType.Robot, TextColor = "lightmagenta")]
    public SkillConfig skill = new SkillConfig();

    [TabGroup("SkillCompiler", "Damage", SdfIconType.Dash, TextColor = "blue")]
    public List<SkillDamageConfig> damageList = new List<SkillDamageConfig>();

    [TabGroup("SkillCompiler", "Effect", SdfIconType.OpticalAudio, TextColor = "blue")]
    public List<SkillEffectConfig> effctList = new List<SkillEffectConfig>();

#if UNITY_EDITOR
    //是否开始播放技能
    private bool isStartPlaySkill = false;


    [MenuItem("Skill/技能编辑器")]
    public static SkillCompilerWindow ShowWindow()
    {
        //绘制窗口
        return GetWindowWithRect<SkillCompilerWindow>(new Rect(0, 0, 800, 500));
    }
    /// <summary>
    /// 保存技能配置
    /// </summary>
    public void SaveSkillData()
    {
        SkillDataConfig.SaveSkillData(character, skill, damageList, effctList);
        Close();
    }

    /// <summary>
    /// 读取技能配置
    /// </summary>
    public void LoadSkillData(SkillDataConfig skillData)
    {
        this.character = skillData.character;
        this.skill = skillData.skillCfg;
        this.damageList = skillData.damageCfgList;
        this.effctList = skillData.effctCfgList;
    }

    public static SkillCompilerWindow GetWindow()
    {
        return GetWindow<SkillCompilerWindow>();
    }

    /// <summary>
    /// 获取技能面板下角色的位置
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetCharacterPos()
    {
        SkillCompilerWindow window = GetWindow<SkillCompilerWindow>();
        if(window.character.skillCharacter != null)
        {
            return window.character.skillCharacter.transform.position;
        }
        return Vector3.zero;
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var item in damageList)
        {
            item.OnInit();
        }
        EditorApplication.update += OnEditorUpdate;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        foreach (var item in damageList)
        {
            item.OnRelease();
        }
        EditorApplication.update -= OnEditorUpdate;
    }

    /// <summary>
    /// 开始播放技能
    /// </summary>
    public void StartPlaySkill()
    {
        foreach (var item in effctList)
        {
            item.StartPlaySkill();
        }
        foreach (var item in damageList)
        {
            item.PlaySkillStart();
        }
        isStartPlaySkill = true;

        //逻辑帧数据置0
        mAccLogicRuntime = 0;
        mNextLogicFrameTime = 0;
        mLastUpdateTime = 0;
    }

    public void SkillPause()
    {
        foreach (var item in effctList)
        {
            item.SkillPause();
        }
    }

    /// <summary>
    /// 播放技能结束
    /// </summary>
    public void PlaySkillEnd()
    {
        foreach (var item in effctList)
        {
            item.PlaySkillEnd();
        }
        foreach (var item in damageList)
        {
            item.PlaySkillEnd();
        }
        isStartPlaySkill = false;

        //逻辑帧数据置0
        mAccLogicRuntime = 0;
        mNextLogicFrameTime = 0;
        mLastUpdateTime = 0;
    }

    /// <summary>
    /// 编辑器窗口更新
    /// </summary>
    public void OnEditorUpdate()
    {
        try
        {
            //角色面板更新
            character.OnUpdate(()=>
            {
                Focus();
            });
            if(isStartPlaySkill)
            {
                //开始播放技能后再调用逻辑帧更新
                OnLogicUpdate();
            }
        }
        catch (System.Exception)
        {

        }
    }

    private float mAccLogicRuntime;   //逻辑帧累计运行时间
    private float mNextLogicFrameTime;  //下一个逻辑帧的时间
    private float mDeltaTime;   //动画缓动时间
    private double mLastUpdateTime;   //上次更新时间
    /// <summary>
    /// 逻辑帧更新
    /// </summary>
    public void OnLogicUpdate()
    {
        //模拟帧同步更新，以0.066间隔
        if(mLastUpdateTime == 0)
        {
            mLastUpdateTime = EditorApplication.timeSinceStartup;
        }
        //计算逻辑帧累计运行时间
        mAccLogicRuntime = (float)(EditorApplication.timeSinceStartup - mLastUpdateTime);
        /*?????为啥用while
        懂了，如果由于某些原因（例如系统负载高或帧率下降），实际的更新间隔超过了预期的逻辑帧间隔（例如 0.066 秒），
        那么 mAccLogicRuntime 会累积超过一个逻辑帧的时间。
        使用 while 循环可以确保在这段时间内执行多次逻辑更新，从而补上错过的时间。*/
        while (mAccLogicRuntime > mNextLogicFrameTime)
        {
            OnLogicFrameUpdate();
            //下一个逻辑帧时间
            mNextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
        }
    }

    public void OnLogicFrameUpdate()
    {
        foreach (var item in effctList)
        {
            item.OnLogicFrameUpdate();
        }
        foreach (var item in damageList)
        {
            item.OnLogicFrameUpdate();
        }
    }

#endif
}
