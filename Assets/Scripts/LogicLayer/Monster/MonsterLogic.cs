using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixIntPhysics;
using FixMath;

public class MonsterLogic : LogicActor
{
    public int MonsterId { get; private set; }
    public MonsterLogic(int monsterId,RenderObject renderObject,FixIntBoxCollider boxCollider,FixIntVector3 logicInitPos)
    {
        MonsterId = monsterId;
        RenderObj = renderObject;
        Collider = boxCollider;
        LogicPos = logicInitPos;
        ObjectType = LogicObjectType.Monster;
    }
}
