using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[HideMonoScript]
[System.Serializable]
public class SkillCharacterConfig
{
    [AssetList]
    [LabelText("��ɫģ��")]
    [PreviewField(70, ObjectFieldAlignment.Center)]
    public GameObject skillCharacter;

    [LabelText("���ܶ���")]
    [TitleGroup("������Ⱦ","����Ӣ����Ⱦ���ݻ��ڼ���һ��ʼ����")]
    public AnimationClip skillAnim;

    [BoxGroup("��������")]
    [ProgressBar(0,100,r:0,g:255,b:0,Height = 30)]
    [HideLabel]
    [OnValueChanged("OnAnimProgressValueChange")]
    public short animProgress = 0;

    [BoxGroup("��������")]
    [LabelText("�Ƿ�ѭ������")]
    public bool isLoopAnim = false;

    [BoxGroup("��������")]
    [LabelText("ѭ������")]
    [ShowIf("isLoopAnim")]
    public int animLoopCount = 1;

    [BoxGroup("��������")]
    [LabelText("�߼�֡��")]
    public int logicFrame = 0;

    [BoxGroup("��������")]
    [LabelText("��������")]
    public float animLength = 0;

    [BoxGroup("��������")]
    [LabelText("�����Ƽ�ʱ�� ����ms")]
    public float skillDuration = 0;

    //��ʱ��ɫ����
    private GameObject mTempCharacter;
    private bool mIsPlayAnim = false;   //�Ƿ񲥷Ŷ���������������ͣ����
    private double mLastRunTime = 0;    //�ϴ����е�ʱ��
    private Animation mAnimation = null;

    [ButtonGroup("��ť����")]
    [Button("����", ButtonSizes.Large)]
    public void Play()
    {
        if(skillCharacter != null)
        {
            string characterName = skillCharacter.name;
            mTempCharacter = GameObject.Find(characterName);
            if(mTempCharacter == null)
            {
                mTempCharacter = GameObject.Instantiate(skillCharacter);
            }
            mAnimation = mTempCharacter.GetComponent<Animation>();
            if(!mAnimation.GetClip(skillAnim.name))
            {
                mAnimation.AddClip(skillAnim, skillAnim.name);
            }
            mAnimation.clip = skillAnim;

            logicFrame = (int)(isLoopAnim ? skillAnim.length / LogicFrameConfig.LogicFrameInterval * animLoopCount : skillAnim.length / LogicFrameConfig.LogicFrameInterval);
            animLength = isLoopAnim ? skillAnim.length * animLoopCount : skillAnim.length;
            skillDuration = (int)(isLoopAnim ? skillAnim.length *1000 * animLoopCount : skillAnim.length * 1000);

            mLastRunTime = 0;
            mIsPlayAnim = true;
        }
    }
    [ButtonGroup("��ť����")]
    [Button("��ͣ", ButtonSizes.Large)]
    public void Pause()
    {
        mIsPlayAnim = false;
    }
    [GUIColor(0, 1, 0)]
    [ButtonGroup("��ť����")]
    [Button("��������", ButtonSizes.Large)]
    public void SaveAssets()
    {

    }

    public void OnUpdate(System.Action progressUpdateCallback)
    {
        if(mIsPlayAnim)
        {
            if(mLastRunTime==0)
            {
                mLastRunTime = EditorApplication.timeSinceStartup;
            }
            //��ȡ��ǰ����ʱ��
            double currentTime = EditorApplication.timeSinceStartup - mLastRunTime;
            
            //���㶯�����Ž���
            float curAnimNormalizationValue = (float)currentTime / animLength;
            animProgress = (short)(Mathf.Clamp(curAnimNormalizationValue * 100, 0, 100));

            //�����߼�֡
            logicFrame = (int)(currentTime / LogicFrameConfig.LogicFrameInterval);

            //��������
            mAnimation.clip.SampleAnimation(mTempCharacter, (float)currentTime);

            if(animProgress == 100)
            {
                PlaySkillEnd();
            }
            //�������ھ۽��ص���ˢ�´���
            progressUpdateCallback?.Invoke();
        }
    }

    public void PlaySkillEnd()
    {
        mIsPlayAnim = false;
    }

    //�������ȸı����
    public void OnAnimProgressValueChange(float value)
    {
        //���ݵ�ǰ�������Ƚ��ж�������
        string characterName = skillCharacter.name;
        mTempCharacter = GameObject.Find(characterName);
        if (mTempCharacter == null)
        {
            mTempCharacter = GameObject.Instantiate(skillCharacter);
        }
        mAnimation = mTempCharacter.GetComponent<Animation>();

        float progressValue = (value / 100) * skillAnim.length;
        logicFrame = (int)(progressValue / LogicFrameConfig.LogicFrameInterval);
        //��������
        mAnimation.clip.SampleAnimation(mTempCharacter, progressValue);
    }
}
