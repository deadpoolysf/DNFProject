using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;

public class HeroRender : RenderObject
{
    //英雄逻辑层脚本
    private HeroLogic mHeroLogic;
    //角色动画
    private Animation mAnim;
    //当前摇杆输入
    private Vector3 mInputDir;
    public override void OnCreate()
    {
        base.OnCreate();
        mHeroLogic = LogicObject as HeroLogic;
        JoystickUGUI.OnMoveCallBack += OnJoyStickMove;
        mAnim = transform.GetComponent<Animation>();
    }

    private void OnJoyStickMove(Vector3 inputDir)
    {
        //逻辑方向
        FixIntVector3 logicDir = FixIntVector3.zero;
        if(inputDir != Vector3.zero)
        {
            logicDir.x = inputDir.x;
            logicDir.y = inputDir.y;
            logicDir.z = inputDir.z;
        }
        mInputDir = inputDir;
        //向英雄逻辑层直接输入操作帧逻辑，（没有服务端情况下的测试代码）
        mHeroLogic.InputLogicFrameEvent(logicDir);
    }

    public override void OnRelease()
    {
        base.OnRelease();
        JoystickUGUI.OnMoveCallBack -= OnJoyStickMove;
    }

    public override void Update()
    {
        base.Update();
        //判断摇杆输入播放动画
        if(mInputDir.x==0 && mInputDir.y==0)
        {
            PlayAnim("Anim_Idle02");
        }
        else
        {
            PlayAnim("Anim_Run");
        }
    }

    public void PlayAnim(string animName)
    {
        mAnim.CrossFade(animName, 0.2f);
    }
}
