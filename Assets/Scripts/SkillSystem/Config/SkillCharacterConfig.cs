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
    [LabelText("角色模型")]
    [PreviewField(70, ObjectFieldAlignment.Center)]
    public GameObject skillCharacter;

    [LabelText("技能动画")]
    [TitleGroup("技能渲染","所有英雄渲染数据会在技能一开始触发")]
    public AnimationClip skillAnim;

    [BoxGroup("动画数据")]
    [ProgressBar(0,100,r:0,g:255,b:0,Height = 30)]
    [HideLabel]
    [OnValueChanged("OnAnimProgressValueChange")]
    public short animProgress = 0;

    [BoxGroup("动画数据")]
    [LabelText("是否循环动画")]
    public bool isLoopAnim = false;

    [BoxGroup("动画数据")]
    [LabelText("循环次数")]
    [ShowIf("isLoopAnim")]
    public int animLoopCount = 1;

    [BoxGroup("动画数据")]
    [LabelText("逻辑帧数")]
    public int logicFrame = 0;

    [BoxGroup("动画数据")]
    [LabelText("动画长度")]
    public float animLength = 0;

    [BoxGroup("动画数据")]
    [LabelText("技能推荐时长 毫秒ms")]
    public float skillDuration = 0;

    //临时角色对象
    private GameObject mTempCharacter;
    private bool mIsPlayAnim = false;   //是否播放动画，用来控制暂停动画
    private double mLastRunTime = 0;    //上次运行的时间
    private Animation mAnimation = null;

    [ButtonGroup("按钮数组")]
    [Button("播放", ButtonSizes.Large)]
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
    [ButtonGroup("按钮数组")]
    [Button("暂停", ButtonSizes.Large)]
    public void Pause()
    {
        mIsPlayAnim = false;
    }
    [GUIColor(0, 1, 0)]
    [ButtonGroup("按钮数组")]
    [Button("保存配置", ButtonSizes.Large)]
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
            //获取当前运行时间
            double currentTime = EditorApplication.timeSinceStartup - mLastRunTime;
            
            //计算动画播放进度
            float curAnimNormalizationValue = (float)currentTime / animLength;
            animProgress = (short)(Mathf.Clamp(curAnimNormalizationValue * 100, 0, 100));

            //计算逻辑帧
            logicFrame = (int)(currentTime / LogicFrameConfig.LogicFrameInterval);

            //采样动画
            mAnimation.clip.SampleAnimation(mTempCharacter, (float)currentTime);

            if(animProgress == 100)
            {
                PlaySkillEnd();
            }
            //触发窗口聚焦回调，刷新窗口
            progressUpdateCallback?.Invoke();
        }
    }

    public void PlaySkillEnd()
    {
        mIsPlayAnim = false;
    }

    //动画进度改变监听
    public void OnAnimProgressValueChange(float value)
    {
        //根据当前动画进度进行动画采样
        string characterName = skillCharacter.name;
        mTempCharacter = GameObject.Find(characterName);
        if (mTempCharacter == null)
        {
            mTempCharacter = GameObject.Instantiate(skillCharacter);
        }
        mAnimation = mTempCharacter.GetComponent<Animation>();

        float progressValue = (value / 100) * skillAnim.length;
        logicFrame = (int)(progressValue / LogicFrameConfig.LogicFrameInterval);
        //采样动画
        mAnimation.clip.SampleAnimation(mTempCharacter, progressValue);
    }
}
