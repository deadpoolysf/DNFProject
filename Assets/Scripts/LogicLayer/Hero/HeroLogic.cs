using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;

public class HeroLogic : LogicActor
{
    public int HeroId { get; private set; }
    public HeroLogic(int heroId,RenderObject renderObj)
    {
        HeroId = heroId;
        RenderObj = renderObj;
        ObjectType = LogicObjectType.Hero;
    }
}
