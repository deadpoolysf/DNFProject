using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillCompilerWindow : OdinEditorWindow
{
    [TabGroup("Skill","��ɫ", SdfIconType.PersonFill,TextColor = "orange")]
    public SkillCharacterConfig character = new SkillCharacterConfig();

    [TabGroup("SkillCompiler", "Skill", SdfIconType.Robot, TextColor = "lightmagenta")]
    public SkillConfig skill = new SkillConfig();

    [TabGroup("SkillCompiler", "Damage", SdfIconType.Dash, TextColor = "blue")]
    public List<SkillDamageConfig> damageList = new List<SkillDamageConfig>();

    [TabGroup("SkillCompiler", "Effect", SdfIconType.OpticalAudio, TextColor = "blue")]
    public List<SkillEffectConfig> effctList = new List<SkillEffectConfig>();

#if UNITY_EDITOR
    //�Ƿ�ʼ���ż���
    private bool isStartPlaySkill = false;


    [MenuItem("Skill/���ܱ༭��")]
    public static SkillCompilerWindow ShowWindow()
    {
        //���ƴ���
        return GetWindowWithRect<SkillCompilerWindow>(new Rect(0, 0, 800, 500));
    }
    /// <summary>
    /// ���漼������
    /// </summary>
    public void SaveSkillData()
    {
        SkillDataConfig.SaveSkillData(character, skill, damageList, effctList);
        Close();
    }

    /// <summary>
    /// ��ȡ��������
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
    /// ��ȡ��������½�ɫ��λ��
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
    /// ��ʼ���ż���
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

        //�߼�֡������0
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
    /// ���ż��ܽ���
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

        //�߼�֡������0
        mAccLogicRuntime = 0;
        mNextLogicFrameTime = 0;
        mLastUpdateTime = 0;
    }

    /// <summary>
    /// �༭�����ڸ���
    /// </summary>
    public void OnEditorUpdate()
    {
        try
        {
            //��ɫ������
            character.OnUpdate(()=>
            {
                Focus();
            });
            if(isStartPlaySkill)
            {
                //��ʼ���ż��ܺ��ٵ����߼�֡����
                OnLogicUpdate();
            }
        }
        catch (System.Exception)
        {

        }
    }

    private float mAccLogicRuntime;   //�߼�֡�ۼ�����ʱ��
    private float mNextLogicFrameTime;  //��һ���߼�֡��ʱ��
    private float mDeltaTime;   //��������ʱ��
    private double mLastUpdateTime;   //�ϴθ���ʱ��
    /// <summary>
    /// �߼�֡����
    /// </summary>
    public void OnLogicUpdate()
    {
        //ģ��֡ͬ�����£���0.066���
        if(mLastUpdateTime == 0)
        {
            mLastUpdateTime = EditorApplication.timeSinceStartup;
        }
        //�����߼�֡�ۼ�����ʱ��
        mAccLogicRuntime = (float)(EditorApplication.timeSinceStartup - mLastUpdateTime);
        /*?????Ϊɶ��while
        ���ˣ��������ĳЩԭ������ϵͳ���ظ߻�֡���½�����ʵ�ʵĸ��¼��������Ԥ�ڵ��߼�֡��������� 0.066 �룩��
        ��ô mAccLogicRuntime ���ۻ�����һ���߼�֡��ʱ�䡣
        ʹ�� while ѭ������ȷ�������ʱ����ִ�ж���߼����£��Ӷ����ϴ����ʱ�䡣*/
        while (mAccLogicRuntime > mNextLogicFrameTime)
        {
            OnLogicFrameUpdate();
            //��һ���߼�֡ʱ��
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
