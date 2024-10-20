using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;

public class HeroRender : RenderObject
{
    //Ӣ���߼���ű�
    private HeroLogic mHeroLogic;
    //��ɫ����
    private Animation mAnim;
    //��ǰҡ������
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
        //�߼�����
        FixIntVector3 logicDir = FixIntVector3.zero;
        if(inputDir != Vector3.zero)
        {
            logicDir.x = inputDir.x;
            logicDir.y = inputDir.y;
            logicDir.z = inputDir.z;
        }
        mInputDir = inputDir;
        //��Ӣ���߼���ֱ���������֡�߼�����û�з��������µĲ��Դ��룩
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
        //�ж�ҡ�����벥�Ŷ���
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
