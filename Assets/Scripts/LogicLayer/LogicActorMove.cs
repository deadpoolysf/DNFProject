using FixMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LogicMove�ƶ�
/// </summary>
public partial class LogicActor
{
    private FixIntVector3 mInputMoveDir;
    /// <summary>
    /// �߼�֡λ�ø���
    /// </summary>
    public void OnLogicFrameUpdateMove()
    {
        if(ActionSate != LogicObjectActionState.Idle && ActionSate != LogicObjectActionState.Move)
        {
            return;
        }
        //�����߼�λ��
        LogicPos += mInputMoveDir* LogicMoveSpeed * (FixInt)LogicFrameConfig.LogicFrameInterval;

        //�����߼�������
        if(LogicDir != mInputMoveDir)
        {
            LogicDir = mInputMoveDir;
        }

        //�����߼�����
        if(LogicDir.x != FixInt.Zero)
        {
            LogicXAxis = LogicDir.x > 0 ? 1 : -1;
        }
    }

    /// <summary>
    /// �߼������ҡ�˷���
    /// </summary>
    /// <param name="inputDir"></param>
    public void InputLogicFrameEvent(FixIntVector3 inputDir)
    {
        mInputMoveDir = inputDir;
    }
}
