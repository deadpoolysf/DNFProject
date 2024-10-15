using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillConfig", menuName = "SkillConfig", order = 0)]
public class SkillDataConfig : ScriptableObject
{
    //角色数据配置
    public SkillCharacterConfig character;
    //技能基础数据配置
    public SkillConfig skillCfg;
    //技能伤害配置列表
    public List<SkillDamageConfig> damageCfgList;
    //技能特效配置列表
    public List<SkillEffectConfig> effctCfgList;

    public static void SaveSkillData(SkillCharacterConfig character, SkillConfig skillCfg, List<SkillDamageConfig> damageCfgList, List<SkillEffectConfig> effctCfgList)
    {
        //通过代码创建skilldataconfig实例，并对字段进行赋值存储
        SkillDataConfig skillDataConfig = ScriptableObject.CreateInstance<SkillDataConfig>();
        skillDataConfig.character = character;
        skillDataConfig.skillCfg = skillCfg;
        skillDataConfig.damageCfgList = damageCfgList;
        skillDataConfig.effctCfgList = effctCfgList;
        //把实例存储为.asset资源文件，当作技能配置
        string assetPath = "Assets/GameData/Game/SkillSystem/SkillData/"+ skillCfg.skillId+".asset";
        AssetDatabase.DeleteAsset(assetPath);
        AssetDatabase.CreateAsset(skillDataConfig, assetPath);
    }
}
