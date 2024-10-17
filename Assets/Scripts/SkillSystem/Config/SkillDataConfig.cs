using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
[CreateAssetMenu(fileName = "SkillConfig", menuName = "SkillConfig", order = 0)]
public class SkillDataConfig : ScriptableObject
{
    //��ɫ��������
    public SkillCharacterConfig character;
    //���ܻ�����������
    public SkillConfig skillCfg;
    //�����˺������б�
    public List<SkillDamageConfig> damageCfgList;
    //������Ч�����б�
    public List<SkillEffectConfig> effctCfgList;

#if UNITY_EDITOR

    public static void SaveSkillData(SkillCharacterConfig character, SkillConfig skillCfg, List<SkillDamageConfig> damageCfgList, List<SkillEffectConfig> effctCfgList)
    {
        //ͨ�����봴��skilldataconfigʵ���������ֶν��и�ֵ�洢
        SkillDataConfig skillDataConfig = ScriptableObject.CreateInstance<SkillDataConfig>();
        skillDataConfig.character = character;
        skillDataConfig.skillCfg = skillCfg;
        skillDataConfig.damageCfgList = damageCfgList;
        skillDataConfig.effctCfgList = effctCfgList;
        //��ʵ���洢Ϊ.asset��Դ�ļ���������������
        string assetPath = "Assets/GameData/Game/SkillSystem/SkillData/"+ skillCfg.skillId+".asset";
        AssetDatabase.DeleteAsset(assetPath);
        AssetDatabase.CreateAsset(skillDataConfig, assetPath);
    }

    [Button("���ü���",ButtonSizes.Large), GUIColor("green")]
    public void ShowSkillWindowButtonClick()
    {
        SkillCompilerWindow window = SkillCompilerWindow.ShowWindow();
        window.LoadSkillData(this);
    }
#endif
}
