using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillCompilerWindow : OdinEditorWindow
{
    [TabGroup("Skill","½ÇÉ«", SdfIconType.PersonFill,TextColor = "orange")]
    public SkillCharacterConfig character = new SkillCharacterConfig();

    [MenuItem("Skill/¼¼ÄÜ±à¼­Æ÷")]
    public static SkillCompilerWindow ShowWindow()
    {
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
