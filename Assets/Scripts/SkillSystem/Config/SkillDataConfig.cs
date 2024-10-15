using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
}
