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

    [TabGroup("SkillCompiler", "Effect", SdfIconType.OpticalAudio, TextColor = "blue")]
    public List<SkillEffectConfig> skillEffectList = new List<SkillEffectConfig>();

    [MenuItem("Skill/���ܱ༭��")]
    public static SkillCompilerWindow ShowWindow()
    {
        //���ƴ���
        return GetWindowWithRect<SkillCompilerWindow>(new Rect(0, 0, 800, 500));
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EditorApplication.update += OnEditorUpdate;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EditorApplication.update -= OnEditorUpdate;
    }

    public void OnEditorUpdate()
    {
        try
        {
            character.OnUpdate(()=>
            {
                Focus();
            });
        }
        catch (System.Exception)
        {

        }
    }
}
