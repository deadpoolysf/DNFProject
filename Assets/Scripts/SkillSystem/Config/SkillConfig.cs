using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[HideMonoScript]
[System.Serializable]
public class SkillConfig
{
    [HideInInspector]
    public bool showStockPileData = false;    //�Ƿ���ʾ������������
    [HideInInspector]
    public bool showPosGuideData = false;    //�Ƿ���ʾ������������

    [LabelText("����ͼ��"), LabelWidth(0.1f), PreviewField(70, ObjectFieldAlignment.Left),SuffixLabel("����ͼ��")]
    public Sprite skillIcon;

    [LabelText("����Id")]
    public int skillId;

    [LabelText("��������")]
    public string skillName;

    [LabelText("������������")]
    public int needSPValue;  //������������

    [LabelText("����ǰҡʱ��")]
    public int skillShakeBeforeTimeMs;   //����ǰҡʱ��

    [LabelText("���ܹ�������ʱ��")]
    public int skillAttackDurationMs;    //���ܹ�������ʱ��

    [LabelText("���ܹ�����ҡʱ��")]
    public int skillShakeAfterMs;       //���ܹ�����ҡʱ��

    [LabelText("������ȴʱ��")]
    public int skillCDTimes;            //������ȴʱ��

    [LabelText("��������"), OnValueChanged("OnSkillTypeChange")]
    public SkillType skillType;

    [LabelText("�����׶�����(����һ�δ���ʱ�䲻Ϊ0����յ�ʱ��Ϊ��������ʱ��)"), ShowIf("showStockPileData")]
    public List<StockPileData> sockPileData;

    [LabelText("λ������������Ч"), ShowIf("showPosGuideData"),BoxGroup("λ��������������")]
    public GameObject skillGuideObj;

    [LabelText("λ���������ܷ�Χ"), ShowIf("showPosGuideData"), BoxGroup("λ��������������")]
    public float skillGuideRange;

    [LabelText("��ϼ���id"),Tooltip("���磺����A�ɼ���B C D���")]
    public int CombinationSkillId;


    //������Ⱦ���

    [LabelText("����������Ч"), TitleGroup("������Ⱦ", "����Ӣ����Ⱦ���ݻ��ڼ���һ��ʼ����")]
    public GameObject skillHitEffect;

    [LabelText("���ܻ�����Ч���ʱ��")]
    public int hitEffectSurvivalTimeMs;

    [LabelText("����������Ч")]
    public AudioClip skillHitAudio;

    [LabelText("�Ƿ���ʾ��������")]
    public bool showSkillProject;

    [LabelText("��������"),ShowIf("showSkillProject")]
    public GameObject skillProjectObj;

    [LabelText("��������")]
    public string skillDes;


    public void OnSkillTypeChange(SkillType skillType)
    {
        showStockPileData = skillType == SkillType.StockPile;
        showPosGuideData = skillType == SkillType.PosGuide;
    }
}


public enum SkillType
{ 
    [LabelText("������(˲������)")]None,
    [LabelText("��������")] Chant,
    [LabelText("��������")] Ballistic,
    [LabelText("��������")] StockPile,
    [LabelText("λ����������")] PosGuide,
}

/// <summary>
/// �����׶�����
/// </summary>
[System.Serializable]
public class StockPileData
{
    [LabelText("�����׶�id")]
    public int stage;

    [LabelText("��ǰ�����׶δ����ļ���id")]
    public int skillId;

    [LabelText("��ǰ�׶δ�����ʼʱ��")]
    public int startTimeMs;

    [LabelText("��ǰ�׶ν���ʱ��")]
    public int endTimeMs;
}
