using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LogicActor : LogicObject
{
    public override void OnCreate()
    {
        base.OnCreate();
    }

    public override void OnLogicFrameUpdate()
    {
        base.OnLogicFrameUpdate();
        //�����ƶ�֡
        OnLogicFrameUpdateMove();
        //���¼���֡
        OnLogicFrameUpdateSkill();
        //��������֡
        OnLogicFrameUpdateGravity();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
