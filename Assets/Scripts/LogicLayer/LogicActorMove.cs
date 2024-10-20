using FixMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LogicMove移动
/// </summary>
public partial class LogicActor
{
    private FixIntVector3 mInputMoveDir;
    /// <summary>
    /// 逻辑帧位置更新
    /// </summary>
    public void OnLogicFrameUpdateMove()
    {
        if(ActionSate != LogicObjectActionState.Idle && ActionSate != LogicObjectActionState.Move)
        {
            return;
        }
        //计算逻辑位置
        LogicPos += mInputMoveDir* LogicMoveSpeed * (FixInt)LogicFrameConfig.LogicFrameInterval;

        //计算逻辑对象朝向
        if(LogicDir != mInputMoveDir)
        {
            LogicDir = mInputMoveDir;
        }

        //计算逻辑轴向
        if(LogicDir.x != FixInt.Zero)
        {
            LogicXAxis = LogicDir.x > 0 ? 1 : -1;
        }
    }

    /// <summary>
    /// 逻辑层接收摇杆方向
    /// </summary>
    /// <param name="inputDir"></param>
    public void InputLogicFrameEvent(FixIntVector3 inputDir)
    {
        mInputMoveDir = inputDir;
    }
}
